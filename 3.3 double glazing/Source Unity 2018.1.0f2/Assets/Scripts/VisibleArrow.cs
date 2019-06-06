using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleArrow : MonoBehaviour
{
    public CreatingGrid Grid;

    public MeshRenderer meshRenderer;

	// Update is called once per frame
	void Update () {
	    if (!Grid)
	    {
            return;
	    }

	    if (this.transform.position.z > Grid.ScanPosition - 0.5f && this.transform.position.z < Grid.ScanPosition + 0.5)
	    {
	        meshRenderer.enabled = true;
	    }
	    else
	    {
	        meshRenderer.enabled = false;
        }
	}
}
