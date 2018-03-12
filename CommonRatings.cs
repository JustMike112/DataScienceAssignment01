using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScienceAssignment
{
    static class CommonRatings
    {
        public static Tuple<Vector,Vector> getCommonRatings(Dictionary<int, double> user1, Dictionary<int, double> user2)
        {
            var RatingsUser1 = new Vector();
            var RatingsUser2 = new Vector();

            // Loop over the items in the Target Users rating
            foreach (var item in user1.Keys)
            {
                // If the item is rated by both users, add the rating to the vector
                if (user2.ContainsKey(item)) {
                    RatingsUser1.AddPoint(user1[item]);
                    RatingsUser2.AddPoint(user2[item]);
                }
            }

            // Return a tuple containing the common ratings between the users
            return new Tuple<Vector, Vector>(RatingsUser1,RatingsUser2);
        }

        public static Tuple<Vector, Vector> getCosineRatings(Dictionary<int, double> user1, Dictionary<int, double> user2)
        {
            var RatingsUser1 = new Vector();
            var RatingsUser2 = new Vector();

            // Loop over the items in user1
            foreach (var item in user1.Keys)
            {
                // If the item is rated by both users, add the ratings to the vector, else add the rating to the vector of user1 and add 0 to the vector of user2
                if (user2.ContainsKey(item))
                {
                    RatingsUser1.AddPoint(user1[item]);
                    RatingsUser2.AddPoint(user2[item]);
                } else
                {
                    RatingsUser1.AddPoint(user1[item]);
                    RatingsUser2.AddPoint(0);
                }
            }
            // Loop over the items in user2
            foreach (var item in user2.Keys)
            {
                // if the ratings overlapse, it's already in the vector. so only add the values when it doesn't overlapse
                if (!user1.ContainsKey(item))
                {
                    RatingsUser1.AddPoint(0);
                    RatingsUser2.AddPoint(user2[item]);
                }
            }

            // Return a tuple containing the common ratings between the users
            return new Tuple<Vector, Vector>(RatingsUser1, RatingsUser2);
        }
    }
}
