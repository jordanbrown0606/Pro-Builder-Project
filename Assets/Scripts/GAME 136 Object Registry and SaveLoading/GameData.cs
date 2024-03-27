using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Store all game state information.
/// Each GUID object is responsible for storing and loading its own data.
/// GameData is just a container for those bits of information.
/// </summary>

[System.Serializable]
public class GameData
{
    // Stores all the GUID objects in the game.
    // There are many types of GUID objects, but they all will be stored as a base type of GUID object token.
    // and therefore the inheriting types will always be GUID object tokens as well. (Despite having more features.)
    private List<GuidObjectToken> _guidInGame = new List<GuidObjectToken>();

    /// <summary>
    /// All GUID tokens are responsible for storing and loading their own data.
    /// Simply loop through all GUid tokens and request that they load their data as they require.
    /// </summary>
    public void LoadGUIDData()
    {
        for (int i = 0; i < _guidInGame.Count; i++)
        {
            _guidInGame[i].LoadGUIDData();
        }
    }


    public GameData()
    {
        // Get the object registry and loop through all of the contained GUIDs and then store them as GUID tokens based on their need.
        foreach (KeyValuePair<string, Transform> item in ObjectRegister.Instance.ObjectDictionary)
        {
            // Find out what kind of GUID object we are currently looking at. Then make a token of the needed type
            // Add that token to the GUID list to be saved.

            CharacterGUID guid = ObjectRegister.Instance.ReturnObject(item.Key).GetComponent<CharacterGUID>();

            if(guid != null)
            {
                _guidInGame.Add(new CharacterGUIDToken(guid));
                continue;
            }

            DooDadGUID doodad = ObjectRegister.Instance.ReturnObject(item.Key).GetComponent<DooDadGUID>();
            {
                if(doodad != null)
                {
                    _guidInGame.Add(new DoodadGuidToken(doodad));
                    continue;
                }
            }
        }
    }
}


/// <summary>
/// The base type of GUID token.
/// Other tokens will store unique information.
/// This base version cannot ever be used outside of being inherited.
/// As the base type, it stores the information that all GUID objects will use.
/// Such as position and rotation.
/// </summary>
[System.Serializable]
public abstract class GuidObjectToken
{
    protected string _guid;
    protected VectorToken _position;
    protected VectorToken _rotation;

    /// <summary>
    /// A fully complete method but is VIRTUAL so that more can be added if needed.
    /// Some GUID types such as CHARACTERS need to load more data such as health and mana and therefore will override this method
    /// but still call the base to handle the generic information.
    /// </summary>
    public virtual void LoadGUIDData()
    {
        Transform go = ObjectRegister.Instance.ReturnObject(_guid);
        go.position = _position.GetVector;
        go.rotation = Quaternion.Euler(_rotation.GetVector);
    }
}

/// <summary>
/// Stores base information for objects like barrels, boxes, chairs etc.
/// This token only exists because the base is ABSTRACT and therefore cannot be created.
/// </summary>
[System.Serializable]
public class DoodadGuidToken : GuidObjectToken
{

    public DoodadGuidToken(DooDadGUID go)
    {
        _guid = go.GetGUID;
        _position = new VectorToken(go.transform.position);
        _rotation = new VectorToken(go.transform.rotation.eulerAngles);
    }
}

/// <summary>
/// Stores the base GUID object information but also stores character stuff like health.
/// </summary>
[System.Serializable]
public class CharacterGUIDToken : GuidObjectToken
{
    private int _health;
    private int _mana;

    public CharacterGUIDToken(CharacterGUID go)
    {
        _guid = go.GetGUID;
        _position = new VectorToken(go.transform.position);
        _rotation = new VectorToken(go.transform.rotation.eulerAngles);
        _health = go.Health;
        _mana = go.Mana;
    }

    /// <summary>
    /// Override the original to still do the base stuff (load position etc),
    /// but then also do the character unique things like load health.
    /// </summary>
    public override void LoadGUIDData()
    {
        base.LoadGUIDData();

        CharacterGUID go = ObjectRegister.Instance.ReturnObject(_guid).GetComponent<CharacterGUID>();
        go.Health = _health;
        go.Mana = _mana;

    }
}

/// <summary>
/// Vectors cannot be saved and loaded.
/// This exists to store the floats of the vector in a saveable and loadable way.
/// </summary>
[System.Serializable]
public class VectorToken
{
    private float _x;
    private float _y;
    private float _z;

    public Vector3 GetVector { get { return new Vector3(_x, _y, _z); } }

        public VectorToken(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    public VectorToken(Vector3 v)
    {
        _x=v.x;
        _y=v.y;
        _z=v.z;
    }
}
