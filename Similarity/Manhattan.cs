using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScienceAssignment.Similarity
{
    class Manhattan : ISimilarity
    {
        /* Manhatten formula : d(p,q) = |p1-q1| + |pn - qn| */
        public double getSimilarity(Vector user1, Vector user2)
        {
            var length = user1.Size();
            var manhattanDistance = 0.0;

            for (var i = 0; i < length; i++)
            {
                manhattanDistance += Math.Abs(user1.getPoints()[i] - user2.getPoints()[i]);
            }

            /* Similarity formula : sim = 1 / (1+d) */
            return 1 / (1 + manhattanDistance);
        }
    }
}
