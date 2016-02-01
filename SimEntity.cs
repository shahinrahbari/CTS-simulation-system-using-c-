using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;


namespace MASIM.classes
{
    public class SimEntity
    {
        // define probability distribution parameters
        public ProbabilityDitributor pd;
        public int craetingProbability;
        public string name = "";
        public ConsoleApplication1.SIMSystem sys;
        public List<Creator> creatorsList;

        // personality attribute
        public int happyness;
        public string PType;
        int h;
        public int score;
        

        public SimEntity(int _cp, string _name, ProbabilityDitributor _pd, string _PType = "Busy", int _happyness = 5)
        {
            craetingProbability = _cp;
            pd = _pd;
            name = _name;
            creatorsList = new List<Creator>();
            PType = _PType;
            happyness = _happyness;
            h = happyness;
            score = 0;
        }
        
        public Tuple<List<Customer> , Customer> ActionInsideQueue(Queue q , Customer owner)
        {
            if (PType == "Cheater")
            {
                // go
                int ownerIndex = q.customersInQueue.IndexOf(owner);
                Customer temp1 = q.customersInQueue[ownerIndex];
                int poorIndex = Math.Max(0, ownerIndex - 1);
                Customer temp2 = q.customersInQueue[poorIndex];
                q.customersInQueue[poorIndex] = owner;
                q.customersInQueue[ownerIndex] = temp2;
                q.customersInQueue[ownerIndex].SetStatus(@"/O\"); 
                q.customersInQueue[ownerIndex].entity.score -= 1;
                owner.SetStatus(" ;) ");
                owner.entity.score += 5;
            }
            else if (PType == "Poor")
            {
                // do nothing
                score--;
                owner.status = " :| ";
            }
            else if (PType == "Busy")
            {
                score -= 5;
                owner.SetStatus(" :( ");
                // dispose the customer
                q.customersInQueue.Remove(owner);
                for (int i = 0; i < sys.customers.Count; i++)
                {
                    if (sys.customers[i].ID == owner.ID)
                    {
                        sys.customers[i] = owner;
                        owner.isInSystem = false;
                        owner.position = "Out Of System!";
                    }
                }
            }
            else if (PType == "Rioter")
            {
                score--;
                for (int i = 0; i < q.customersInQueue.Count; i++)
                {
                    q.customersInQueue[i].entity.happyness--;
                }
                owner.SetStatus(" :E ");
                // reduce happyness from poeple around him/her
            }
            Tuple<List<Customer>, Customer> t = new Tuple<List<Customer>, Customer>(q.customersInQueue, owner);
            return t ;
        }
        //Life is Strange

        //CODEX
        //Binaries
        //Win32


        public void ActionInsideInventory(Inventory inv)
        {
            if (inv.countOfItems == 0)
            {
                score -= 10;
            }
            else
            {
                score += 60;
            }
            score -= Convert.ToInt32(inv.sellingCost);
        }

        public void ActionInsideServer()
        {
            score += 4;
        }

        public string DecideWithAwareness(List<ConsoleApplication1.Queue> nextQueues)
        {
            int minIndex = 0;
            int minLength = 100000;
            for (int i = 0; i < nextQueues.Count; i++)
            {
                if (minLength > nextQueues[i].customersInQueue.Count)
                {
                    minLength = nextQueues[i].customersInQueue.Count;
                    minIndex = i;
                }
            }
            return nextQueues[minIndex].name;
        }
        public Tuple<List<Customer>,Customer> BehaviorInQueue(Queue q , Customer c)
        {
            h--;
            if (h == 0)
            {
                h = happyness;
                if (q != null)
                {
                    //Tuple<List<Customer> , Customer> t = DoActionWithQueue(q,c);
                    return ActionInsideQueue(q  ,c);
                }
            }
            Tuple<List<Customer>, Customer> TT = new Tuple<List<Customer>, Customer>(q.customersInQueue, c);
            return TT;
        }

        public int GenerateServiceTime()
        {
            return pd.GenerateInteger();
        }


    }
}
