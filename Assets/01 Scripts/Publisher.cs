using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Publisher : MonoBehaviour
{
    static Dictionary<string, List<Action<object[]>>> Listeners
         = new Dictionary<string, List<Action<object[]>>>();

    public static void AddListeners(string name, Action<object[]> callback)
    {
        if (!Listeners.ContainsKey(name))
        {
            Listeners.Add(name, new List<Action<object[]>>());
        }

        Listeners[name].Add(callback);
    }

    public static void RemoveListeners(string name, Action<object[]> callback)
    {
        if (!Listeners.ContainsKey(name))
        {
            return;
        }

        Listeners[name].Remove(callback);
    }

    public static void Notify(string name, params object[] datas)
    {
        if (!Listeners.ContainsKey(name))
        {
            return;
        }

        foreach (var action in Listeners[name])
        {
            try
            {
                action?.Invoke(datas);
            }
            catch (Exception e)
            {
                Debug.LogError("Error on invoke:" + e);
            }
        }
    }
}
