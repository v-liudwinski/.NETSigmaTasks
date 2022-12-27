using System.Threading;

namespace Homework12_LiudvynskyiV.S
{
    internal class Program
    {

        public static void CustomerServing()
        {
            var manager = DesksManager.GetInstance();
            var nooneArrived = false;
            
            while(true)
            {
                Thread.Sleep(2000); // time to serve a client 2 seconds
                var customersServed = manager.ServeOneCustomerPerDesk();
                if(customersServed == 0)
                {
                    if (nooneArrived)
                    {
                        break;
                    }
                    nooneArrived = true;
                    Thread.Sleep(1000); // waiting until customers come 1 second
                }
                else
                {
                    nooneArrived = false;
                }

            }
        }

        static void Main(string[] args)
        {

            var generator = new CustomersGenerator("../../../Customers.txt");
            var manager = DesksManager.GetInstance();
            var customerServingThread = new Thread(new ThreadStart(Program.CustomerServing));
            customerServingThread.Start();
            while (true) 
            {
                Customer? customer = generator.GetNewCustomer();
                Thread.Sleep(100); //time until new customer arrives 1 millisecond
                if (customer == null)
                {
                    break;
                }
                
                var deskId = customer.PickAPayDesk();
                manager.AssignToADesk(deskId, customer);

            }
            manager.CloseADesk(0); // closing desk 0
            
            customerServingThread.Join();
        }
    }
}