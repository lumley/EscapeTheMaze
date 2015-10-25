using UnityEngine;
using NUnit.Framework;

namespace Model
{
    public class BoardCreatorTests
    {

        private BoardCreator boardCreator;

        [SetUp]
        public void setUp()
        {
            boardCreator = new BoardCreator();
        }

        [Test]
        public void testGenerateCorridorsShouldGenerateOneLessCorridorThanRooms()
        {
            const int roomCount = 6;
            const int corridorsExpected = 5;

            BoardCreator.Corridor[] corridors = boardCreator.GenerateCorridors(roomCount);
            Assert.AreEqual(corridorsExpected, corridors.Length);
        }


    }
}

