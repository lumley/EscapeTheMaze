using UnityEngine;

public class RandomProvider : MonoBehaviour
{
    public int seed;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public T GetRandomElement<T>(T[] elements)
    {
        int index = Random.Range(0, elements.Length);
        return elements[index];
    }

    public T GetRandomElementExcluding<T>(T[] elements, params T[] exclusions)
    {
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
