using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOBJPOOL : MonoBehaviour
{
    private Rigidbody _rb;

    private int _curHealth;
    private int _maxHealth;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rb.velocity = Vector3.zero;
        _curHealth = _maxHealth;
    }
}
