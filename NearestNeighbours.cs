using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataScienceAssignment.Similarity;

namespace DataScienceAssignment
{
    class NearestNeighbours
    {
        public Dictionary<int, double> nearestNeighbours = new Dictionary<int, double>();

        public NearestNeighbours(Dictionary<int, Dictionary<int, double>> ratings, int userId, ISimilarity similarityFunction, int maxNeighbours, double threshold)
        {

            // loop over the users in the dictionary
            foreach (var user in ratings)
            {
                // only consider the ratings of the users that are not our target user
                if (user.Key != userId)
                {
                    Tuple<Vector, Vector> commonRatings;
                    
                    if (similarityFunction.GetType() != typeof(Cosine))
                        commonRatings = CommonRatings.getCommonRatings(ratings[userId], user.Value);
                    else
                        commonRatings = CommonRatings.getCosineRatings(ratings[userId], user.Value);

                    var newSimilarity = similarityFunction.getSimilarity(commonRatings.Item1, commonRatings.Item2);
                    var anotherRating = anotherRatingYesOrNo(ratings[userId], user.Value);

                    if (newSimilarity > threshold && anotherRating)
                    {
                        if (nearestNeighbours.Count() < maxNeighbours)
                            nearestNeighbours.Add(user.Key, newSimilarity);
                        else
                        {
                            var oldSimilarity = nearestNeighbours.OrderBy( y => y.Value ).First();
                            if (newSimilarity > oldSimilarity.Value)
                            {
                                nearestNeighbours.Remove(oldSimilarity.Key);
                                nearestNeighbours.Add(user.Key, newSimilarity);
                            }
                            
                        }
                    }
                    if (nearestNeighbours.Count() == maxNeighbours)
                    {
                        threshold = nearestNeighbours.Values.Min();
                    }
                }
            }

        }

        private bool anotherRatingYesOrNo(Dictionary<int, double> user1, Dictionary<int, double> user2)
        {
            return user2.Any(x => !user1.Keys.Contains(x.Key));
        }


    }
}
