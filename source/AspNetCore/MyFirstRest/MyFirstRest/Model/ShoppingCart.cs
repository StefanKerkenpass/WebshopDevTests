using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyFirstRest.Model
{
    public class ShoppingCart
    {
        [Key] [StringLength(20)] [JsonIgnore] public string Id { get; set; }

        [NotMapped] public decimal TotalPrice => CalculateTotalPrice();

        public List<ShoppingCartPosition> Positions { get; set; } = new();

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (var position in Positions)
            {
                var price = position.Article.Price;
                var quantity = position.Quantity;
                totalPrice += price * quantity;
            }

            return totalPrice;
        }
    }
}