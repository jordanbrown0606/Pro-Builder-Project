using UnityEngine;

/// <summary>
/// All characters have health and mana.
/// Is a type of GUIDObject so it has a GUID with which it can be saved and loaded.
/// </summary>
public class CharacterGUID : GUIDObject
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _mana;

    public int Health {  get { return _health; } set { _health = value; } }
    public int Mana { get { return _mana; } set { _mana = value; } }
}
