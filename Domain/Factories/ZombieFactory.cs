namespace Domain.Factories
{
    public class ZombieFactory
    {
        public Zombie Create(int width, int height)
        {
            return new Zombie
                {
                    Height = height,
                    Width = width
                };
        }
    }
}