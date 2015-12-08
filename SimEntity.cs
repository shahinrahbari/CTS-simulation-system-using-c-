using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASIM.classes
{
    public class SimEntity
    {
        public static Random R = new Random();
        public Troschuetz.Random.TriangularDistribution triangular = new Troschuetz.Random.TriangularDistribution();
        public Troschuetz.Random.DiscreteUniformDistribution uniform = new Troschuetz.Random.DiscreteUniformDistribution();
        public Troschuetz.Random.ExponentialDistribution exponential = new Troschuetz.Random.ExponentialDistribution();
        public Troschuetz.Random.BernoulliDistribution bernoulli = new Troschuetz.Random.BernoulliDistribution();
        public Troschuetz.Random.PoissonDistribution poisson = new Troschuetz.Random.PoissonDistribution();
        // define probability distribution parameters
        public int craetingProbability;
        public string distributionMethod = "Uniform";
        public string name = "";
        public int MaxServiceTime = 8;
        public double a = 0.5;
        public ConsoleApplication1.SIMSystem sys;
        public SimEntity(double _a, int _maxServiceTime , string _disributionMethod , int _cp , string _name)
        {
            craetingProbability = _cp;
            distributionMethod = _disributionMethod;
            a = _a;
            MaxServiceTime = _maxServiceTime;
            name = _name;
        }



       


        public int GenerateServiceTime()
        {
            ProbabilityDitributor pd = new ProbabilityDitributor(distributionMethod, a, MaxServiceTime);
            return pd.GenerateInteger();
        }


    }
}
