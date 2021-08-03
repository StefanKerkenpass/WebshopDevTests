using System.Text.Json.Serialization;

namespace MyFirstRest.Model
{
    public class ShoppingCartPosition
    {
        public int Id { get; set; }

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