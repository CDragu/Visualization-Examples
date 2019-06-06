using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowLine : MonoBehaviour
{
    public LineRenderer line;

    public float MaxFlow;
    public float MinFlow;

    public AnimationCurve Red = new AnimationCurve();

    void Start()
    {
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.SetPositions(new Vector3[]{this.transform.position});
        StartCoroutine(WaitAndDrawLine(0.1f));
        CreateCurves();
    }


    private void CreateCurves()
    {
        Red.AddKey(new Keyframe(MinFlow, 0));
        Red.AddKey(new Keyframe(MaxFlow, 255));
    }


    public void OnMoved()
    {
        Vector3[] OldPositions = new Vector3[line.positionCount];
        line.GetPositions(OldPositions);
        Vector3[] NewPositions = new Vector3[line.positionCount + 1];
        for (int i = 0; i < OldPositions.Length; i++)
        {
            NewPositions[i] = OldPositions[i];
        }

        NewPositions[line.positionCount] = this.transform.position;
        line.positionCount = NewPositions.Length;
        line.SetPositions(NewPositions);

    }

    public void SetGradient(Vector3 speed)
    {
        Gradient g;
        GradientColorKey[] gck;
        GradientAlphaKey[] gak;

        g = line.colorGradient;

        gck = g.colorKeys;
        gak = g.alphaKeys;

        GradientColorKey[] newGCK = new GradientColorKey[gck.Length+1];

        GradientAlphaKey[] newGAK = new GradientAlphaKey[gak.Length+1];

        for (int i = 0; i < gak.Length; i++)
        {
            newGCK[i] = gck[i];
        }

        for (int i = 0; i < gak.Length; i++)
        {
            newGAK[i] = gak[i];
        }

        float red = Red.Evaluate(speed.magnitude)/100.0f ;

        newGCK[newGCK.Length - 2] = new GradientColorKey(new Color(red, 0, 0), gak.Length/100.0f);
        newGAK[newGAK.Length - 2] = new GradientAlphaKey(1, gak.Length / 100.0f);

        newGCK[newGCK.Length - 1] = new GradientColorKey(newGCK[newGCK.Length - 2].color, 1);
        newGAK[newGAK.Length - 1] = new GradientAlphaKey(newGAK[newGAK.Length - 2].alpha, 1);

        g.colorKeys = newGCK;
        g.alphaKeys = newGAK;

        line.colorGradient = g;
    }

    private IEnumerator WaitAndDrawLine(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            OnMoved();
        }
    }
}
