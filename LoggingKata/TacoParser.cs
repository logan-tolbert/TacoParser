using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();

        public ITrackable Parse(string line)
        {
            
            logger.LogInfo("Begin parsing");
            logger.LogInfo(line + "\n");

            var cells = line.Split(',');

            #region Error check: Line check

            if (cells.Length < 3)
            {
                logger.LogError("Error: Missing required data.");
                return null;
            }

            #endregion

            var isLatitude = double.TryParse(cells[0], out double latitude);
            #region Error check: latitude formatting

            if (!isLatitude)
            {
                logger.LogError("Error: Incorrect formatting of latitude");
                return null;
            }

            #endregion

            var isLongitude = double.TryParse(cells[1], out double longitude);
            #region Error check: longitude formatting
            if (!isLongitude)

            {
                logger.LogError("Error: Incorrect formatting of latitude");
                return null;
            }

            #endregion

            string name = cells[2];

            var coordinates = new Point();
            coordinates.Latitude = latitude;
            coordinates.Longitude = longitude;

            TacoBell tacoBell = new();
            tacoBell.Name = name;
            tacoBell.Location = coordinates;

            return tacoBell;

        }
    }
}
