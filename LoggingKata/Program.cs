using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
       
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            const double milePerMeter = 0.00062137;
            string divider = new string('-', 26);

            #region App Introduction
            Console.WriteLine
               (
                "{0, 33}",
                "Welcome to TacoParser!!!"
                );
            Console.WriteLine
                (
                "{0,37}",
                "Brought to you by TrueCoders.\n"
                );
            Console.WriteLine("\t" + divider + "\n");
            logger.LogInfo("Log initialized\n");
            #endregion

            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Line count: {lines.Count()}\n");
            
            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable storeA = null;
            ITrackable storeB = null;
            
            double distance = 0;
            
            foreach (var origin in locations)
            {
                GeoCoordinate corA = new GeoCoordinate();


                try
                {
                    corA.Latitude = origin.Location.Latitude;
                }
                catch (NullReferenceException err)
                {
                    logger.LogFatal("Fatal Error", err);

                }

                try
                {
                    corA.Longitude = origin.Location.Longitude;
                }
                catch (NullReferenceException err)
                {
                      logger.LogFatal("Fatal Error", err);
                }

                    foreach (var destination in locations)
                    {
                    GeoCoordinate corB = new GeoCoordinate();

                    corB.Latitude = destination.Location.Latitude;
                    corB.Longitude = destination.Location.Longitude;

                    double distanceBetween = corA.GetDistanceTo(corB);

                    if (distanceBetween > distance)
                    {
                        distance = distanceBetween;
                        storeA = origin;
                        storeB = destination;
                    }
                }
            }
            
            logger.LogInfo("Parsing complete");
            Console.WriteLine();

            #region Results
            Console.WriteLine("{0, 15}",
                "Results"
                );
            Console.WriteLine(divider);
            Console.WriteLine($"Store A:{storeA.Name}\nLatitude: {storeA.Location.Latitude}\nLongitude: {storeA.Location.Longitude}");
            Console.WriteLine();
            Console.WriteLine($"Store B:{storeB.Name}\nLatitude: {storeB.Location.Latitude}\nLongitude: {storeB.Location.Longitude}");
            Console.WriteLine($"Distance between: {distance * milePerMeter}");
            #endregion
        }
    }
}
