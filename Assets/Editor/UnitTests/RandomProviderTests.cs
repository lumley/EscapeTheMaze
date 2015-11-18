using UnityEngine;
using System.Collections.Generic;
using Commons;
using NUnit.Framework;

public class RandomProviderTests {

    private const int SEED = 0;
    private int seedBefore;

    [SetUp]
    public void SetUp()
    {
        seedBefore = Random.seed;
        Random.seed = SEED;
    }

    [TearDown]
    public void TearDown()
    {
        Random.seed = seedBefore;
    }

    [Test]
    public void GetRandomElementExcludingArrayShouldNotReturnAnElementExcluded()
    {
        int excludedValue = 0;
        int[] elements = { excludedValue, excludedValue+1 };

        // First run we prove that the excluded value would have been selected
        Random.seed = SEED;
        Assert.AreEqual(excludedValue, RandomProvider.GetRandomElementExcluding(elements, null));

        int[] excluded = { excludedValue };

        // We reset the seed and start again, excluding the value
        Random.seed = SEED;
        for (int i=0; i< 1000; ++i)
        {
            int element = RandomProvider.GetRandomElementExcluding(elements, excluded);
            Assert.AreNotEqual(excludedValue, element);
        }
    }

    [Test]
    public void GetRandomElementExcludingCollectionShouldNotReturnAnElementExcluded()
    {
        KeyValuePair<string, int> excludedValue = new KeyValuePair<string, int>("hi2", 2);
        Dictionary<string, int> elements = new Dictionary<string, int>(5);
        elements.Add("hi1", 1);
        elements.Add("hi2", 2);
        elements.Add("hi3", 3);
        elements.Add("hi4", 4);
        elements.Add("hi5", 5);

        // First run we prove that the excluded value would have been selected
        Random.seed = SEED;
        Assert.AreEqual(excludedValue, RandomProvider.GetRandomElementExcluding(elements, null));

        KeyValuePair<string, int>[] excluded = { excludedValue };

        // We reset the seed and start again, excluding the value
        Random.seed = SEED;
        for (int i = 0; i < 1000; ++i)
        {
            KeyValuePair<string, int> element = RandomProvider.GetRandomElementExcluding(elements, excluded);
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
    public void GetRandomElementExcludingArrayShouldGiveSameValuesAsGetRandomElementWhenExclusionsAreNull()
    {
        int[] elements = { 0, 1, 2, 3, 4 };
        Random.seed = SEED;

        const int numValues = 1000;
        int[] expectedValues = new int[numValues];
        for (int i = 0; i < numValues; ++i)
        {
            int element = RandomProvider.GetRandomElement(elements);
            expectedValues[i] = element;
        }

        Random.seed = SEED;
        for (int i = 0; i < numValues; ++i)
        {
            int element = RandomProvider.GetRandomElementExcluding(elements);
            Assert.AreEqual(expectedValues[i], element);
        }
    }

    [Test]
    public void GetRandomElementExcludingCollectionShouldGiveSameValuesAsGetRandomElementWhenExclusionsAreNull()
    {
        Dictionary<string, int> elements = new Dictionary<string, int>(5);
        elements.Add("hi1", 1);
        elements.Add("hi2", 2);
        elements.Add("hi3", 3);
        elements.Add("hi4", 4);
        elements.Add("hi5", 5);

        Random.seed = SEED;

        const int numValues = 1000;
        KeyValuePair<string, int>[] expectedValues = new KeyValuePair<string, int>[numValues];
        for (int i = 0; i < numValues; ++i)
        {
            KeyValuePair<string, int> element = RandomProvider.GetRandomElement(elements);
            expectedValues[i] = element;
        }

        Random.seed = SEED;
        for (int i = 0; i < numValues; ++i)
        {
            KeyValuePair<string, int> element = RandomProvider.GetRandomElementExcluding(elements);
            Assert.AreEqual(expectedValues[i], element);
        }
    }


}
