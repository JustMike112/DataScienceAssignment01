using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScienceAssignment
{
    static class Prediction
    {
        /* Prediction formula : p = (rn * sn) / sn  */
        public static double getPrediction(Dictionary<int, Dictionary<int, double>> ratings, Dictionary<int, double> nearestNeighbours, int userId, int itemId)
        {
            var total = 0.0;
            var n = 0.0;
            var predictedRating = 0.0;

            foreach (var user in nearestNeighbours)
            {
                if (ratings[user.Key].ContainsKey(itemId))
                {
                    total += user.Value * ratings[user.Key][itemId];
                    n += user.Value;
                }
            }

            predictedRating = total / n;

            return predictedRating;
        }

        public static Dictionary<int, double> getTopPredictions(Dictionary<int, Dictionary<int, double>> ratings, Dictionary<int, double> nearestNeighbours, int userId, int minRatings, int maxItems)
        {
            Dictionary<int, double> topPredictions = new Dictionary<int, double>();
            List<int> allItems = new List<int>();

            // Add all the items to the list
            foreach (var user in ratings)
            {
                foreach (var item in user.Value)
                {
                    if (!allItems.Contains(item.Key))
                        allItems.Add(item.Key);
                }
            }

            // Loop over all the items
            foreach (var item in allItems)
            {
                var x = 0;
                if (minRatings > x)
                    foreach (var user in nearestNeighbours.Keys)
                        if (ratings[user].ContainsKey(item))
                            x += 1;

                if ((x >= minRatings) || (minRatings < 1))
                    topPredictions.Add(item, getPrediction(ratings, nearestNeighbours, userId, item));
            }
            
            // Return the max Items requested
            return topPredictions.OrderByDescending(x => x.Value).Take(maxItems).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
