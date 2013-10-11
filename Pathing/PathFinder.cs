using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Pathing
{
    public class PathFinder
    {
        public Path CreatePath(Map map, IHaveLocation start, IHaveLocation end)
        {
            PathingMap pathingMap;
            var adjacentPoints = new List<AdjectPoint>
                {
                    new AdjectPoint(1, 1),
                    new AdjectPoint(1, 0),
                    new AdjectPoint(1, -1),
                    new AdjectPoint(0, -1),
                    new AdjectPoint(-1, -1),
                    new AdjectPoint(-1, 0),
                    new AdjectPoint(-1, 1),
                    new AdjectPoint(0, 1)
                };
            var path = new Path();
            var lowestLength = 0;
            for (int i = 0; i < 7; i++)
            {
                pathingMap = BuildPathingMap(map);
                var startingPoint = new PathingPoint { LocX = start.LocX, LocY = start.LocY };
                startingPoint.IsCurrent = true;
                pathingMap.ClosedPoints.Add(startingPoint);
                var chosenPath = new List<PathingPoint>();
                while (startingPoint.LocX != end.LocX || startingPoint.LocY != end.LocY)
                {
                    var currentAdjacentPoints = new List<PathingPoint>();
                    foreach (var adjacentPoint in adjacentPoints)
                    {
                        var point = new PathingPoint
                            {
                                LocX = startingPoint.LocX + adjacentPoint.DeltaX,
                                LocY = startingPoint.LocY + adjacentPoint.DeltaY,
                                ParentX = startingPoint.LocX,
                                ParentY = startingPoint.LocY
                            };

                        if (adjacentPoint.DeltaX == 0 || adjacentPoint.DeltaY == 0)
                            point.GScore = 10;
                        else
                        {
                            point.GScore = 14;
                        }

                        point.HScore = 10 * (Math.Abs(end.LocX - point.LocX) + Math.Abs(end.LocY - point.LocY));

                        var matchedPoint = pathingMap.ClosedPoints.Any(x => x.LocX == point.LocX && x.LocY == point.LocY);
                        if (matchedPoint)
                            continue;

                        currentAdjacentPoints.Add(point);
                        var matchedOpen = pathingMap.OpenPoints.Where(x => x.LocX == point.LocX && x.LocY == point.LocY).ToList();
                        if (matchedOpen.Count > 0)
                        {
                            Console.WriteLine("Matched Open X: {0} Y: {1} F: {2} G: {3} H: {4}", point.LocX, point.LocY,
                                              point.FScore, point.GScore, point.HScore);
                            continue;
                        }

                        pathingMap.OpenPoints.Add(point);

                        Console.WriteLine("Open Adjacents X: {0} Y: {1} F: {2} G: {3} H: {4}", point.LocX, point.LocY,
                                          point.FScore, point.GScore, point.HScore);
                    }

                    startingPoint = currentAdjacentPoints.OrderBy(x => x.FScore).FirstOrDefault();
                    var otherOptions = currentAdjacentPoints.Where(x => x.FScore == startingPoint.FScore).OrderBy(y => y.FScore).ToList();
                    if (i > 0 && otherOptions.Count > i)
                        startingPoint = otherOptions[i];

                    Console.WriteLine("Chosen Point X: {0} Y: {1} F: {2} G: {3} H: {4}", startingPoint.LocX, startingPoint.LocY,
                                      startingPoint.FScore, startingPoint.GScore, startingPoint.HScore);
                    startingPoint.IsCurrent = true;
                    //Remove new current point from open points
                    var originalOpenPoint =
                        pathingMap.OpenPoints.Single(x => x.LocX == startingPoint.LocX && x.LocY == startingPoint.LocY);
                    pathingMap.OpenPoints.Remove(originalOpenPoint);
                    chosenPath.Add(startingPoint);
                    pathingMap.ClosedPoints.ForEach(x => x.IsCurrent = false);
                    pathingMap.ClosedPoints.Add(startingPoint);
                }

                foreach (var thingy in chosenPath)
                {
                    Console.WriteLine("Chosen Point X: {0} Y: {1} F: {2} G: {3} H: {4}", thingy.LocX, thingy.LocY,
                                     thingy.FScore, thingy.GScore, thingy.HScore);
                }
                if (lowestLength == 0 || lowestLength > chosenPath.Count)
                {
                    path = new Path();
                    foreach (var chosenPointInChosenPath in chosenPath)
                    {
                        path.Movements.Add(new Movement
                            {
                                NewX = chosenPointInChosenPath.LocX,
                                NewY = chosenPointInChosenPath.LocY
                            });
                    }

                    lowestLength = chosenPath.Count;
                }
            }

            return path;
        }

        private static PathingMap BuildPathingMap(Map map)
        {
            var pathingMap = new PathingMap();

            foreach (var worldObject in map.WorldObjects)
            {
                for (int i = 0; i < worldObject.Height; i++)
                {
                    for (int j = 0; j < worldObject.Width; j++)
                    {
                        var closedPoint = new PathingPoint();
                        closedPoint.LocY = worldObject.LocY + i;
                        closedPoint.LocX = worldObject.LocX + j;
                        pathingMap.ClosedPoints.Add(closedPoint);
                        Console.WriteLine("Closed Point X: {0} Y: {1}", closedPoint.LocX, closedPoint.LocY);
                    }
                }
            }
            return pathingMap;
        }
    }

    public class AdjectPoint
    {
        public AdjectPoint(int x, int y)
        {
            DeltaX = x;
            DeltaY = y;
        }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }
    }

    public class PathingPoint
    {
        public int LocX { get; set; }
        public int LocY { get; set; }
        public int ParentX { get; set; }
        public int ParentY { get; set; }
        public int FScore { get { return GScore + HScore; } }
        public int GScore { get; set; }
        public int HScore { get; set; }
        public bool IsCurrent { get; set; }
    }

    public class PathingMap
    {
        public PathingMap()
        {
            OpenPoints = new List<PathingPoint>();
            ClosedPoints = new List<PathingPoint>();
        }

        public List<PathingPoint> OpenPoints { get; set; }
        public List<PathingPoint> ClosedPoints { get; set; }
    }
}
