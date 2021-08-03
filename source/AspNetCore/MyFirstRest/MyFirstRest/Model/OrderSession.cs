namespace MyFirstRest.Model
{
    public enum OrderSessionState
    {
        InProgress,
        Aborted,
        Ordered
    }

    public class OrderSession
    {
        public ShoppingCart ShoppingCart { get; set; }
        public OrderSessionState State { get; set; } = OrderSessionState.InProgress;

        public OrderSession()
        {
            ShoppingCart = new ShoppingCart();
        }
    }
}