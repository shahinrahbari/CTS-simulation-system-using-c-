using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASIM.classes
{
    public  class Inventory
    {
        public ConsoleApplication1.SIMSystem sys;
        public string name;
        public string nextItem;
        public double initialCost;
        public double sellingCost;
        public double salvageRevenue;

        public double totalProfit;
        public double orderCost;
        public double shortageCost;

        public int countOfItems;
        public int periodicLevelCheck;
        public int maxLevel;

        private int initialClock; // for check end of period
        private bool firstScan = true;
        // attributes for result

        public List<double> listOfTotalProfits;
        public List<double> listOfShortageCost;
        public List<double> listOfOrderCost;
        
        public Inventory(ConsoleApplication1.SIMSystem _sys,string _name, string _nextItem,double _initialC,double _sellingC,double _salvageR,int maxL,int periodLC)
        {
            sys = _sys;
            name = _name;
            nextItem = _nextItem;
            orderCost = 0;
            shortageCost = 0;
            totalProfit = 0;

            listOfOrderCost = new List<double>();
            listOfShortageCost = new List<double>();
            listOfTotalProfits = new List<double>();

            initialCost = _initialC;
            sellingCost = _sellingC;
            salvageRevenue = _salvageR;

            maxLevel = maxL;
            periodicLevelCheck = periodLC;
            
        }

        public void RefreshInventoryItems()
        {
            orderCost = (maxLevel - countOfItems) * initialCost;
            countOfItems = maxLevel;
            shortageCost = 0;
            totalProfit = 0;
        }

        public void CalculateProfitPerCustomerGetItem()
        {
            totalProfit += sellingCost;
            shortageCost += sellingCost - initialCost;
            countOfItems--;
        }


        

        public void EndPeriod()
        {
            if (salvageRevenue > 0)
            {
                for (int i = 0; i < countOfItems; i++)
                {
                    totalProfit += salvageRevenue;
                    shortageCost += salvageRevenue - initialCost;
                }
                countOfItems = 0;
            }
            listOfOrderCost.Add(orderCost);
            listOfShortageCost.Add(shortageCost);
            listOfTotalProfits.Add(totalProfit);
            initialClock = 0;
        }

        public void Scan()
        {
            if (firstScan)
            {
                RefreshInventoryItems();
                firstScan = false;
            }

            if (initialClock == periodicLevelCheck)
            {
                EndPeriod();
                RefreshInventoryItems();
            }
            for (int i = 0; i < sys.customers.Count; i++)
            {
                if (sys.customers[i].position == this.name)
                {
                    if (this.countOfItems > 0)
                    {
                        CalculateProfitPerCustomerGetItem();
                    }
                    sys.customers[i].GoToItem(this.nextItem);
                }
            }
            initialClock++;
        }



    }
}
