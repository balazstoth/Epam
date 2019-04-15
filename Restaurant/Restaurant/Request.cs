namespace Restaurant
{
    class Request
    {
        public Order Order { get; set; }
        public Client Client { get; set; }

        public Request(Order o, Client c)
        {
            Order = o;
            Client = c;
        }
    }
}
