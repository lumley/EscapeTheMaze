using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class TileTests
{

    private Model.Tile tile;
    private Model.Tile neighbour;

    [SetUp]
    public void SetUp()
    {
        this.tile = new Model.Tile();
        this.neighbour = new Model.Tile();
    }

    [Test]
    public void BindNeighboursShouldSetNeighboursInBothDirections()
    {
        Model.Direction directionFromTileToNeighbour = Model.Direction.NORTH;
        Model.Direction directionFromNeighbourToTile = Model.Direction.SOUTH;

        this.tile.BindNeighbours(neighbour, directionFromTileToNeighbour);

        Assert.AreEqual(this.neighbour, this.tile.GetNeighbour(directionFromTileToNeighbour));
        Assert.AreEqual(this.tile, this.neighbour.GetNeighbour(directionFromNeighbourToTile));
    }

    [Test]
    public void BindNeighboursShouldOverrideNeighbourWhenAlreadySet()
    {
        Model.Tile newNeighbour = new Model.Tile();
        Model.Direction directionFromTileToNeighbour = Model.Direction.NORTH;

        this.tile.BindNeighbours(neighbour, directionFromTileToNeighbour);

        Assert.AreEqual(this.neighbour, this.tile.GetNeighbour(directionFromTileToNeighbour));

        this.tile.BindNeighbours(newNeighbour, directionFromTileToNeighbour);
        Assert.AreEqual(newNeighbour, this.tile.GetNeighbour(directionFromTileToNeighbour));
    }

    [Test]
    public void GetNeighbourShouldReturnNullWhenNoNeighbourWasSetInGivenDirection()
    {
        Assert.IsNull(this.tile.GetNeighbour(Model.Direction.NORTH));
    }

    [Test]
    public void GetNeighbourShouldReturnNeighbourWhenANeighbourWasSetInGivenDirection()
    {
        Model.Tile newNeighbour = new Model.Tile();
        Model.Direction directionFromTileToNeighbour = Model.Direction.NORTH;

        this.tile.BindNeighbours(neighbour, directionFromTileToNeighbour);

        Assert.AreEqual(this.neighbour, this.tile.GetNeighbour(directionFromTileToNeighbour));
    }

    [Test]
    public void HasAttributeShouldReturnFalseWhenTileHasNotTheSpecifiedAttribute()
    {
        Assert.IsFalse(this.tile.HasAttribute(Model.TileAttribute.Type.SPAWNING_POINT));
    }

    [Test]
    public void HasAttributeShouldReturnTrueWhenTileHasGivenAttribute()
    {
        this.tile.AddAttribute(new Model.TileAttribute.SpawningPoint());
        Assert.IsTrue(this.tile.HasAttribute(Model.TileAttribute.Type.SPAWNING_POINT));
    }

    [Test]
    public void GetAtributeShouldReturnNullWhenTileHasNotTheSpecifiedAttribute()
    {
        Assert.IsNull(this.tile.GetAttribute<Model.TileAttribute.SpawningPoint>(Model.TileAttribute.Type.SPAWNING_POINT));
    }

    [Test]
    public void GetAtributeShouldReturnAttributeWhenTileContainsTheSpecifiedAttribute()
    {
        Model.TileAttribute.SpawningPoint expected = new Model.TileAttribute.SpawningPoint();
        this.tile.AddAttribute(expected);
        Assert.AreEqual(expected, this.tile.GetAttribute<Model.TileAttribute.SpawningPoint>(Model.TileAttribute.Type.SPAWNING_POINT));
    }
}