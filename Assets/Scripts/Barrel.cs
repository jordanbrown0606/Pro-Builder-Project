using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _explodeRange;
    [SerializeField] private Color _gizmoColour;

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColour;
        Gizmos.DrawWireSphere(transform.position, _explodeRange);
    }

}
