using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Chopsticks.Framework
{
    [TestFixture]
    class MapTests
    {
        [Test]
        public void CanInsertItemToScene()
        {
            var map = new Map();

            var transformation = new Transformation(500, 500);

            map.Insert("grass", transformation);

            Assert.AreEqual(map.Scene["grass"].First().Translation, transformation.Translation);
        }

        [Test]
        public void CanInsertCollectionOfItemsToScene()
        {
            var map = new Map();
            var transformation = new Transformation(500, 500);
            var transformation2 = new Transformation(100, 100);

            map.Insert("grass", new List<Transformation> { transformation, transformation2 });

            Assert.AreEqual(map.Scene["grass"].Count, 2);
        }
    }
}
