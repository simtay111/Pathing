using Domain.Enums;

namespace Domain
{
    public class Zombie : IHaveArea, IHaveLocation, ILivingEntity, ICanMove
    {
        public int LocX { get; set; } 
        public int LocY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public LivingEntityTypes GetEntityType()
        {
            return LivingEntityTypes.Zombie;
        }

        public int Rate { get; set; }
    }
}