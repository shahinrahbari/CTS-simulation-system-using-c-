using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Queue
    {
        public string name;
        public int lenght;
        public ConsoleApplication1.SIMSystem sys;
        // type of queue (FIFO , FILO , ...)
        public List<ConsoleApplication1.Server> nextItems;
        public List<Customer> customersInQueue;
        public Queue(string _name, int _lenght, ConsoleApplication1.SIMSystem _sys)
        {
            sys = _sys;
            name = _name;
            lenght = _lenght;
            nextItems = new List<Server>();
            customersInQueue = new List<Customer>();
        }
        public void AddNextItem(ConsoleApplication1.Server _server)
        {
            nextItems.Add(_server);
            nextItems = nextItems.OrderBy(o => o.priority).ToList();
        }

        public ConsoleApplication1.Server findIdleServer()
        {
            foreach (ConsoleApplication1.Server s in nextItems)
            {
                s.CheckBusyStatus();
            }
            for (int i = 0; i < nextItems.Count; i++)
            {
                if (!nextItems[i].isBusy)
                {
                    return nextItems[i];
                }
            }
            return null;
        }
        public void Scane()
        {
            // add new customers to the list
            for (int i = 0; i < sys.customers.Count; i++)
            {
                Customer c = sys.customers[i];
                if (c.position == this.name && !customersInQueue.Contains(c))
                {
                    customersInQueue.Add(c);
                }
            }



            // try pass customer to server
            
            ConsoleApplication1.Server s = findIdleServer();

            if (s != null && customersInQueue.Count != 0)
            {

                customersInQueue[0].GoToItem(s.name);
                customersInQueue.RemoveAt(0);
            }
            else
            {
                foreach (Customer c in customersInQueue)
                {
                    c.delay++;
                }
            }
        }


    }
}
