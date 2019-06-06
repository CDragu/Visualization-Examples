using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    //StartCoroutine(WaitAndSpawnParticle(2.5f));
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnParticle(this.transform.position + Vector3.left * 10);
            SpawnParticle(this.transform.position + Vector3.left * 20);
            SpawnParticle(this.transform.position + Vector3.left * 30);
            SpawnParticle(this.transform.position + Vector3.left * 40);
            SpawnParticle(this.transform.position + Vector3.left * 50);
            SpawnParticle(this.transform.position + Vector3.left * 60);
            SpawnParticle(this.transform.position + Vector3.left * 70);
            SpawnParticle(this.transform.position + Vector3.left * 80);
            SpawnParticle(this.transform.position + Vector3.left * 90);
            SpawnParticle(this.transform.position + Vector3.left * 100);
            SpawnParticle(this.transform.position + Vector3.left * 110);
        }
    }


    private IEnumerator WaitAndSpawnParticle(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SpawnParticle(this.transform.position);
        }
    }

    private void SpawnParticle(Vector3 Position)
    {
        var g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        g.transform.position = Position;
        g.transform.localScale *= 4;
        var r = g.AddComponent<Rigidbody>();
        r.useGravity = false;
    }
}
