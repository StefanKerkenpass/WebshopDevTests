using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyFirstRest.Model
{
    public class ShoppingCart
    {
        [Key]
        [StringLength(20)]
        [JsonIgnore]
        public string Id { get; set; }

        public List<ShoppingCartPosition> Positions { get; set; } = new();

        public ShoppingCart()
        {
        }

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            for (int i = 0; i < Positions.Count; i++)
            {
                ShoppingCartPosition position = Positions[i];
                var price = position.Article.Price;
                int quantity = position.Quantity;
                totalPrice = totalPrice + price * quantity;
            }

            return totalPrice;
        }

        public void AddArticle(Article article, int quantity)
        {
            foreach (var position in Positions)
            {
                if (article.Equals(position.Article))
                {
                    position.Quantity = quantity;
                    return;
                }
            }

            Positions.Add(new ShoppingCartPosition(article, quantity, this));
        }
    }
}