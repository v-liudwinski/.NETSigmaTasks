using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework12_LiudvynskyiV.S
{
    public sealed class DesksManager
    {
        private Dictionary<int, PayDesk> desks;
        private int numberOfDesks;

        private static readonly object _lock = new object();
        private static DesksManager _instance;

        public static DesksManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new DesksManager();
                }
            }
            return _instance;
        }
        
        private DesksManager(int numberOfDesks = 10)
        {
            this.numberOfDesks = numberOfDesks;
            this.CreateDesks();
        }

        public void FilterVIPs(int deskId, int peopleInQueue)
        {
            lock (_lock)
            {
                var desk = this.desks[deskId];
                if (desk.QueueWasOverloaded)
                {
                    desk.CloseDeskWithoutRearranging();
                    return;
                }
                for (var i = 0; i <= desk.PeopleInTheQueue; i++)
                {
                    var customer = desk.CustomerServed();

                    if (customer == null)
                    {
                        break;
                    }
                    if (customer.Status == CustomerStatuses.VIP)
                    {
                        this.AssignToADesk(deskId, customer, false);
                    }
                    else
                    {
                        int newDeskId = customer.PickAPayDesk(desk.Id);
                        this.AssignToADesk(newDeskId, customer, false);

                        ActivityReport.WriteACustomerMoved(customer, desk.Id, newDeskId, (DateTime.Now - customer.GotInTheQueue).Seconds);
                    }
                }
                desk.QueueWasOverloaded = true;
                desk.CheckQueueLength();
            }
        }
        
        public int GetDeskWithTheShortestQueue(int evoidDesk = -1)
        {
            var shortestQueueId = -1;
            var smallestAmount = int.MaxValue;
            foreach (var deskPair in this.desks.Where(deskPair => deskPair.Value.IsOpened && deskPair.Value.Id != evoidDesk))
            {
                shortestQueueId = deskPair.Value.PeopleInTheQueue < smallestAmount ? deskPair.Value.Id : shortestQueueId;
                smallestAmount = deskPair.Value.PeopleInTheQueue < smallestAmount ? deskPair.Value.PeopleInTheQueue : smallestAmount;
            }
            return shortestQueueId;
        }
        
        public int GetClosestDesk(int customerCoord, int evoidDesk = -1)
        {
            var closestDeskId = -1;
            var closestDeskDistance = int.MaxValue;
            foreach (var deskPair in this.desks.Where(deskPair => deskPair.Value.IsOpened && deskPair.Value.Id != evoidDesk))
            {
                closestDeskId = Math.Abs(deskPair.Value.LocationY - customerCoord) < closestDeskDistance ? deskPair.Value.Id : closestDeskId;
                closestDeskDistance = Math.Abs(deskPair.Value.LocationY - customerCoord) < closestDeskDistance ? Math.Abs(deskPair.Value.LocationY - customerCoord) : closestDeskDistance;
            }
            return closestDeskId;
        }
        
        public void AssignToADesk(int deskId, Customer customer, bool checkLength = true)
        {
            this.desks[deskId].AddCustomer(customer, checkLength);
            customer.CustomerGotInTheQueue(this.desks[deskId].LocationY);
        }
        
        public int PeopleInTheQueue(int deskId)
        {
            return this.desks[deskId].PeopleInTheQueue;
        }
        
        public void CloseADesk(int deskId)
        {
            var desk = this.desks[deskId];
            if (!desk.IsOpened)
            {
                return;
            }
            this.RearrangeQueue(desk);
            try
            {
                desk.CloseDesk();
            }
            catch (Exception e)
            {
                this.CloseADesk(deskId);

            }
        }
        
        public int ServeOneCustomerPerDesk()
        {
            lock (_lock)
            {
                var customersServed = 0;
                foreach (KeyValuePair<int, PayDesk> deskPair in this.desks)
                {
                    if (!deskPair.Value.IsOpened) continue;
                    var customer = deskPair.Value.CustomerServed();
                    if (customer != null)
                    {
                        ActivityReport.WriteACustomerServed(customer, deskPair.Key, customer.WaitedTimeSeconds());
                        customersServed++;
                    }
                }
                return customersServed;
            }
        }

        private void RearrangeQueue(PayDesk desk)
        {
            lock (_lock)
            {
                while (true)
                {
                    var customer = desk.CustomerServed();
                    if (customer == null)
                    {
                        break;
                    }
                    var newDeskId = customer.PickAPayDesk(desk.Id);
                    this.AssignToADesk(newDeskId, customer);

                    ActivityReport.WriteACustomerMoved(customer, desk.Id, newDeskId, (DateTime.Now - customer.GotInTheQueue).Seconds);
                }
            }
        }
        
        private void CreateDesks()
        {
            var deskCoordinate = 0;
            this.desks = new Dictionary<int, PayDesk>();
            for (var i = 0; i < this.numberOfDesks; i++)
            {
                PayDesk desk = new PayDesk(i, deskCoordinate);
                desk.QueueOverloaded += this.FilterVIPs;
                this.desks[i] = desk;
                deskCoordinate += 2;
            }
        }
    }
}

