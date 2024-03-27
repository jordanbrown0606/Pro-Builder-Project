using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField, Min(0f)] private float _radius;
    [SerializeField, Min(0f)] private float _rayDistance;
    [SerializeField] private uint _amount;
    [SerializeField] private DirectionType _directionType;
    [SerializeField] private Shape _shape;
    [SerializeField] private Vector2 _rectangleSize;
    [SerializeField] private float _stepSize;

    [SerializeField] private Color _gizmoColour;

    private const float TAU = Mathf.PI * 2f;


    // Start is called before the first frame update
    public void Generate()
    {
        switch (_shape)
        {
            case Shape.Circle:
                SpawnInCircle();
                break;
            case Shape.Rectangle:
                SpawnInRectangle();
                break;
            default:
                break;
        }
    }

    private void SpawnInRectangle()
    {
        Vector3 centre = transform.position;
        float perimeter = (_rectangleSize.x * 2) + (_rectangleSize.y * 2);
        float spacePerSpawn = perimeter / _amount;

        Vector3 topLeft = centre + new Vector3(-_rectangleSize.x * 0.5f, 0f, _rectangleSize.y * 0.5f);
        Vector3 bottomLeft = centre + new Vector3(-_rectangleSize.x * 0.5f, 0f, -_rectangleSize.y * 0.5f);
        Vector3 topRight = centre + new Vector3(_rectangleSize.x * 0.5f, 0f, _rectangleSize.y * 0.5f);
        Vector3 bottomRight = centre + new Vector3(_rectangleSize.x * 0.5f, 0f, -_rectangleSize.y * 0.5f);

        float spacePassedBetweenSpawns = 0;
        
        //Top Line
        float totalLineLength = Mathf.Abs(topRight.x - topLeft.x);

        for (float i = 0; i < totalLineLength; i += _stepSize)
        {
            spacePassedBetweenSpawns += _stepSize;
            if(spacePassedBetweenSpawns >= spacePerSpawn)
            {
                spacePassedBetweenSpawns = 0;
                Vector3 spawnPosition = topLeft + new Vector3(i, 0f, 0f);
                SpawnObject(spawnPosition, Vector3.down, centre);
            }
        }

        //Right Line
        totalLineLength = Mathf.Abs(topRight.z - bottomRight.z);

        for (float i = 0; i < totalLineLength; i += _stepSize)
        {
            spacePassedBetweenSpawns += _stepSize;
            if (spacePassedBetweenSpawns >= spacePerSpawn)
            {
                spacePassedBetweenSpawns = 0;
                Vector3 spawnPosition = topRight + new Vector3(0f, 0f, -i);
                SpawnObject(spawnPosition, Vector3.down, centre);
            }
        }

        //Bottom Line
        totalLineLength = Mathf.Abs(bottomLeft.x - bottomRight.x);

        for (float i = 0; i < totalLineLength; i += _stepSize)
        {
            spacePassedBetweenSpawns += _stepSize;
            if (spacePassedBetweenSpawns >= spacePerSpawn)
            {
                spacePassedBetweenSpawns = 0;
                Vector3 spawnPosition = bottomRight + new Vector3(-i, 0f, 0f);
                SpawnObject(spawnPosition, Vector3.down, centre);
            }
        }

        //Left Line
        totalLineLength = Mathf.Abs(bottomLeft.z - topLeft.z);

        for (float i = 0; i < totalLineLength; i += _stepSize)
        {
            spacePassedBetweenSpawns += _stepSize;
            if (spacePassedBetweenSpawns >= spacePerSpawn)
            {
                spacePassedBetweenSpawns = 0;
                Vector3 spawnPosition = bottomLeft + new Vector3(0f, 0f, i);
                SpawnObject(spawnPosition, Vector3.down, centre);
            }
        }
    }

    private void SpawnInCircle()
    {
        Vector3 centre = transform.position;

        for (int i = 0; i < _amount; i++)
        {
            float radians = TAU / _amount * i;

            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);

            Vector3 spawnDirection = new Vector3(cos, 0f, sin);
            Vector3 rayPosition = centre + spawnDirection * _radius;
            SpawnObject(rayPosition, spawnDirection, centre);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColour;
        Vector3 centre = transform.position;

        switch (_shape)
        {
            case Shape.Circle:
                for (int i = 0; i < _amount; i++)
                {
                    float radians = TAU / _amount * i;
                    float sin = Mathf.Sin(radians);
                    float cos = Mathf.Cos(radians);

                    Vector3 spawnDirection = new Vector3(cos, 0f, sin);
                    Vector3 rayPosition = centre + spawnDirection * _radius;

                    RaycastHit hit;

                    if (Physics.Raycast(rayPosition, Vector3.down, out hit, _rayDistance))
                    {
                        Vector3 lookRotation = new Vector3(centre.x, hit.point.y, centre.z) - hit.point;

                        if (_directionType == DirectionType.DirectionNormal)
                        {
                            Vector3 yAxis = hit.normal;
                            Vector3 xAxis = Vector3.Cross(yAxis, lookRotation).normalized;
                            Vector3 zAxis = Vector3.Cross(xAxis, yAxis);

                            Gizmos.DrawMesh(_objectToSpawn.GetComponent<MeshFilter>().sharedMesh, hit.point, Quaternion.LookRotation(zAxis, yAxis));
                        }
                        else
                        {
                            Gizmos.DrawMesh(_objectToSpawn.GetComponent<MeshFilter>().sharedMesh, hit.point, Quaternion.LookRotation(lookRotation));
                        }
                    }
                }
                break;
            case Shape.Rectangle:
                float perimeter = (_rectangleSize.x * 2) + (_rectangleSize.y * 2);
                float spacePerSpawn = perimeter / _amount;

                Vector3 topLeft = centre + new Vector3(-_rectangleSize.x * 0.5f, 0f, _rectangleSize.y * 0.5f);
                Vector3 bottomLeft = centre + new Vector3(-_rectangleSize.x * 0.5f, 0f, -_rectangleSize.y * 0.5f);
                Vector3 topRight = centre + new Vector3(_rectangleSize.x * 0.5f, 0f, _rectangleSize.y * 0.5f);
                Vector3 bottomRight = centre + new Vector3(_rectangleSize.x * 0.5f, 0f, -_rectangleSize.y * 0.5f);

                float spacePassedBetweenSpawns = 0;

                //Top Line
                float totalLineLength = Mathf.Abs(topRight.x - topLeft.x);

                for (float i = 0; i < totalLineLength; i += _stepSize)
                {
                    spacePassedBetweenSpawns += _stepSize;
                    if (spacePassedBetweenSpawns >= spacePerSpawn)
                    {
                        spacePassedBetweenSpawns = 0;
                        Vector3 spawnPosition = topLeft + new Vector3(i, 0f, 0f);

                        RaycastHit hit;

                        if (Physics.Raycast(spawnPosition, Vector3.down, out hit, _rayDistance))
                        {
                            Vector3 lookRotation = new Vector3(centre.x, hit.point.y, centre.z) - hit.point;

                            if (_directionType == DirectionType.DirectionNormal)
                            {
                                Vector3 yAxis = hit.normal;
                                Vector3 xAxis = Vector3.Cross(yAxis, lookRotation).normalized;
                                Vector3 zAxis = Vector3.Cross(xAxis, yAxis);

                                Gizmos.DrawMesh(_objectToSpawn.GetComponent<MeshFilter>().sharedMesh, hit.point, Quaternion.LookRotation(zAxis, yAxis));
                            }
                            else
                            {
                                Gizmos.DrawMesh(_objectToSpawn.GetComponent<MeshFilter>().sharedMesh, hit.point, Quaternion.LookRotation(lookRotation));
                            }
                        }
                    }
                }

                //Right Line
                totalLineLength = Mathf.Abs(topRight.z - bottomRight.z);

                for (float i = 0; i < totalLineLength; i += _stepSize)
                {
                    spacePassedBetweenSpawns += _stepSize;
                    if (spacePassedBetweenSpawns >= spacePerSpawn)
                    {
                        spacePassedBetweenSpawns = 0;
                        Vector3 spawnPosition = topRight + new Vector3(0f, 0f, -i);

                        RaycastHit hit;

                        if (Physics.Raycast(spawnPosition, Vector3.down, out hit, _rayDistance))
                        {
                            Vector3 lookRotation = new Vector3(centre.x, hit.point.y, centre.z) - hit.point;

                            if (_directionType == DirectionType.DirectionNormal)
                            {
                                Vector3 yAxis = hit.normal;
                                Vector3 xAxis = Vector3.Cross(yAxis, lookRotation).normalized;
                                Vector3 zAxis = Vector3.Cross(xAxis, yAxis);

                                Gizmos.DrawMesh(_objectToSpawn.GetComponent<MeshFilter>().sharedMesh, hit.point, Quaternion.LookRotation(zAxis, yAxis));
                            }
                            else
                            {
                                Gizmos.DrawMesh(_objectToSpawn.GetComponent<MeshFilter>().sharedMesh, hit.point, Quaternion.LookRotation(lookRotation));
                            }
                        }
                    }
                }

                //Bottom Line
                totalLineLength = Mathf.Abs(bottomLeft.x - bottomRight.x);

                for (float i = 0; i < totalLineLength; i += _stepSize)
                {
                    spacePassedBetweenSpawns += _stepSize;
                    if (spacePassedBetweenSpawns >= spacePerSpawn)
                    {
                        spacePassedBetweenSpawns = 0;
                        Vector3 spawnPosition = bottomRight + new Vector3(-i, 0f, 0f);

                        RaycastHit hit;

                        if (Physics.Raycast(spawnPosition, Vector3.down, out hit, _rayDistance))
                        {
                            Vector3 lookRotation = new Vector3(centre.x, hit.point.y, centre.z) - hit.point;

                            if (_directionType == DirectionType.DirectionNormal)
                            {
                                Vector3 yAxis = hit.normal;
                                Vector3 xAxis = Vector3.Cross(yAxis, lookRotation).normalized;
                                Vector3 zAxis = Vector3.Cross(xAxis, yAxis);

                                Gizmos.DrawMesh(_objectToSpawn.GetComponent<MeshFilter>().sharedMesh, hit.point, Quaternion.LookRotation(zAxis, yAxis));
                            }
                            else
                            {
                                Gizmos.DrawMesh(_objectToSpawn.GetComponent<MeshFilter>().sharedMesh, hit.point, Quaternion.LookRotation(lookRotation));
                            }
                        }
                    }
                }

                //Left Line
                totalLineLength = Mathf.Abs(bottomLeft.z - topLeft.z);

                for (float i = 0; i < totalLineLength; i += _stepSize)
                {
                    spacePassedBetweenSpawns += _stepSize;
                    if (spacePassedBetweenSpawns >= spacePerSpawn)
                    {
                        spacePassedBetweenSpawns = 0;
                        Vector3 spawnPosition = bottomLeft + new Vector3(0f, 0f, i);

                        RaycastHit hit;

                        if (Physics.Raycast(spawnPosition, Vector3.down, out hit, _rayDistance))
                        {
                            Vector3 lookRotation = new Vector3(centre.x, hit.point.y, centre.z) - hit.point;

                            if (_directionType == DirectionType.DirectionNormal)
                            {
                                Vector3 yAxis = hit.normal;
                                Vector3 xAxis = Vector3.Cross(yAxis, lookRotation).normalized;
                                Vector3 zAxis = Vector3.Cross(xAxis, yAxis);

                                Gizmos.DrawMesh(_objectToSpawn.GetComponent<MeshFilter>().sharedMesh, hit.point, Quaternion.LookRotation(zAxis, yAxis));
                            }
                            else
                            {
                                Gizmos.DrawMesh(_objectToSpawn.GetComponent<MeshFilter>().sharedMesh, hit.point, Quaternion.LookRotation(lookRotation));
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    private void SpawnObject(Vector3 rayPosition, Vector3 rayDirection, Vector3 centre)
    {
        RaycastHit hit;

        if (Physics.Raycast(rayPosition, Vector3.down, out hit, _rayDistance))
        {
            Vector3 lookRotation = new Vector3(centre.x, hit.point.y, centre.z) - hit.point;
            GameObject go = Instantiate(_objectToSpawn, hit.point, Quaternion.LookRotation(lookRotation));
            Undo.RegisterCreatedObjectUndo(go, "spawn Obects");

            if (_directionType == DirectionType.DirectionNormal)
            {

                Vector3 yAxis = hit.normal;
                Vector3 xAxis = Vector3.Cross(yAxis, lookRotation).normalized;
                Vector3 zAxis = Vector3.Cross(xAxis, yAxis);
                go.transform.rotation = Quaternion.LookRotation(zAxis, yAxis);
            }
        }
    }

    private enum Shape
    {
        Circle,
        Rectangle
    }

    private enum DirectionType
    {
        Flat,
        DirectionNormal
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ObjectSpawner))]
public class ObjectSpawnerEditor : Editor
{
    public ObjectSpawner _curSpawner;

    private void OnEnable()
    {
        _curSpawner = target as ObjectSpawner;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Generate"))
        {
            _curSpawner.Generate();
        }
    }
}
#endif