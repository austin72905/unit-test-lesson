namespace TestNinja.Mocking
{
    public class OrderService
    {
        /*
            state-based test
            單個類的狀態(包含return 的值、屬性質的變化)
            interaction test (通常只測試有外部依賴的類)
            多個類之間的互動
            ex:
            Publisher 、Subscriber 兩個類
            有通過基於狀態的測試，我們可以驗證發布者是否跟踪其訂閱者。
            不過，更有趣的問題是，訂閱者是否收到了發布者發送的消息
            可以用Mock object來測試
            
         
        */
        private readonly IStorage _storage;

        public OrderService(IStorage storage)
        {
            _storage = storage;
        }

        public int PlaceOrder(Order order)
        {
            //return 0;
            var orderId = _storage.Store(order);
            
            // Some other work

            return orderId; 
        }
    }

    public class Order
    {
    }

    public interface IStorage
    {
        int Store(object obj);
    }
}