using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASIM.classes
{
    public class NameGenerator
    {
        
        public NameGenerator()
        {
        
        }

        public string NewServerName(int ServersCount)
        {
            string s = "S";
            s += ServersCount.ToString();
            return s;
        }
        public string NewCreatorName(int CreatorsCount)
        {
            string s = "C";
            s += CreatorsCount.ToString();
            return s;
        }
        public string NewDisposerName(int DisposersCount)
        {
            string s = "D";
            s += DisposersCount.ToString();
            return s;
        }
        public string NewEntityName(int EntityCount)
        {
            string s = "E";
            s += EntityCount.ToString();
            return s;
        }
        public string NewQueueName(int QueuesCount)
        {
            string s = "Q";
            s += QueuesCount.ToString();
            return s;
        }

        public string NewDecideName(int DecideprobsCount)
        {
            string s = "DP";
            s += DecideprobsCount.ToString();
            return s;
        }

        public string NewInventoryName(int InventoryCount)
        {
            string s = "In";
            s += InventoryCount.ToString();
            return s;
        }


    }
}
