using System;
using System.Collections.Generic;
using Domain;

namespace Pathing
{
    public class MapPrinter
    {
        public void PrintMap(Map map, Path currentPath)
        {
            var mapCoords = new Dictionary<int, string[]>();
            for (int i = 0; i < map.Height; i++)
            {
                mapCoords.Add(i, new string[map.Width]);
            }

            foreach (var worldObject in map.WorldObjects)
            {
                var pointsFromObject = new List<PrinterPoint>();
                for (int i = 0; i < worldObject.Height; i++)
                {
                    for (int j = 0; j < worldObject.Width; j++)
                    {
                        pointsFromObject.Add(new PrinterPoint{Y = worldObject.LocY + i, X = worldObject.LocX + j});
                    }
                }
                foreach (var printerPoint in pointsFromObject)
                {
                    var row = mapCoords[printerPoint.Y];
                    row[printerPoint.X] = "x";
                    mapCoords[printerPoint.Y] = row;
                }
            }

            foreach (var movement in currentPath.Movements)
            {
                var row = mapCoords[movement.NewY];
                row[movement.NewX] = "Z";
                mapCoords[movement.NewY] = row;
            }

            foreach (var line in mapCoords)
            {
                foreach (var spot in line.Value)
                {
                    if (string.IsNullOrEmpty(spot))
                        Console.Write(" ");
                    else
                    {
                        Console.Write(spot);
                    }
                }
                Console.Write("|");
                Console.WriteLine();
            }
        }
    }

    public class PrinterPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}