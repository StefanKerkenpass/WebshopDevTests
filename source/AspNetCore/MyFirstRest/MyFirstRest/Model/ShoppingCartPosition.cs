using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyFirstRest.Model
{
    public class ShoppingCartPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }

        [JsonIgnore] public ShoppingCart ShoppingCart { get; private set; }

        public Article Article { get; private set; }

        public int Quantity { get; set; }
        
        public ShoppingCartPosition(Article article, int quantity, ShoppingCart shoppingCart)
        {
            Article = article;
            Quantity = quantity;
            ShoppingCart = shoppingCart;
        }

        protected ShoppingCartPosition()
        {
        }
    }
}