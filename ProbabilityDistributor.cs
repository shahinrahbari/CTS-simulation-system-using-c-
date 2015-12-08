using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASIM.classes
{
    public class ProbabilityDitributor
    {
        public static Troschuetz.Random.PoissonDistribution poisson;
        public static Troschuetz.Random.TriangularDistribution triangular;
        public static Troschuetz.Random.DiscreteUniformDistribution uniform;
        public static Troschuetz.Random.ExponentialDistribution exponential;
        public static Troschuetz.Random.BernoulliDistribution bernoulli;
        public double a;
        public string distributionMethod;
        public int MaxServiceTime;
        public ProbabilityDitributor(string _method  ,  double _a , int _max)
        {
            MaxServiceTime = _max;
            distributionMethod = _method;
            a = _a;
            
            poisson = new Troschuetz.Random.PoissonDistribution();
            triangular = new Troschuetz.Random.TriangularDistribution();
            uniform = new Troschuetz.Random.DiscreteUniformDistribution();
            bernoulli = new Troschuetz.Random.BernoulliDistribution();
            exponential = new Troschuetz.Random.ExponentialDistribution();
        }
        public int GenerateInteger()
        {
            double p = 0;
            if (distributionMethod == "Triangular") // not tested
            {
                triangular.Gamma = a;
                p = triangular.NextDouble();
                p *= MaxServiceTime;
                p /= triangular.Maximum;
            }
            else if (distributionMethod == "Uniform") // not tested
            {
                p = uniform.NextDouble();
                p *= MaxServiceTime;
                p /= uniform.Maximum;
            }
            else if (distributionMethod == "Exponential")
            {
                exponential.Lambda = a;
                p = exponential.NextDouble();
                p *= MaxServiceTime;
                p /= exponential.Maximum;
            }
            else if (distributionMethod == "Bernoulli")
            {
                bernoulli.Alpha = a;
                p = bernoulli.NextDouble();
                p *= MaxServiceTime;
                p /= bernoulli.Maximum;
            }
            else if (distributionMethod == "Poisson")
            {
                poisson.Lambda = a;
                p = poisson.NextDouble();
                p *= MaxServiceTime;
                p /= poisson.Maximum;
            }
            else if (distributionMethod == "Constant")
            {
                p = poisson.Maximum;
            }
            return ChangeToInt(p);
        }


        public double GenerateDouble()
        {
            double p = 0;
            if (distributionMethod == "Triangular") // not tested
            {
                triangular.Gamma = a;
                p = triangular.NextDouble();
                p *= MaxServiceTime;
                p /= triangular.Maximum;
            }
            else if (distributionMethod == "Uniform") // not tested
            {
                p = uniform.NextDouble();
                p *= MaxServiceTime;
                p /= uniform.Maximum;
            }
            else if (distributionMethod == "Exponential")
            {
                exponential.Lambda = a;
                p = exponential.NextDouble();
                p *= MaxServiceTime;
                p /= exponential.Maximum;
            }
            else if (distributionMethod == "Bernoulli")
            {
                bernoulli.Alpha = a;
                p = bernoulli.NextDouble();
                p *= MaxServiceTime;
                p /= bernoulli.Maximum;
            }
            else if (distributionMethod == "Poisson")
            {
                poisson.Lambda = a;
                p = poisson.NextDouble();
                p *= MaxServiceTime;
                p /= poisson.Maximum;
            }
            else if (distributionMethod == "Constant")
            {
                p = poisson.Maximum;
            }
            return ChangeToDouble(p);
        }
        public double ChangeToDouble(double d)
        {
            string ss = d.ToString();
            ss += "00000";
            string newss = "";
            newss += ss[0];
            newss += ss[1];
            for (int i = 2; i < ss.Length; i++)
            {
                newss += ss[i];
                if (ss[i - 2] == '.')
                {
                    break;
                }
            }
            double res = Convert.ToDouble(newss);
            return res;
        }
        public int ChangeToInt(double d)
        {
            string ss = d.ToString();
            string newss = "";
            newss += ss[0];
            for (int i = 1; i < ss.Length; i++)
            {
                newss += ss[i];
                if (ss[i - 1] == '.')
                {
                    break;
                }
            }
            int res = Convert.ToInt32(Convert.ToDouble(newss));
            return res;
        }
    }
}
