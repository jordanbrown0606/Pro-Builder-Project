using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores all GUID objects in a dictionary
/// so that those objects are quick and easy to find
/// when the game saves or loads, we have instant access to those objects to get or set their values.
/// </summary>
public class ObjectRegister : MonoBehaviour
{

    // A public version of the only regiseter that exists. ALL objects can reference this register through the instance.
    public static ObjectRegister Instance;

    // Uses keys to find values. In this case, GUID objects will have a unique ID that can be associated with X transform.
    private Dictionary<string, Transform> _objectDictionary = new Dictionary<string, Transform>();

    public Dictionary<string, Transform> ObjectDictionary { get { return _objectDictionary; } }

    /// <summary>
    /// At the start of the game
    /// check if the instance is empty, if it is make us the instance.
    /// Otherwise, double-check if the non-empty instance is us.
    /// If it is us, then we are not needed and should be destroyed.
    /// </summary>
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

    /// <summary>
    /// Checks if the passed in key already exists.
    /// If so do not add a new version just set the value for that key.
    /// Otherwise, add a new key value pair of the passed in values.
    /// </summary>
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

    /// <summary>
    /// Checks for X key, if it is found then return the transform value.
    /// Otherwise, return null.
    /// </summary>
    public Transform ReturnObject(string key)
    {
        if (_objectDictionary.ContainsKey(key) == true)
        {
            return _objectDictionary[key];
        }

        return null;
    }

}