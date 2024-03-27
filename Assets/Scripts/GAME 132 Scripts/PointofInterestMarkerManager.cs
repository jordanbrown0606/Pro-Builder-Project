using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointofInterestMarkerManager : MonoBehaviour
{
    public static PointofInterestMarkerManager instance;

    [SerializeField] private RectTransform _poiContainer;
    [SerializeField] private RectTransform _poiMarkerPrefab;

    private Dictionary<TrackObject, RectTransform> _pois = new Dictionary<TrackObject, RectTransform>();

    private Camera _mainCamera;
    private Canvas _parentCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        _mainCamera = Camera.main;
        _parentCanvas = GetComponentInParent<Canvas>();
    }

    private void LateUpdate()
    {
        foreach (KeyValuePair<TrackObject, RectTransform> item in _pois)
        {
            TrackObject target = item.Key;
            RectTransform poi = item.Value;

            if (target == null)
            {
                continue;
            }

            poi.anchoredPosition = GetCanvasPositionFromTarget(target);
        }
    }

    public void AddPOIMarker(TrackObject newObject)
    {
        if (_pois.ContainsKey(newObject) == true)
        {
            return;
        }

        RectTransform newPOI = Instantiate(_poiMarkerPrefab);

        newPOI.SetParent(_poiContainer, false);
        newPOI.pivot = new Vector2(0.5f, 0.5f);
        newPOI.anchorMin = Vector2.zero;
        newPOI.anchorMax = Vector2.zero;

        _pois[newObject] = newPOI;
        newPOI.anchoredPosition = GetCanvasPositionFromTarget(newObject);
    }

    public void RemovePOIMarker(TrackObject objectToRemove)
    {
        if ( _pois.ContainsKey(objectToRemove) == true)
        {
            if (_pois[objectToRemove] != null)
            {
                Destroy(_pois[objectToRemove].gameObject);
            }

            _pois.Remove(objectToRemove);
        }
    }

    private Vector2 GetCanvasPositionFromTarget(TrackObject target)
    {
        Vector3 poiMarkerPoint = _mainCamera.WorldToViewportPoint(target.transform.position);

        poiMarkerPoint.x = Mathf.Clamp01(poiMarkerPoint.x);
        poiMarkerPoint.y = Mathf.Clamp01(poiMarkerPoint.y);

        if (poiMarkerPoint.z < 0 )
        {
            poiMarkerPoint.y = 0;
            poiMarkerPoint.x = 1f - poiMarkerPoint.x;
        }

        Vector2 canvasSize = _parentCanvas.GetComponent<RectTransform>().sizeDelta;
        poiMarkerPoint.Scale(canvasSize);
        return poiMarkerPoint;
    }
}
