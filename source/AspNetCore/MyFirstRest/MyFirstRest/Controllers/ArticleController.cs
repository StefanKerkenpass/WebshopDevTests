using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MyFirstRest.Model;

namespace MyFirstRest.Controllers
{
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        
        private readonly StoreContext _context;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(StoreContext context, ILogger<ArticleController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public IActionResult CreateArticle([FromBody] CreateArticleRequest request)
        {
            if (_context.Articles.Any(listArticle => listArticle.Number == request.Number))
            {
                ModelState.AddModelError("Number", "Gibt es bereits");
            }
            
            /*if (_context.Articles.Find(request.Number) != null)
            {
                ModelState.AddModelError("Number", "Gibt es bereits");
            }*/
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var article = new Article
            {
                Number = request.Number,
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };
            
            _context.Articles.Add(article);

            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public async Task<Article[]> Get()
        {
           // await Task.Delay(1000);
            
            _logger.LogTrace("Ich hole mir jetzt alle Artikel");
            var articles = _context.Articles.ToArray();
            return articles;

            /*using var connection = new SqlConnection("Server=Develop;Database=Praktikant;Trusted_Connection=True;");
            
            connection.Open();

            using var command = new SqlCommand("select Number, Name, Price, Description from Articles", connection);
            using var reader = command.ExecuteReader();

            var articles = new List<Article>();

            while (reader.Read())
            {
                var number = reader.GetString(0);
                var name = reader.GetString(1);
                var price = reader.GetDecimal(2);
                var description = reader.GetString(3);
                var article = new Article
                {
                    Number = number,
                    Name = name,
                    Price = price,
                    Description = description
                };
                
                articles.Add(article);
            }

            return articles.ToArray();*/
        }
        
        [HttpGet("Exact")]
        public async Task<Article> GetExact(string id)
        {
            var exactArticle = _context.Articles.Find(id);
            return exactArticle;
            

            // using var connection = new SqlConnection("Server=Develop;Database=Praktikant;Trusted_Connection=True;");
            //     
            //     connection.Open();
            //
            //     using var command = new SqlCommand("select * from Articles where Number = @id ", connection);
            //     command.Parameters.AddWithValue("@id", id);
            //     using var reader = command.ExecuteReader();
            //
            //     var articles = new List<Article>();
            //
            //     while (reader.Read())
            //     {
            //         var number = reader.GetString(0);
            //         var name = reader.GetString(1);
            //         var price = reader.GetDecimal(2);
            //         var description = reader.GetString(3);
            //         var article = new Article
            //         {
            //             Number = number,
            //             Name = name,
            //             Price = price,
            //             Description = description
            //         };
            //         
            //         articles.Add(article);
            //     }
            //
            //     return articles.ToArray();
            //     
        }
       
    }
}