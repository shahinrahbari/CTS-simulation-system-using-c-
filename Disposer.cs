using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    
    public class Disposer
    {
        public string name;
        public ConsoleApplication1.SIMSystem sys;
        public Disposer(string _name, ConsoleApplication1.SIMSystem _sys)
        {
            sys = _sys;
            name = _name;
        }

        void DisposCustomer(Customer c)
        {
            for (int i = 0; i < sys.customers.Count; i++)
            {
                if (sys.customers[i].ID == c.ID )
                {
                    c.isInSystem = false;

                }
            }
        }

        public void Scane()
        {
            for (int i = 0; i < sys.customers.Count; i++)
            {
                if (sys.customers[i].position == this.name)
                {
                    DisposCustomer(sys.customers[i]);
                }
            }
        }

    }
}
