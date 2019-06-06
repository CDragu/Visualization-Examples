using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParticleOnMouseClick : MonoBehaviour
{
    public GameObject Grid;
    public LayerMask layer;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.LeftShift))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layer))
            {
                if (hit.transform.GetComponent<Wall>())
                {
                    Grid.GetComponent<CreatingGrid>().CreateParticleAtPosition(hit.point - hit.normal*15);
                }
            }
        }
    }
}
