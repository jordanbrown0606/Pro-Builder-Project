using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTester : MonoBehaviour
{
    public float timeToDespawn;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0.001f, 0.001f);   
    }

    private void Spawn()
    {
        GameObject go = ObjectPooling.Spawn(prefab, transform.position, Quaternion.identity);
        StartCoroutine(Despawn(go));
    }

    IEnumerator Despawn(GameObject go)
    {
        yield return new WaitForSeconds(timeToDespawn);
        ObjectPooling.Despawn(go);
    }
}
