using Restaurant.Foods;
using System;
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
            else
                Console.WriteLine("WaitressRobot: Order(s) processed");
        }

        public void Start()
        {
            Console.WriteLine($"WaitressRobot: Processing {_orders.Count} order(s)...");
            TakeToTheKitchen();
        }

        void TakeToTheKitchen()
        {
            Request current = _orders.Dequeue();
            currentClient = current.Client;
            _kitchen.Cook(current.Order);
        }

        public void TakeOrder(Client client, Order order)
        {
            _orders.Enqueue(new Request(order,client));
            order.FoodReady += OrderIsReady;
            Console.WriteLine($"WaitressRobot: Order registered, client: {client.ToString()}, order: {order.ToString()}");
        }

        private void OrderIsReady(FoodReadyEventArgs foodReadyEventArgs)
        {
            ServeOrders(foodReadyEventArgs.Food);
        }
    }
}
