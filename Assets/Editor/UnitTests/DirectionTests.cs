using Map.Model;
using NUnit.Framework;

namespace Model
{
    public class DirectionTests
    {
        [Test]
        public void GetDirectionsExcludingShouldGiveSameValuesAsGetAllDirectionsWhenExclusionsAreNull()
        {
            var expectedDirections = Map.Model.Utils.GetAllDirections();
            var directions = Map.Model.Utils.GetAllDirectionsExcluding();

            foreach (var expectedDirection in expectedDirections)
            {
                Assert.Contains(expectedDirection, directions);
            }
        }

        [Test]
        public void GetAllDirectionsShouldReturnAllDirections()
        {
            Direction[] expectedDirections = {Direction.EAST, Direction.NORTH, Direction.WEST, Direction.SOUTH};
            var directions = Map.Model.Utils.GetAllDirections();

            foreach (var expectedDirection in expectedDirections)
            {
                Assert.Contains(expectedDirection, directions);
            }
        }
    }
}