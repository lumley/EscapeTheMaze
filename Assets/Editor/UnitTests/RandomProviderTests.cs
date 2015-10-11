using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class RandomProviderTests {

    private RandomProvider randomProvider;
    private int seedBefore;

    [SetUp]
    public void SetUp()
    {
        this.randomProvider = new RandomProvider();
        this.randomProvider.seed = 0;

        seedBefore = UnityEngine.Random.seed;
        UnityEngine.Random.seed = this.randomProvider.seed;
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Random.seed = seedBefore;
    }

    [Test]
    public void GetRandomElementExcludingShouldNotReturnAnElementExcluded()
    {
        int excludedValue = 0;
        int[] elements = { excludedValue, excludedValue+1 };
        int[] excluded = { excludedValue };

        for(int i=0; i< 1000; ++i)
        {
            int element = this.randomProvider.GetRandomElementExcluding(elements, excluded);
            Assert.AreNotEqual(excludedValue, element);
        }
    }

}
