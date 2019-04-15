using Restaurant.Foods;
using System.Collections.Generic;

namespace Restaurant
{
    class Waitress
    {
        Queue<Request> _orders;
        Kitchen _kitchen;
        Client currentClient;

        public Waitress(Kitchen kitchen)
        {
            this._kitchen = kitchen;
            _orders = new Queue<Request>();
        }

        void ServeOrders(IFood food)
        {
            currentClient.Eat(food);
            if (_orders.Count > 0)
                TakeToTheKitchen();
        }

        void TakeToTheKitchen()
        {
            Request current = _orders.Dequeue();
            currentClient = current.Client;
            _kitchen.Cook(current.Order);
        }

        void TakeOrder(Client client, Order order)
        {
            _orders.Enqueue(new Request(order,client));
            order.FoodReady += OrderIsReady; 
        }

        private void OrderIsReady(FoodReadyEventArgs foodReadyEventArgs)
        {
            ServeOrders(foodReadyEventArgs.food);
        }
    }
}
