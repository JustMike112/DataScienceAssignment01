using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScienceAssignment.Similarity
{
    interface ISimilarity
    {
        double getSimilarity(Vector user1, Vector user2);
    }
}
