using Domain.Factories;
using NUnit.Framework;

namespace TestLibrary
{
    [TestFixture]
    public class ZombieFactoryTests
    {
        private ZombieFactory _zombieFactory;

        [SetUp]
        public void SetUp()
        {
            _zombieFactory = new ZombieFactory();
        }

        [Test]
        public void CanCreateZombies()
        {
            var zombie = _zombieFactory.Create(50, 100);

            Assert.That(zombie.Width, Is.EqualTo(50));
            Assert.That(zombie.Height, Is.EqualTo(100));
        }
         
    }
}