using Domain.Enums;

namespace Domain
{
    public class Human : IHaveArea, IHaveLocation, ILivingEntity, ICanMove 
    {
        public int LocX { get; set; } 
        public int LocY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public LivingEntityTypes GetEntityType()
        {
            return LivingEntityTypes.Human;
        }

        public int Rate { get; set; }
    }
}