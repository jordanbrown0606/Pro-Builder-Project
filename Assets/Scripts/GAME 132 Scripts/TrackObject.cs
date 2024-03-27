using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
    
{
    // Start is called before the first frame update
    private void Start()
    {
        PointofInterestMarkerManager.instance?.AddPOIMarker(this);
    }

    private void OnDestroy()
    {
        PointofInterestMarkerManager.instance?.RemovePOIMarker(this);
    }
}