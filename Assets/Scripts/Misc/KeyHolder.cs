using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{

    private static readonly int MaxKeys = 32;

    public int KeyCount => keys.Count;

    private List<string> keys = new List<string>();
    
    public void AddKey(Key key)
    {
        if (HasKey(key.id))
        {
            return;
        }

        keys.Add(key.id);
    }

    public void RemoveKey(string keyId)
    {
        for (int i = 0; i < KeyCount; ++i)
        {
            if (keys[i] == keyId)
            {
                keys.RemoveAt(i);
                return;
            }
        }
    }

    public void RemoveKey(Key key)
    {
        keys.Remove(key.id);
    }

    public bool HasKey(string keyId)
    {
        for (int i = 0; i < KeyCount; ++i)
        {
            if (keys[i] == keyId)
            {
                return true;
            }
        }

        return false;
    }

    public bool HasKey(Key key)
    {
        return key != null && keys.Contains(key.id);
    }

}
