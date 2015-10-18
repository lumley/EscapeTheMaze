using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace Model
{
    public class DirectionTests
    {

        [Test]
        public void GetDirectionsExcludingShouldGiveSameValuesAsGetAllDirectionsWhenExclusionsAreNull()
        {
            Direction[] expectedDirections = Utils.GetAllDirections();
            Direction[] directions = Utils.GetAllDirectionsExcluding();

            foreach (Direction expectedDirection in expectedDirections)
            {
                Assert.Contains(expectedDirection, directions);
            }
        }

        [Test]
        public void GetAllDirectionsShouldReturnAllDirections()
        {
            Direction[] expectedDirections = { Direction.EAST, Direction.NORTH, Direction.WEST, Direction.SOUTH };
            Direction[] directions = Utils.GetAllDirections();

            foreach (Direction expectedDirection in expectedDirections)
            {
                Assert.Contains(expectedDirection, directions);
            }
        }
    }
}
