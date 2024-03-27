using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInterface : MonoBehaviour, IDamagable
{
    public void TakeDamage(int amount)
    {
        Debug.Log("Dude, that hurt!");
    }
}
