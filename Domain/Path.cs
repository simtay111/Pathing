using System.Collections.Generic;

namespace Domain
{
    public class Path
    {
        public Path()
        {
            Movements = new List<Movement>();
        }

        public List<Movement> Movements { get; set; } 
    }
}