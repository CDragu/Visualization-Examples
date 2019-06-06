using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        collision.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(collision.collider.gameObject.GetComponent<FlowLine>());
        Destroy(collision.collider.gameObject.GetComponent<Rigidbody>());
        Destroy(collision.collider.gameObject.GetComponent<MeshRenderer>());
        Destroy(collision.collider.gameObject.GetComponent<SphereCollider>());
    }
}
