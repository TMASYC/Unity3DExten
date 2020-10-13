using System.Collections.Generic;
using UnityEngine;


public static class ListExtensions
{
    private const int MAX_ITERATION_MULT = 100;

    public static T GetRandom<T>(this IList<T> list)
    {
        if (list.Count <= 0)
        {
            return default(T);
        }

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }

    // Be careful when using this function on lists with small contents.
    public static T GetRandomExcluding<T>(this IList<T> list, T exclude)
    {
        if (list.Count <= 1)
        {
            return default(T);
        }
        int maxIterations = list.Count * MAX_ITERATION_MULT;
        int whileCounter = 0;

        int excludeIndex = list.IndexOf(exclude);
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, list.Count);
            whileCounter++;

        } while (randomIndex == excludeIndex && whileCounter < maxIterations);

        if (whileCounter >= maxIterations)
        {
        }

        return list[randomIndex];
    }


    // Be careful when using this function on lists with small contents.
    public static T GetRandomExcluding<T>(this IList<T> list, List<T> exclude)
    {
        if (list.Count <= 1)
        {
            return default(T);
        }

        int maxIterations = list.Count * MAX_ITERATION_MULT;
        int whileCounter = 0;

        bool found;
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, list.Count);

            found = !exclude.Contains(list[randomIndex]);

        } while (!found && whileCounter < maxIterations);

        if (whileCounter >= maxIterations)
        {
            Debug.LogError("Exceeded maxIterationCount. Item returned is not unique");
        }
        return list[randomIndex];
    }

    public static T GetRandomWithWeights<T>(this IList<T> list, float[] weights)
    {
        Debug.Assert(list.Count == weights.Length, "Tried to get random list element with weights, but probability array did not match size of list");

        float totalWeight = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            totalWeight += weights[i];
        }

        float target = Random.Range(0f, totalWeight);
        float current = 0f;

        for (int i = 0; i < list.Count; i++)
        {
            current += weights[i];
            if (current >= target)
            {
                return list[i];
            }
        }
        return list[0];
    }

    // Be careful when using this function on lists with small contents.
    public static T GetRandomExcluding<T>(this IList<T> list, int excludeIndex)
    {
        if (list.Count <= 1)
        {
            Debug.LogError("List count was less than 1.");
            return default(T);
        }

        int maxIterations = list.Count * MAX_ITERATION_MULT;
        int whileCounter = 0;

        int randomIndex;
        if (excludeIndex >= list.Count)
        {
            Debug.LogError("excludeIndex is greater than the count of the list");
            return list[0];
        }

        do
        {
            randomIndex = Random.Range(0, list.Count);
        } while (randomIndex == excludeIndex && whileCounter < maxIterations);

        if (whileCounter >= maxIterations)
        {
            Debug.LogError("Exceeded maxIterationCount. Item returned is not unique");
        }

        return list[randomIndex];
    }

    public static T LastItem<T>(this IList<T> list)
    {
        if (list.Count <= 0)
        {
            Debug.LogError("list count is 0.");
            return default(T);
        }

        return list[list.Count - 1];
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        if (list.Count <= 0)
        {
            Debug.LogError("list remains unshuffled.");
        }
        else
        {
            int p = list.Count;
            for (int n = p - 1; n > 0; n--)
            {
                int r = Random.Range(0, n);
                T t = list[r];
                list[r] = list[n];
                list[n] = t;
            }
        }
    }

    //Combine one list of items with another
    public static IList<T> Concatenate<T>(this IList<T> list, IList<T> listToAdd)
    {
        if (list != null && listToAdd != null)
        {
            for (int i = 0; i < listToAdd.Count; i++)
            {
                list.Add(listToAdd[i]);
            }
        }
        return list;
    }

    //Same as Concat but the listToAdd will be cleared
    public static void ConcatAndClear<T>(this IList<T> list, IList<T> listToAdd)
    {
        if (list != null && listToAdd != null)
        {
            for (int i = 0; i < listToAdd.Count; i++)
            {
                list.Add(listToAdd[i]);
            }
            listToAdd.Clear();
        }
    }

    //Replace every item in the list with the supplied item
    public static void Fill<T>(this List<T> list, T itemToFill)
    {
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = itemToFill;
            }
        }
    }

    //Replace the item range in the list with the supplied item
    public static void Fill<T>(this IList<T> list, T itemToFill, int start, int end)
    {
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (i >= start && i <= end && start <= list.Count && end <= list.Count)
                {
                    list[i] = itemToFill;
                }
            }
        }
    }

    public static void RemoveDuplicates<T>(this IList<T> list)
    {
        List<T> newList = new List<T>();
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (newList.Contains(list[i]) == false)
                {
                    newList.Add(list[i]);
                }
            }
            list.Clear();
            list.Concatenate(newList);
        }
    }


    public static int Push<T>(this IList<T> list, T itemToPush)
    {
        if (list != null)
        {
            list.Add(itemToPush);
            return list.Count;
        }
        Debug.LogError("List was null, returning 0");
        return 0;
    }

    public static T Pop<T>(this List<T> list)
    {
        if (list != null)
        {
            T item;
            item = list.LastItem();
            list.Remove(item);
            return item;
        }
        return default(T);
    }

    public static List<T> Split<T>(this IList<T> list, int start, int end)
    {
        List<T> newList = new List<T>();

        if (list != null)
        {
            if (start <= list.Count && end <= list.Count)
            {
                for (int i = start; i < end; i++)
                {
                    newList.Add(list[i]);
                }
            }
            else
            {
                Debug.LogError("Start or end out of range of list.");
            }
            return newList;
        }
        return newList;
    }

    //add to the beginning of the list
    public static int Unshift<T>(this IList<T> list, T itemToUnshift)
    {
        if (list != null)
        {
            list.Insert(0, itemToUnshift);
            return list.Count;
        }
        Debug.LogError("List was null, returning 0");
        return 0;
    }

    //Given the index and the length of the list, should the index start from 0
    public static int WrapIndex<T>(this IList<T> list, int index)
    {
        int indexToReturn = 0;
        if (list != null)
        {
            if (index >= list.Count)
            {
                indexToReturn = 0;
            }
            else
            {
                indexToReturn = index;
            }
        }
        return indexToReturn;
    }

    //Given the index and the length of the list, clamp the index to the length - 1
    public static int ClampIndex<T>(this IList<T> list, int index)
    {
        if (list != null)
        {
            if (index >= list.Count)
            {
                return list.Count - 1;
            }
        }
        Debug.LogError("List was null, returning 0");
        return 0;
    }

    //Is the given index a valid element in the list
    public static bool IsValidIndex<T>(this IList<T> list, int index)
    {
        if (list != null)
        {
            return index < list.Count && index >= 0;
        }
        Debug.LogError("List was null, returning 0");
        return false;
    }

    //GameObject
    public static bool AreAllItemsValid<T>(this IList<GameObject> list)
    {
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                {
                    return false;
                }
            }
            return true;
        }
        Debug.LogError("List was null, returning false");
        return false;
    }

    public static void DestroyAllAndClear(this IList<GameObject> list)
    {
        if (list != null)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] != null)
                {
                    GameObject.Destroy(list[i]);
                }
                else
                {
                    Debug.LogError("Item: " + i.ToString() + " was null.");
                }
            }
            list.Clear();
        }
    }
}
