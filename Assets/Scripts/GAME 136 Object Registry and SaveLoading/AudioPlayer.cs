using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnGetHurt += PlayerHurtSound;
    }

    private void OnDisable()
    {
        EventManager.OnGetHurt -= PlayerHurtSound;
    }

    private void PlayerHurtSound()
    {
        Debug.Log("OOF");
    }
}
