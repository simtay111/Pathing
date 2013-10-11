using System.Collections.Generic;

namespace Domain
{
    public class Map : IHaveArea
    {
        public Map()
        {
            WorldObjects = new List<WorldObject>();
            LivingEntities = new List<ILivingEntity>();
            Width = 10;
            Height = 10;
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public List<WorldObject> WorldObjects { get; set; }

        public List<ILivingEntity> LivingEntities { get; set; }
    }
}
