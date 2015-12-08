using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MASIM.classes;

namespace ConsoleApplication1
{

    public struct ProbCreator
    {
        public Creator c;
        public int p;
        public int a;
        public int b;
    }
    public  class DailyCreator
    {
        private Random r;
        public List<ProbCreator> probCreators;
        public List<Creator> creatorsList;
        public int initialClock;
        public int dayLenght;
        public string name;
        public  ConsoleApplication1.SIMSystem sys;
        private int B;  // end point of probs
        public Creator mainCreator;

        public DailyCreator(int _dayLenght)
        {
            
            initialClock = 0;
            probCreators = new List<ProbCreator>();
            r = new Random();
        }

        public void SetProbs()
        {
            int a = 0;
            int b = 0;
            for (int i = 0; i < creatorsList.Count; i++)
            {
                ProbCreator pc = new ProbCreator();
                pc.c = creatorsList[i];
                pc.p = creatorsList[i].prob;
                pc.a = a;
                b += pc.p;
                pc.b =  b;
                probCreators.Add(pc);
                a = b;
            }
            B = b;
        }
        public void setCreator()
        {
            int x = r.Next(0, B);
            for (int i = 0; i < probCreators.Count; i++)
            {
                if (probCreators[i].a <= x && probCreators[i].b > x)
                {
                    mainCreator = probCreators[i].c;
                }
            }
        }


        public void Scane()
        {
            if (initialClock >= dayLenght)
            {
                initialClock = 0;
                setCreator();
            }
            initialClock += 1;
        }


    }
}
