using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code based on https://www.codegrepper.com/code-examples/csharp/shuffle+array+c%23
public class Shuffle<T>
{
    public static T[] ShuffleTheList(T[] inputArray)
    {
        for (int i = 0; i < inputArray.Length; i++)
        {
            int randomIndex = Random.Range(0, inputArray.Length);
            {
                T temp;
                temp = inputArray[i];
                inputArray[i] = inputArray[randomIndex];
                inputArray[randomIndex] = temp;
            }
        }
        return inputArray;
    }
}