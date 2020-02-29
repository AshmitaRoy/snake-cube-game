using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour {

    public SphereCollider sphereCol;
    public GameObject spawned;

    void Start()
    {
        var r = sphereCol.radius;

        for (int i = 0; i < 100; i++)
        {
            var spawned = Instantiate(this.spawned) as GameObject;

            var x = Random.Range(-1f, 1f);
            var y = Random.Range(-1f, 1f);
            var z = Random.Range(-1f, 1f);

            var vec = new Vector3(x, y, z).normalized * r;
            spawned.transform.position = vec;
        }
    }
}
