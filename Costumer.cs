using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MASIM.classes;

namespace ConsoleApplication1
{
    public struct CustomerStats
    {
        public int totolTimeInService;
        public string position;
        public int serviceTime;
        public int arrivalTime;
        public bool isInSystem;
        public int delay;
    }
    public  class Customer
    {


        public SimEntity entity;
        public int totalTimeinServices;
        public  string position;
        public  int serviceTime;
        public  int arrivalTime;
        public  int departureTime;
        public int ID;
        public bool isInSystem;
        public int delay;
        public ConsoleApplication1.SIMSystem sys;
        public List<CustomerStats> customerStats;
        public Customer(ConsoleApplication1.SIMSystem _sys , SimEntity _entity)
        {
            position = "";
            serviceTime = -1;
            arrivalTime = 0;
            departureTime = 0;
            sys = _sys;
            ID = sys.lastCustomerID;
            sys.lastCustomerID++;
            totalTimeinServices = 0;
            customerStats = new List<CustomerStats>();
            entity = _entity;
            isInSystem = true;
            SetNullState();
        }


        public void SetNullState()
        {
            for (int i = 0; i < sys.clk; i++)
            {
                CustomerStats cs = new CustomerStats();
                cs.arrivalTime = -1;
                cs.isInSystem = false;
                cs.position = "Untitled";
                cs.serviceTime = -1;
                cs.totolTimeInService = -1;
                customerStats.Add(cs);
            }
        }
        

        // TODO : edit with different types of probability distributions
        public void AssignRandomServiceTime()
        {
            serviceTime = entity.GenerateServiceTime();
            totalTimeinServices += serviceTime;
        }

        public void Scane()
        {
            //Console.WriteLine(ID.ToString() + "::::" + serviceTime.ToString());
            //Console.Write(serviceTime);
        }

        public void RefreshStats()
        {
            CustomerStats cs = new CustomerStats();
            cs.arrivalTime = arrivalTime;
            cs.position = position;
            cs.serviceTime = serviceTime;
            cs.totolTimeInService = totalTimeinServices;
            cs.isInSystem = isInSystem;
            cs.delay = delay;
            customerStats.Add(cs);
            
        }

        public void GoToItem(string _nextItem)
        {
            position = _nextItem;
        }
    }
}
