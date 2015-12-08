using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MASIM.classes;

namespace ConsoleApplication1
{
    

    public  class Creator
    {
        public double createRate;
        public static Random random = new Random();
        public double initialTime;
        public string nextItem;
        public string name;
        public  ConsoleApplication1.SIMSystem sys;
        public string distributionMethod;
        public double a;
        public int MaxServiceTime;
        public int prob;

        public Creator(double _cr, string _name, string _nextItem, SIMSystem _sys , string _distr ,double _a ,int _max , int _prob)
        {
            createRate = _cr;
            initialTime = 0;
            name = _name;
            nextItem = _nextItem;
            sys = _sys;
            a = _a;
            distributionMethod = _distr;
            MaxServiceTime = _max;
            prob = _prob;
        }
        private MASIM.classes.SimEntity SetEntity()
        {
            int a = 0;
            int b = 0;
            List<Tuple<int, int>> x = new List<Tuple<int, int>>();
            for (int i = 0; i < sys.entities.Count; i++)
            {
                b = sys.entities[i].craetingProbability;
                Tuple<int, int> T = new Tuple<int, int>(a, b);
                x.Add(T);
                a = b;
            }
            int r = random.Next(0, b);
            MASIM.classes.SimEntity result = sys.entities[0];
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i].Item1 < r && x[i].Item2 > r)
                {
                    result = sys.entities[i];
                }
            }
            return result;
        }
        public void Create()
        {
            Customer c = new Customer(this.sys , SetEntity());
            c.arrivalTime = sys.clk;
            sys.customers.Add(c);
            c.GoToItem(nextItem);
        }

        public void setNextItem(string _nextItem)
        {
            nextItem = _nextItem;
        }
        private void setCreateRate()
        {
            ProbabilityDitributor pd = new ProbabilityDitributor(distributionMethod, a, MaxServiceTime);
            createRate = pd.GenerateDouble();
        }
        public void Scane()
        {
            
            initialTime += 1;
            if (initialTime >= createRate)
            {
                initialTime -= createRate;
                Create();
                setCreateRate();
            }
        }


    }
}
