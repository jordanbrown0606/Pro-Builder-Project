using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the save and load.
/// Protip: This script is bad and should not handle that stuff.
/// Maybe you should use an input manager instead.
/// </summary>
public class Player : CharacterGUID
{
    /*
    public int maxHealth = 10;
    public Vector2Int damageRange;

    public UnityEvent<int> TakeDamage;

    private void Start()
    {
        _health = maxHealth;
        EventManager.OnUpdateHealthBar.Invoke(_health, maxHealth);
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
    */
    // Update is called once per frame
    void LateUpdate()
    {
        /*
        if(Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage?.Invoke(UnityEngine.Random.Range(damageRange.x, damageRange.y + 1));
        }
        */

        if (Input.GetKeyDown(KeyCode.S))
        {

            // Send a message to SaveLoad that will make a binary file of the passed name of this file.
            //Generate a new game data based on the current game state.
            // Save the game data in the generated file.
            SaveLoad.Save("Jordan", new GameData());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            // Send a request to SaveLoad to find a file of the passed name.
            // If the file is not found it will return null.
            GameData gd = SaveLoad.Load("Jordan");

            // The file was not found, therefore leave before we break things.
            if(gd == null)
            {
                return;
            }

            // Send a message to GameData to set the values of the stored GUID objects position, rotation, health, and mana.
            gd.LoadGUIDData();
            GetComponent<CharacterController>().enabled = false;
        }
    }
}
