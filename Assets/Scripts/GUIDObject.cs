using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GUIDObject : MonoBehaviour
{

    [SerializeField] private string _GUID;


    public string GetGUID {  get { return _GUID; } }

    private void OnEnable()
    {
        if(_GUID == "" || _GUID == string.Empty)
        {
            GenerateGUID();
        }
    }

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
