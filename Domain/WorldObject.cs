namespace Domain
{
    public class WorldObject : IHaveLocation, IHaveArea
    {
        public int LocX { get; set; } 
        public int LocY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}