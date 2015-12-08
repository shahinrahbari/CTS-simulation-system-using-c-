using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MASIM;

namespace ConsoleApplication1
{
    public class SIMSystem
    {
        public int clk;
        public int finishClk;
        public int numberInService;
        public int numberInQueue;
        public bool finishConditions;
        public  List<Customer> customers;
        public List<Server> servers;
        public List<MASIM.classes.SimEntity> entities;
        public List<Creator> creators;
        public List<Disposer> disposers;
        public List<Queue> queues;
        public List<MASIM.classes.DecideProb> decides;
        public List<MASIM.classes.Inventory> inventroies;

        // components list
        public List<CreatorComponent> creatorComponents ;
        public List<DisposerComponent> disposerComponents;
        public List<QueueComponent> queuesComponents;
        public List<ServerComponent> serversComponents;
        public List<EntityComponent> entitiesComponets;
        public List<DecideComponent> decideComponents;
        public List<InventoryComponent> inventoryComponents;
        public SIMSystemComponent simSystemComponent;

        public DailyCreator dailyCreator;

        public int lastCustomerID;

        public SIMSystem(int _finishClk)
        {
            clk = 0;
            finishClk = _finishClk;
            finishConditions = false;
            customers = new List<Customer>();
            servers = new List<Server>();
            creators = new List<Creator>();
            disposers = new List<Disposer>();
            queues = new List<Queue>();
            entities = new List<MASIM.classes.SimEntity>();
            decides = new List<MASIM.classes.DecideProb>();
            inventroies = new List<MASIM.classes.Inventory>();

            creatorComponents = new List<MASIM.CreatorComponent>();
            disposerComponents = new List<MASIM.DisposerComponent>();
            queuesComponents = new List<MASIM.QueueComponent>();
            serversComponents = new List<MASIM.ServerComponent>();
            entitiesComponets = new List<EntityComponent>();
            decideComponents = new List<DecideComponent>();
            inventoryComponents = new List<InventoryComponent>();
            

            numberInQueue = 0;
            numberInService = 0;
            lastCustomerID = 0;
        }

        public void SetSystems()
        {
            dailyCreator.sys = this;
            for (int i = 0; i < creatorComponents.Count; i++)
            {
                if (creatorComponents[i].Visible)
                {
                    creators[i].sys = this;
                }
            }

            for (int i = 0; i < disposerComponents.Count; i++)
            {
                if (disposerComponents[i].Visible)
                {
                    disposers[i].sys = this;
                }
            }

            for (int i = 0; i < queuesComponents.Count; i++)
            {
                if (queuesComponents[i].Visible)
                {
                    queues[i].sys = this;
                }
            }


            for (int i = 0; i < serversComponents.Count; i++)
            {
                if (serversComponents[i].Visible)
                {
                    servers[i].sys = this;
                }

            }


            for (int i = 0; i < entitiesComponets.Count; i++)
            {
                if (entitiesComponets[i].Visible)
                {
                    entities[i].sys = this;
                }
            }

            for (int i = 0; i < decideComponents.Count; i++)
            {
                if (decideComponents[i].Visible == true)
                {
                    decides[i].sys = this;
                }
            }
            for (int i = 0; i < inventoryComponents.Count; i++)
            {
                if (inventoryComponents[i].Visible == true)
                {
                    inventroies[i].sys = this;
                }
            }
        }

        public void Delay(int time)
        {
            System.Timers.Timer timedelay;
            timedelay = new System.Timers.Timer(time);
            timedelay.Enabled = true;
            timedelay.Start();
        }


        public void InitaileComponents()
        {
            // put componets scripts
            for (int i = 0; i < creatorComponents.Count; i++)
            {
                if (creatorComponents[i].Visible)
                {
                    creators.Add(creatorComponents[i].c);
                }
            }
            dailyCreator.creatorsList = creators;
            dailyCreator.SetProbs();
            dailyCreator.setCreator();

            for (int i = 0; i < disposerComponents.Count; i++)
            {
                if (disposerComponents[i].Visible)
                {
                    disposers.Add(disposerComponents[i].disposer);
                }
            }

            for (int i = 0; i < queuesComponents.Count; i++)
            {
                if (queuesComponents[i].Visible)
                {
                    queues.Add(queuesComponents[i].queue);
                }
            }

            for (int i = 0; i < entitiesComponets.Count; i++)
            {
                if(entitiesComponets[i].Visible)
                {
                entities.Add(entitiesComponets[i].entity);
                }
            }

            for (int i = 0; i < serversComponents.Count; i++)
            {
                if (serversComponents[i].Visible)
                {
                    servers.Add(serversComponents[i].server);
                }

            }

            for (int i = 0; i < decideComponents.Count; i++)
            {
                if (decideComponents[i].Visible == true)
                {
                    decides.Add(decideComponents[i].decide);
                }
            }

            for (int i = 0; i < inventoryComponents.Count; i++)
            {
                if (inventoryComponents[i].Visible == true)
                {
                    inventroies.Add(inventoryComponents[i].inventory);
                }
            }
        }


        public void NextStep()
        {
                SetSystems();
                // call scane method for all components
                dailyCreator.Scane();
                dailyCreator.setCreator();
                dailyCreator.mainCreator.Scane();

                
                for (int i = 0; i < servers.Count; i++)
                {
                    servers[i].Scane();
                }
                for (int i = 0; i < customers.Count; i++)
                {
                    customers[i].Scane();
                }
                for (int i = 0; i < disposers.Count; i++)
                {
                    disposers[i].Scane();
                }
                for (int i = 0; i < queues.Count; i++)
                {
                    queues[i].Scane();
                }

                for (int i = 0; i < decides.Count; i++)
                {
                    decides[i].Scan();
                }

                for (int i = 0; i < inventroies.Count; i++)
                {
                    inventroies[i].Scan();
                }

                // refresh all stats of some components
                for (int i = 0; i < customers.Count; i++)
                {
                    customers[i].RefreshStats();
                }
                for (int i = 0; i < servers.Count; i++)
                {
                    servers[i].RefreshStats();
                }
                   

                for (int i = 0; i < creatorComponents.Count; i++)
                {
                    creatorComponents[i].Sync();
                }
                for (int i = 0; i < disposerComponents.Count; i++)
                {
                    disposerComponents[i].Sync();
                }
                for (int i = 0; i < queuesComponents.Count; i++)
                {
                    queuesComponents[i].Sync();
                }
                for (int i = 0; i < serversComponents.Count; i++)
                {
                    serversComponents[i].Sync();
                }

                for (int i = 0; i < decideComponents.Count; i++)
                {
                    decideComponents[i].Sync();
                }
                // sync for inventories    


                simSystemComponent.Sync();
                clk++;
            
        }

        public void Start()
        {


            InitaileComponents();


            while (clk < finishClk)
            {
                NextStep();
            }
        }
    }
}
