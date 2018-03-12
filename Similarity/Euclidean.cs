using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScienceAssignment.Similarity
{
    class Euclidean : ISimilarity
    {
        /* Euclidian formula : d(p,q) = sqrt(pow(p1-q1)+pow(pn-qn)) */

        public double getSimilarity(Vector user1, Vector user2)
        {
            var length = user1.Size();
            var total = 0.0;

            for (var i = 0; i < length; i++)
            {
                total += Math.Pow(user1.getPoints()[i] - user2.getPoints()[i], 2);
            }

            var euclideanDistance = Math.Sqrt(total);

            /* Similarity formula : sim = 1 / (1+d) */
            return 1 / (1 + euclideanDistance);
        }


    }
}
