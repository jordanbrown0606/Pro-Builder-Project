using UnityEditor;
using UnityEngine;

public class Barrel : MonoBehaviour, IDamagable
{
    [SerializeField, Min(0f)] private float _explodeRange;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private Color _gizmoColour;

    private bool _hasExploded = false;
    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColour;
        Gizmos.DrawWireSphere(transform.position, _explodeRange);
    }

    public void TakeDamage(int amount)
    {
        if(_hasExploded == true)
        {
            return;
        }

        _hasExploded = true;

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        Debug.Log("Boom");

        Collider[] collidersFound = Physics.OverlapSphere(transform.position, _explodeRange);

        for (int i = 0; i < collidersFound.Length; i++)
        {
            collidersFound[i].GetComponent<IDamagable>()?.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
