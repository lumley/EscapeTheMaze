using Combat;
using NUnit.Framework;

public class HealthTest
{
    private Health health;

    [SetUp]
    public void SetUp()
    {
        health = new Health();
    }

    [Test]
    public void CheckIfIsAliveWhenHealthLeft()
    {
        health.health = 0;
        Assert.IsTrue(health.IsAlive(), "Subject should have been alive");
    }
}