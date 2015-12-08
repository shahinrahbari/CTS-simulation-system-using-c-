using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public struct serverStat
    {
        public bool isBusy;
        public int totalServiceTime;
        public int beginTime;
        public int finishTime;
        public int serviceTime;
        public int priority;
    }


    public class Server
    {
        public static Random R;
        public string name;
        public bool isBusy;
        public int totalServiceTime;
        public int beginTime;
        public int finishTime;
        public string nextItem;
        public int serviceTime;
        public int priority;
        public ConsoleApplication1.SIMSystem sys;
        Customer customerInService;
        public List<serverStat> serverStats;
        // queue
        public Server(string _name, ConsoleApplication1.SIMSystem _sys, string _nextItem, int _priority)
        {
            name = _name;
            isBusy = false;
            totalServiceTime = 0;
            beginTime = 0;
            finishTime = 0;
            nextItem = "";
            sys = _sys;
            serviceTime = 0;
            nextItem = _nextItem;
            priority = _priority;
            R = new Random();
            serverStats = new List<serverStat>();
            SetNullState();
        }


        public void CheckBusyStatus()
        {
            bool flag = false;
            for (int i = 0; i < sys.customers.Count; i++)
            {
                if (sys.customers[i].position == this.name)
                {
                    isBusy = true;
                    flag = true;
                }
            }
            if (!flag)
            {
                isBusy = false;
            }
            
        }

        public void SetNullState()
        {
            for (int i = 0; i < serverStats.Count; i++)
            {
                serverStat ss = new serverStat();
                ss.beginTime = -1;
                ss.finishTime = -1;
                ss.isBusy = false;
                ss.serviceTime = -1;
                ss.totalServiceTime = -1;
                serverStats.Add(ss);
            }
        }

        public void Scane()
        {
            for (int i = 0; i < sys.customers.Count; i++)
            {
                Console.WriteLine(isBusy);
                Customer c = sys.customers[i];
                if (c.position == this.name)
                {
                    

                    //set a random int for service time   NOTE!!!!!
                    if (c.serviceTime == -1)
                    {
                        c.AssignRandomServiceTime();
                        isBusy = true;
                        beginTime =  sys.clk;
                    }
                    
                    // if its work with server was complete pass to next item 
                    if (c.serviceTime == 0)
                    {
                        c.serviceTime = -1;
                        c.GoToItem(this.nextItem);
                        isBusy = false;
                        finishTime = sys.clk;
                        serviceTime = c.serviceTime;//just for show service time in server component
                        //Console.WriteLine("BABI");
                    }
                    else 
                    {
                        c.serviceTime -= 1;
                        serviceTime = c.serviceTime;//just for show service time in server component
                    }
                }
                
            }
        }

        public void serviceForNewCusomer(Customer c)
        {
            isBusy = true;
            customerInService = c;
        }
        public void releaseCustomer()
        {
            isBusy = false;
            // serve a service for new customer of queue
        }
        public bool canGetNewCustomer()
        {
            return !isBusy;
        }
        void setNextItem(string _nextItem)
        {
            nextItem = _nextItem;
        }
        public void RefreshStats()
        {
            serverStat ss = new serverStat();
            ss.beginTime = beginTime;
            ss.finishTime = finishTime;
            ss.isBusy = isBusy;
            ss.serviceTime = serviceTime;
            ss.totalServiceTime = totalServiceTime;
            serverStats.Add(ss);
        }


    }
}
