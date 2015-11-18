using Map.Model;
using Map.Model.TileAttribute;
using NUnit.Framework;

namespace Model
{
    public class TileTests
    {
        private Tile tile;
        private Tile neighbour;

        [SetUp]
        public void SetUp()
        {
            this.tile = new Tile();
            this.neighbour = new Tile();
        }

        [Test]
        public void BindNeighboursShouldSetNeighboursInBothDirections()
        {
            Direction directionFromTileToNeighbour = Direction.NORTH;
            Direction directionFromNeighbourToTile = Direction.SOUTH;

            this.tile.BindNeighbours(neighbour, directionFromTileToNeighbour);

            Assert.AreEqual(this.neighbour, this.tile.GetNeighbour(directionFromTileToNeighbour));
            Assert.AreEqual(this.tile, this.neighbour.GetNeighbour(directionFromNeighbourToTile));
        }

        [Test]
        public void BindNeighboursShouldOverrideNeighbourWhenAlreadySet()
        {
            Tile newNeighbour = new Tile();
            Direction directionFromTileToNeighbour = Direction.NORTH;

            this.tile.BindNeighbours(neighbour, directionFromTileToNeighbour);

            Assert.AreEqual(this.neighbour, this.tile.GetNeighbour(directionFromTileToNeighbour));

            this.tile.BindNeighbours(newNeighbour, directionFromTileToNeighbour);
            Assert.AreEqual(newNeighbour, this.tile.GetNeighbour(directionFromTileToNeighbour));
        }

        [Test]
        public void GetNeighbourShouldReturnNullWhenNoNeighbourWasSetInGivenDirection()
        {
            Assert.IsNull(this.tile.GetNeighbour(Direction.NORTH));
        }

        [Test]
        public void GetNeighbourShouldReturnNeighbourWhenANeighbourWasSetInGivenDirection()
        {
            Direction directionFromTileToNeighbour = Direction.NORTH;

            this.tile.BindNeighbours(neighbour, directionFromTileToNeighbour);

            Assert.AreEqual(this.neighbour, this.tile.GetNeighbour(directionFromTileToNeighbour));
        }

        [Test]
        public void HasAttributeShouldReturnFalseWhenTileHasNotTheSpecifiedAttribute()
        {
            Assert.IsFalse(this.tile.HasAttribute(Type.SPAWNING_POINT));
        }

        [Test]
        public void HasAttributeShouldReturnTrueWhenTileHasGivenAttribute()
        {
            this.tile.AddAttribute(new SpawningPoint());
            Assert.IsTrue(this.tile.HasAttribute(Type.SPAWNING_POINT));
        }

        [Test]
        public void GetAtributeShouldReturnNullWhenTileHasNotTheSpecifiedAttribute()
        {
            Assert.IsNull(this.tile.GetAttribute<SpawningPoint>(Type.SPAWNING_POINT));
        }

        [Test]
        public void GetAtributeShouldReturnAttributeWhenTileContainsTheSpecifiedAttribute()
        {
            SpawningPoint expected = new SpawningPoint();
            this.tile.AddAttribute(expected);
            Assert.AreEqual(expected, this.tile.GetAttribute<SpawningPoint>(Type.SPAWNING_POINT));
        }
    }
}