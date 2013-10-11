using Domain;
using NUnit.Framework;
using Pathing;

namespace TestLibrary
{
    public class PathFinderTests
    {
        [Test]
        public void FirsTest()
        {
            for (int i = 0; i < 1; i++)
            {
                var start = new Human
                    {
                        LocX = 1,
                        LocY = 5
                    };
                var end = new Zombie
                    {
                        LocX = 5,
                        LocY = 5
                    };

                var wall = new WorldObject
                    {
                        Height = 7,
                        LocX = 3,
                        LocY = 0,
                        Width = 1
                    };

                var map = new Map();
                map.WorldObjects.Add(wall);

                map.LivingEntities.Add(start);
                map.LivingEntities.Add(end);

                var patherFinder = new PathFinder();
                var currentPath = patherFinder.CreatePath(map, start, end);

                var mapPrineter = new MapPrinter();
                mapPrineter.PrintMap(map, currentPath);
            }
        }
    }
}