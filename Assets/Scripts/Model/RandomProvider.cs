using UnityEngine;

public class RandomProvider : MonoBehaviour
{
    public int seed;

    // Use this for initialization
    void Start() {
        // We don't need to call updates on this object, it should probably not even be a mono behavior at all
        this.enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public static T GetRandomElement<T>(T[] elements)
    {
        int index = Random.Range(0, elements.Length);
        return elements[index];
    }
    
    public static T GetRandomElement<T>(System.Collections.Generic.ICollection<T> elements)
    {
        int index = Random.Range(0, elements.Count);
        foreach (T item in elements)
        {
            if (index-- <= 0){
                return item;
            }
        }
        
        throw new System.ArgumentException("Collection cannot be empty");
    }

    public static T GetRandomElementExcluding<T>(T[] elements, params T[] exclusions)
    {
        if (exclusions == null)
        {
            return GetRandomElement(elements);
        }

        int index = Random.Range(0, elements.Length - exclusions.Length);

        T element;
        int i = 0;
        do
        {
            element = elements[i];
            if (System.Array.IndexOf<T>(exclusions, element) < 0)
            {
                --index;
            }
        } while (++i < elements.Length && index >= 0);

        return element;
    }
}
