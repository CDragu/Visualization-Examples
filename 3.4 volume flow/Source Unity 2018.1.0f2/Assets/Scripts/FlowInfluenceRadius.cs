using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowInfluenceRadius : MonoBehaviour
{

    public Vector3 ForceToApplay;

    //void OnTriggerEnter(Collider col)
    //{
    //    
    //}

    void OnTriggerEnter(Collider col)
    {
        //col.transform.position += ForceToApplay;
        //col.gameObject.GetComponent<FlowLine>().OnMoved();
        col.gameObject.GetComponent<Rigidbody>().AddForce(ForceToApplay.normalized * ForceToApplay.magnitude);
        //col.gameObject.GetComponent<FlowLine>().SetGradient(ForceToApplay);
    }
}
