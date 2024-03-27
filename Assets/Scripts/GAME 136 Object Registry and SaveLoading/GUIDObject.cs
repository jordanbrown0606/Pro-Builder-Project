using UnityEngine;

/// <summary>
/// Gives a GUID (A unique ID)
/// At start of game, register this object and its GUID.
/// </summary>

[ExecuteAlways]
public class GUIDObject : MonoBehaviour
{

    [SerializeField] private string _GUID;


    public string GetGUID {  get { return _GUID; } }

    /// <summary>
    /// Checks if a GUID exists, if not make one.
    /// </summary>
    private void OnEnable()
    {
        if(_GUID == "" || _GUID == string.Empty)
        {
            GenerateGUID();
        }
    }

    /// <summary>
    /// In editor DO NOT register the GUID
    /// otherwise, if in game, do register.
    /// </summary>
    private void Start()
    {

        if(Application.isPlaying == false)
        {
            return;
        }

        ObjectRegister.Instance?.Register(_GUID, transform);
    }

    public void GenerateGUID()
    {
        _GUID = System.Guid.NewGuid().ToString();
    }
}
