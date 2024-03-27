using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static Action OnGetHurt;
    public static Action<int, int> OnUpdateHealthBar;
}
