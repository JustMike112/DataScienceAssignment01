using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScienceAssignment.Similarity
{
    class Pearson : ISimilarity
    {
        /* Pearson formula : r =    (xn*yn - (max(x)*max(y))
                                    --------------------------
                         (sqrt(max(x)-(pow(max(x))/n) * (sqrt(max(y)-(pow(max(y))/n) */

        public double getSimilarity(Vector user1, Vector user2)
        {
            var length = user1.Size();
            var xTotal = 0.0;
            var yTotal = 0.0;
            var xSquared = 0.0;
            var ySquared = 0.0;
            var total = 0.0;
            var r = 0.0;

            for (var i = 0; i < length; i++)
            {
                xTotal += user1.getPoints()[i];
                yTotal += user2.getPoints()[i];
                xSquared += Math.Pow(user1.getPoints()[i], 2);
                ySquared += Math.Pow(user2.getPoints()[i], 2);
                total += user1.getPoints()[i] * user2.getPoints()[i];
            }

            var numerator = total - (xTotal * yTotal / length);
            var denominator = Math.Sqrt(xSquared - (Math.Pow(xTotal, 2) / length)) * Math.Sqrt(ySquared - (Math.Pow(yTotal, 2) / length));
            r = numerator / denominator;

            return r;
        }
    }
}
