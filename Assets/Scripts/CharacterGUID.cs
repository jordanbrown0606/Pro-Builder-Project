using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGUID : GUIDObject
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _mana;

    public int Health {  get { return _health; } set { _health = value; } }
    public int Mana { get { return _mana; } set { _mana = value; } }
}
