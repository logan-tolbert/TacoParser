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
           
            logger.LogInfo("Log initialized");

            // Optional: Log an error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable storeA = null;
            ITrackable storeB = null;
            
            double distance = 0;
            
            foreach (var origin in locations)
            {
                GeoCoordinate corA = new GeoCoordinate();
                GeoCoordinate corB = new GeoCoordinate();
                corA.Latitude = origin.Location.Latitude;
                corA.Longitude = origin.Location.Longitude;
            
                foreach (var destination in locations)
                {
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
            Console.WriteLine(storeA.Name);
            Console.WriteLine(storeB.Name);
        
        
    }
}}
