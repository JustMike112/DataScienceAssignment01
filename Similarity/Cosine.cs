using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScienceAssignment.Similarity
{
    class Cosine : ISimilarity
    {
        /* Cosine formula : cos(x, y) = (x * y) / (sqrt(pow(xn)) * sqrt(pow(yn))) */
        public double getSimilarity(Vector user1, Vector user2)
        {
            var length = user1.Size();
            var total = 0.0;
            var xSquared = 0.0;
            var ySquared = 0.0;

            for (var i = 0; i < length; i++)
            {
                total += (user1.getPoints()[i] * user2.getPoints()[i]);
                xSquared += Math.Pow(user1.getPoints()[i], 2);
                ySquared += Math.Pow(user2.getPoints()[i], 2);
            }

            var cos = total / (Math.Sqrt(xSquared) * Math.Sqrt(ySquared));
            return cos;
        }

    }
}
