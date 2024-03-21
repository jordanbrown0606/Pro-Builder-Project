using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRegister : MonoBehaviour
{
    public static ObjectRegister Instance;

    private Dictionary<string, Transform> _objectDictionary = new Dictionary<string, Transform>();

    public Dictionary<string, Transform> ObjectDictionary { get { return _objectDictionary; } }

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
    }

    public void Register(string key, Transform value)
    {
        if (_objectDictionary.ContainsKey(key) == true)
        {
            _objectDictionary[key] = value;
        }
        else
        {
            _objectDictionary.Add(key, value);
        }
    }

    public Transform ReturnObject(string key)
    {
        if (_objectDictionary.ContainsKey(key) == true)
        {
            return _objectDictionary[key];
        }

        return null;
    }

}