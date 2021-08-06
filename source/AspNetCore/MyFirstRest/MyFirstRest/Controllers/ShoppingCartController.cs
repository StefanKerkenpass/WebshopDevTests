using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFirstRest.Model;

namespace MyFirstRest.Controllers
{
    [Route("[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly StoreContext _context;
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly ShoppingCartIdGenerator _shoppingCartIdGenerator;

        public ShoppingCartController(StoreContext context, ILogger<ShoppingCartController> logger,
            ShoppingCartIdGenerator shoppingCartIdGenerator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _shoppingCartIdGenerator = shoppingCartIdGenerator;
        }

        [HttpGet("Exact")]
        public ShoppingCart? GetExact()
        {
            HttpContext.Request.Cookies.TryGetValue("cartId", out var id);
            var shoppingCart = _context.ShoppingCarts
                .Include(cart => cart.Positions)
                .ThenInclude(position => position.Article)
                .FirstOrDefault(cart => cart.Id == id);
            return shoppingCart;
        }

        [HttpPost()]
        public IActionResult UpdateArticle([FromBody] UpdateArticleRequest? request)
        {
            if (request == null) return BadRequest("Missing request data");
            if (string.IsNullOrEmpty(request.ArticleNumber)) return BadRequest("Missing request data");
            if (!request.Quantity.HasValue) return BadRequest("Missing request data");
            if (request.Quantity <= 0) return BadRequest("Quantity is to low!");

            HttpContext.Request.Cookies.TryGetValue("cartId", out var cartId);

            ShoppingCart? cart;

            // Den ShoppingCart suchen
            if (cartId == null)
            {
                cart = new ShoppingCart()
                {
                    Id = _shoppingCartIdGenerator.GenerateId()
                };
                _context.ShoppingCarts.Add(cart);
            }
            else
            {
                cart = _context.ShoppingCarts
                    .Include(x => x.Positions)
                    .ThenInclude(x => x.Article)
                    .SingleOrDefault(x => x.Id == cartId);
                //   Wenn nicht gefunden, dann Fehlermeldung
                if (cart == null)
                {
                    cart = new ShoppingCart()
                    {
                        Id = _shoppingCartIdGenerator.GenerateId()
                    };
                    _context.ShoppingCarts.Add(cart);
                }
            }


            //   Wenn gefunden:
            //     -Artikel im WarenKorb suchen
            var position = _context
                .ShoppingCartPositions
                .FirstOrDefault(pos => pos.ShoppingCart == cart && pos.Article.Number == request.ArticleNumber);

            //     -wenn gefunden, dann Quant gegen die neue Quant ausstauschen
            if (position != null)
            {
                position.Quantity = request.Quantity.Value;
            }
            else
            {
                //     -wenn nicht gefunden, dann Artikel in der Artikelliste suchen
                var article = _context.Articles.Find(request.ArticleNumber);
                if (article == null)
                {
                    //        -wenn nicht gefunden, dann fehlermeldung
                    return BadRequest("Article not found");
                }

                //        -wenn gefunden, dann zum Warenkorb hinzufügen
                position = new ShoppingCartPosition(article, request.Quantity.Value, cart);
                _context.ShoppingCartPositions.Add(position);
            }


            _context.SaveChanges();

            HttpContext.Response.Cookies.Append("cartId", cart.Id, new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(14),
            });

            return Json(cart);
        }

        [HttpPost("Bill")]
        public IActionResult PostBill([FromBody] CheckOutRequest? request)
        {
            if (request == null) return BadRequest("Missing request data");
            
            HttpContext.Request.Cookies.TryGetValue("cartId", out var cartId);
            
            BillHead? bill;
            ShoppingCart? cart;
            BillPosition? position;
            
            cart = _context.ShoppingCarts
                .Include(x => x.Positions)
                .ThenInclude(x => x.Article)
                .SingleOrDefault(x => x.Id == cartId);
            //   Wenn nicht gefunden, dann Fehlermeldung
            if (cart == null)
            {
                return BadRequest("Cart not found");
            }
            var cartPos = _context
                .ShoppingCartPositions
                .Include(x => x.Article)
                .FirstOrDefault(pos => pos.ShoppingCart == cart);
            var article = cartPos.Article;
            if (article == null)
            {
                return BadRequest("No Articles in Cart");
            }


            bill = new BillHead()
            {
                Id = new Guid(),
                Date = DateTime.Now,
                Salutation = request.Salutation,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Country = request.Country,
                Location = request.Location,
                ZipCode = request.ZipCode,
                Street = request.Street,
                TotalPrice = cart.TotalPrice
            };
            _context.BillHeads.Add(bill);
            
            
            
            position = new BillPosition()
            {
                Id = new Guid(),
                ArticleName = article.Name,
                ArticlePrice = article.Price,
                ArticleNumber = article.Number,
                Quantity = cartPos.Quantity,
                BillHeadId = bill.Id
            };
            _context.BillPosition.Add(position);
            
            if (bill != null)
            {
                cart = new ShoppingCart()
                {
                    Id = _shoppingCartIdGenerator.GenerateId()
                };
                _context.ShoppingCarts.Add(cart);
            }
            
            _context.SaveChanges();
            
            HttpContext.Response.Cookies.Append("cartId", cart.Id, new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(14),
            });
            
            return Json(bill);
            
        }


        [HttpDelete("Delete")]
        public IActionResult DeleteArticle([FromBody] DeleteArticleRequest? requestDelete)
        {
            if (requestDelete == null) return BadRequest("Missing request data");


            HttpContext.Request.Cookies.TryGetValue("cartId", out var cartIdDelete);

            ShoppingCart? cartDelete;

            // Den ShoppingCart suchen

            cartDelete = _context.ShoppingCarts.Include(x => x.Positions)
                .ThenInclude(x => x.Article)
                .SingleOrDefault(x => x.Id == cartIdDelete);
            //   Wenn nicht gefunden, dann Fehlermeldung
            if (cartDelete == null)
            {
                return BadRequest("Cart not found");
            }


            //   Wenn gefunden:
            //     -Position im WarenKorb suchen
            ShoppingCartPosition? positionDelete = _context
                .ShoppingCartPositions
                .Find(requestDelete.Id);

            if (positionDelete == null)
            {
                return BadRequest("Position not found");
            }

            // -wenn gefunden, dann Position löschen
            _context.ShoppingCartPositions.Remove(positionDelete);

            _context.SaveChanges();

            HttpContext.Response.Cookies.Append("cartId", cartDelete.Id, new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(14),
            });

            return Json(cartDelete);


            //     StreamReader r = new StreamReader("articles.json");
            //     var content = r.ReadToEnd();
            //     var articleJsonContent = content;
            //     var articles = JsonSerializer.Deserialize<Article[]>(articleJsonContent);
            //     if (articles == null)
            //     {
            //         throw new NullReferenceException(nameof(articles));
            //     }
            //
            //     Article selectedArticle = null;
            //     for (int i = 0; i < articles.Length; i++)
            //     {
            //         var article = articles[i];
            //         var number = article.Number;
            //         if (number == request.ArticleNumber)
            //         {
            //             selectedArticle = article;
            //             break;
            //         }
            //     }
            //
            //     if (selectedArticle == null)
            //     {
            //         return BadRequest("Article with article number not found!");
            //     }
            //
            //     var user = UserAccount.Default();
            //     user.Session.ShoppingCart.AddArticle(selectedArticle, request.Quantity.GetValueOrDefault(1));
            //     return Ok(user.Session.ShoppingCart);
        }


        public class UpdateArticleRequest
        {
            public string? ArticleNumber { get; set; }
            public int? Quantity { get; set; }
        }
        
        public class CheckOutRequest
        {
            public Guid Id { get; set; }
            public DateTime Date { get; set; }
            public string Salutation { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Country { get; set; }
            public string Location { get; set; }
            public string ZipCode { get; set; }
            public string Street { get; set; }
        }
        
        public class DeleteArticleRequest
        {
            public int Id { get; set; }
        }

        public class CreateShoppingCartRequest
        {
            public string? Id { get; set; }
        }
    }
}