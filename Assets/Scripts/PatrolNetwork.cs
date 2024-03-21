using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]

public class PatrolNetwork : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Color _gizmoColours;

    private void OnEnable()
    {
        if(_waypoints != null || _waypoints.Length > 0)
        {
            return;
        }

        Transform[] foundObjects = GetComponentsInChildren<Transform>();
        _waypoints = new Transform[foundObjects.Length - 1];

        for (int i = 0; i < foundObjects.Length; i++)
        {
            if (foundObjects[i] == transform)
            {
                continue;
            }

            _waypoints[i-1] = foundObjects[i];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColours;

        for (int i = 0; i < _waypoints.Length; i++)
        {
            Gizmos.DrawSphere(_waypoints[i].position, 1f);

            if (i < _waypoints.Length - 1)
            {
                Gizmos.DrawLine(_waypoints[i].position, _waypoints[i + 1].position);
            }
            else
            {
                Gizmos.DrawLine(_waypoints[i].position, _waypoints[0].position);
            }
        }
    }
}
