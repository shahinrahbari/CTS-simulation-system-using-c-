using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;

namespace MASIM.classes
{
    public struct nextItemProbs
    {
        public string nextItem;
        public int probability;
        public int a;
        public int b;
    }

    public class DecideProb
    {

        public List<nextItemProbs> nextItemsList;
        public static Random R;
        public string name;
        public SIMSystem sys;


        private int sumOfProbs = 0;
        public void SetNextItemProbs()
        {
            List<nextItemProbs> newNextItemsList = new List<nextItemProbs>();
            int a = 0;
            int b = 0;
            for (int i = 0; i < nextItemsList.Count; i++)
            {
                nextItemProbs n = new nextItemProbs();
                b =+ nextItemsList[i].probability;
                n.nextItem = nextItemsList[i].nextItem;
                n.probability = nextItemsList[i].probability;
                n.a = a;
                n.b = b;
                a = b;
                newNextItemsList.Add(n);
            }
            sumOfProbs = b;
            nextItemsList = newNextItemsList;
        }
        public string FindNextItem()
        {
            string res = "";
            
            int r = R.Next(0, sumOfProbs);
            for (int i = 0; i < nextItemsList.Count; i++)
            {
                if (nextItemsList[i].a <= r && nextItemsList[i].b > r)
                {
                    res =  nextItemsList[i].nextItem;
                }
            }
            return res;
        }

       
        public DecideProb(SIMSystem _sys , List<nextItemProbs> _nextItemsList)
        {
            nextItemsList = _nextItemsList;
            sys = _sys;
            SetNextItemProbs();
            R = new Random();
        }

        public void Scan()
        {
            for (int i = 0; i < sys.customers.Count; i++)
            {
                if (sys.customers[i].position == this.name)
                {
                    sys.customers[i].GoToItem(FindNextItem());
                }

            }
        }
    }
}
