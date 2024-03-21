using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class Player : CharacterGUID
{
    public int maxHealth = 10;
    public Vector2Int damageRange;

    // public UnityEvent OnStartEvents;
    public UnityEvent<int> TakeDamage;

    private void Start()
    {
        _health = maxHealth;
        EventManager.OnUpdateHealthBar.Invoke(_health,maxHealth);
       //  OnStartEvents?.Invoke();
       //  TakeDamage?.Invoke(0);
    }

    private void OnEnable()
    {
        EventManager.OnGetHurt += GetKnockedBack;
    }

    private void OnDisable()
    {
        EventManager.OnGetHurt -= GetKnockedBack;
    }

    private void GetKnockedBack()
    {
        transform.position += new Vector3(0, 0, -1);
    }

    public void SendMessage()
    {
        Debug.Log("Invoked");
    }

    public void GetHurt(int amount)
    {
        _health -= amount;
        EventManager.OnUpdateHealthBar?.Invoke(_health, maxHealth);
        EventManager.OnGetHurt?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage?.Invoke(UnityEngine.Random.Range(damageRange.x, damageRange.y + 1));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveLoad.Save("Jordan", new GameData());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameData gd = SaveLoad.Load("Jordan");

            if(gd == null)
            {
                return;
            }
        }
    }
}
