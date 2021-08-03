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
        public ShoppingCart GetExact()
        {
            HttpContext.Request.Cookies.TryGetValue("cartId", out var id);
            var exactArticle = _context.ShoppingCarts
                .Include(cart => cart.Positions)
                .ThenInclude(position => position.Article)
                .FirstOrDefault(cart => cart.Id == id);
            return exactArticle;
        }

        [HttpPost()]
        public IActionResult UpdateArticle([FromBody] UpdateArticleRequest? request)
        {
            if (request == null) return BadRequest("Missing request data");
            if (string.IsNullOrEmpty(request.ArticleNumber)) return BadRequest("Missing request data");
            if (!request.Quantity.HasValue) return BadRequest("Missing request data");
            if (request.Quantity <= 0) return BadRequest("Quantity is to low!");

            HttpContext.Request.Cookies.TryGetValue("cartId", out var cartId);

            ShoppingCart cart;

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
                cart = _context.ShoppingCarts.Find(cartId);
                //   Wenn nicht gefunden, dann Fehlermeldung
                if (cart == null)
                {
                    return BadRequest("Cart not found");
                }
            }


            //   Wenn gefunden:
            //     -Artikel im WarenKorb suchen
            ShoppingCartPosition? position = _context
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

            return Ok();
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteArticle([FromBody] DeleteArticleRequest? requestDelete)
        {
            if (requestDelete == null) return BadRequest("Missing request data");


            HttpContext.Request.Cookies.TryGetValue("cartId", out var cartIdDelete);

            ShoppingCart cartDelete;

            // Den ShoppingCart suchen

            cartDelete = _context.ShoppingCarts.Find(cartIdDelete);
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

            return Ok();


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