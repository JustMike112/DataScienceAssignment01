using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataScienceAssignment.Similarity;

namespace DataScienceAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("|| Assignment 1: User-Item ||");
            Console.WriteLine();

            var ratings = Parser.Parse(',', "./userItem.data");

            var commonRatings3and4 = CommonRatings.getCommonRatings(ratings[3], ratings[4]);
            var pearson = new Pearson();
            var pearson3and4 = pearson.getSimilarity(commonRatings3and4.Item1, commonRatings3and4.Item2);
            Console.WriteLine("// Pearson coefficient of similarity between users 3 and 4: " + pearson3and4);

            Console.WriteLine();

            Console.WriteLine("// Nearest neighbours and similarities for user 7 (using pearson): ");
            var nearestNeighbourUser7Pearson = new NearestNeighbours(ratings, 7, "pearson", 3, 0.35);
            foreach (var nearestNeighbor in nearestNeighbourUser7Pearson.nearestNeighbours.OrderByDescending(x => x.Value))
                Console.WriteLine("User " + nearestNeighbor.Key + " = Similarity: " + nearestNeighbor.Value);
            
            Console.WriteLine("// Nearest neighbours and similarities for user 7 (using cosine): ");
            var nearestNeighbourUser7Cosine = new NearestNeighbours(ratings, 7, "cosine", 3, 0.35);
            foreach (var nearestNeighbor in nearestNeighbourUser7Cosine.nearestNeighbours.OrderByDescending(x => x.Value))
                Console.WriteLine("User " + nearestNeighbor.Key + " = Similarity: " + nearestNeighbor.Value);

            Console.WriteLine("// Nearest neighbours and similarities for user 7 (using euclidean): ");
            var nearestNeighbourUser7Euclidean = new NearestNeighbours(ratings, 7, "euclidean", 3, 0.35);
            foreach (var nearestNeighbor in nearestNeighbourUser7Euclidean.nearestNeighbours.OrderByDescending(x => x.Value))
                Console.WriteLine("User " + nearestNeighbor.Key + " = Similarity: " + nearestNeighbor.Value);

            Console.WriteLine();

            Console.WriteLine("// Predicted ratings for user 7 (using pearson): ");
            var predictedRatingUser7item101 = Prediction.getPrediction(ratings, nearestNeighbourUser7Pearson.nearestNeighbours, 7, 101);
            Console.WriteLine("Item 101 has a predicted rating of: " + predictedRatingUser7item101);

            var predictedRatingUser7item103 = Prediction.getPrediction(ratings, nearestNeighbourUser7Pearson.nearestNeighbours, 7, 103);
            Console.WriteLine("Item 103 has a predicted rating of: " + predictedRatingUser7item103);

            var predictedRatingUser7item106 = Prediction.getPrediction(ratings, nearestNeighbourUser7Pearson.nearestNeighbours, 7, 106);
            Console.WriteLine("Item 106 has a predicted rating of: " + predictedRatingUser7item106);

            Console.WriteLine();

            Console.WriteLine("// Predicted Rating for user 4 (using pearson): ");
            var nearestNeighbourUser4Pearson = new NearestNeighbours(ratings, 4, "pearson", 3, 0.35);
            var predictedRatingUser4item101 = Prediction.getPrediction(ratings, nearestNeighbourUser4Pearson.nearestNeighbours, 4, 101);
            Console.WriteLine("Item 101 has a predicted rating of: " + predictedRatingUser4item101);

            Console.WriteLine();

            Console.WriteLine("// Adjusted 106 Rating to 2.8 for user 7 (using pearson) gives predictions: ");
            ratings[7][106] = 2.8;
            var nearestNeighbourUser7PearsonAdj1 = new NearestNeighbours(ratings, 7, "pearson", 3, 0.35);
            var predictedRatingUser7Adjusted1item101 = Prediction.getPrediction(ratings, nearestNeighbourUser7PearsonAdj1.nearestNeighbours, 7, 101);
            var predictedRatingUser7Adjusted1item103 = Prediction.getPrediction(ratings, nearestNeighbourUser7PearsonAdj1.nearestNeighbours, 7, 103);
            Console.WriteLine("Item 101 has a predicted rating of: " + predictedRatingUser7Adjusted1item101);
            Console.WriteLine("Item 103 has a predicted rating of: " + predictedRatingUser7Adjusted1item103);

            Console.WriteLine();

            Console.WriteLine("// Adjusted 106 Rating to 5 for user 7 (using pearson) gives predictions: ");
            ratings[7][106] = 5;
            var nearestNeighbourUser7PearsonAdj2 = new NearestNeighbours(ratings, 7, "pearson", 3, 0.35);
            var predictedRatingUser7Adjusted2item101 = Prediction.getPrediction(ratings, nearestNeighbourUser7PearsonAdj2.nearestNeighbours, 7, 101);
            var predictedRatingUser7Adjusted2item103 = Prediction.getPrediction(ratings, nearestNeighbourUser7PearsonAdj2.nearestNeighbours, 7, 103);
            Console.WriteLine("Item 101 has a predicted rating of: " + predictedRatingUser7Adjusted2item101);
            Console.WriteLine("Item 103 has a predicted rating of: " + predictedRatingUser7Adjusted2item103);

            Console.WriteLine();

            var movieRatings = Parser.Parse('\t', "u.data");

            Console.WriteLine("// After importing the movie dataset, the top 8 recommendations for user 186: ");
            var movieUser186Pearson = new NearestNeighbours(movieRatings, 186, "pearson", 25, 0.35);
            var movieUser186PearsonTop8 = Prediction.getTopPredictions(movieRatings, movieUser186Pearson.nearestNeighbours, 186, 0, 8);
            foreach (var prediction in movieUser186PearsonTop8)
            {
                Console.WriteLine("Movie: {0} has a rating of {1}", prediction.Key, prediction.Value);
            }

            Console.WriteLine();

            Console.WriteLine("// The top 8 recommendations for user 186 with at least three nearest neighbours: ");
            var movieUser186PearsonTop8Adj = Prediction.getTopPredictions(movieRatings, movieUser186Pearson.nearestNeighbours, 186, 3, 8);
            foreach (var prediction in movieUser186PearsonTop8Adj)
            {
                Console.WriteLine("Movie: {0} has a rating of {1}", prediction.Key, prediction.Value);
            }

            Console.WriteLine();

            //Console.WriteLine("Parsed Data file: ");
            //foreach (var outerEntry in ratings)
            //{
            //    foreach (var innerEntry in outerEntry.Value)
            //    {
            //        Console.WriteLine("User:{0} Key:{1} Value:{2}", outerEntry.Key, innerEntry.Key, innerEntry.Value);
            //    }
            //}

            Console.ReadLine();
        }
    }
}
