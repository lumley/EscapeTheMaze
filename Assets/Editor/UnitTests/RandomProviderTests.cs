using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

public class RandomProviderTests {

    private const int SEED = 0;
    private int seedBefore;

    [SetUp]
    public void SetUp()
    {
        seedBefore = UnityEngine.Random.seed;
        UnityEngine.Random.seed = SEED;
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

        // First run we prove that the excluded value would have been selected
        UnityEngine.Random.seed = SEED;
        Assert.AreEqual(excludedValue, RandomProvider.GetRandomElementExcluding(elements, null));

        int[] excluded = { excludedValue };

        // We reset the seed and start again, excluding the value
        UnityEngine.Random.seed = SEED;
        for (int i=0; i< 1000; ++i)
        {
            int element = RandomProvider.GetRandomElementExcluding(elements, excluded);
            Assert.AreNotEqual(excludedValue, element);
        }
    }

    [Test]
    public void GetRandomElementShouldSelectARandomElementFromWithinTheGivenElementsWhenUsingAnArray()
    {
        int[] elements = { 0, 1, 2 };

        for(int i=0; i< 1000; ++i)
        {
            int element = RandomProvider.GetRandomElement(elements);
            Assert.IsTrue(System.Array.IndexOf(elements, element) >= 0);
        }
    }

    [Test]
    public void GetRandomElementShouldSelectARandomElementFromWithinTheGivenElementsWhenUsingAnCollection()
    {
        List<int> elements = new List<int>(new int[]{ 0, 1, 2});

        for (int i = 0; i < 1000; ++i)
        {
            int element = RandomProvider.GetRandomElement(elements);
            Assert.IsTrue(elements.Contains(element));
        }
    }

    [Test]
    public void GetRandomElementExcludingShouldGiveSameValuesAsGetRandomElementWhenExclusionsAreNull()
    {
        int[] elements = { 0, 1, 2, 3, 4 };
        UnityEngine.Random.seed = SEED;

        const int numValues = 1000;
        int[] expectedValues = new int[numValues];
        for (int i = 0; i < numValues; ++i)
        {
            int element = RandomProvider.GetRandomElement(elements);
            expectedValues[i] = element;
        }

        UnityEngine.Random.seed = SEED;
        for (int i = 0; i < numValues; ++i)
        {
            int element = RandomProvider.GetRandomElementExcluding(elements);
            Assert.AreEqual(expectedValues[i], element);
        }
    }


}
