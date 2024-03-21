using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class GameData
{

    private List<GuidObjectToken> _guidInGame = new List<GuidObjectToken>();

    public void LoadGUIDData()
    {
        for (int i = 0; i < _guidInGame.Count; i++)
        {
            _guidInGame[i].LoadGUIDData();
        }
    }


    public GameData()
    {
        foreach (KeyValuePair<string, Transform> item in ObjectRegister.Instance.ObjectDictionary)
        {
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
                }
            }

            //_guidInGame.Add(new GuidObjectToken(item));
        }
    }
}

[System.Serializable]
public abstract class GuidObjectToken
{
    protected string _guid;
    protected VectorToken _position;
    protected VectorToken _rotation;

    public virtual void LoadGUIDData()
    {
        Transform go = ObjectRegister.Instance.ReturnObject(_guid);
        go.position = _position.GetVector;
        go.rotation = Quaternion.Euler(_rotation.GetVector);
    }
/*
    public GuidObjectToken(KeyValuePair<string, Transform> go)
    {
        _guid = go.Key;
        _position = new VectorToken(go.Value.position);
        _rotation = new VectorToken(go.Value.rotation.eulerAngles);
    }
*/
}

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

    public override void LoadGUIDData()
    {
        base.LoadGUIDData();

        CharacterGUID go = ObjectRegister.Instance.ReturnObject(_guid).GetComponent<CharacterGUID>();

    }
}

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
