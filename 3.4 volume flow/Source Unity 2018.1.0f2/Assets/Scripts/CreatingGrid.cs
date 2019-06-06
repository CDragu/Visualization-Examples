using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingGrid : MonoBehaviour
{
    public ReadingFlowData FlowData;

    public Material PointerMat;
    public Material ArrowMat;

    public bool LineRenderers;

    public GameObject LineTracer;

    public AnimationCurve Blue = new AnimationCurve();
    public AnimationCurve Red = new AnimationCurve();


    // Use this for initialization
    void Start () {

	    FlowData.ReadString();
        CreateCurves(FlowData.MinFlow, FlowData.MaxFlow);

        foreach (var data in FlowData.PositionAndFlow)
        {
            var g = GameObject.CreatePrimitive(PrimitiveType.Cube);
            g.transform.position = data.Positions;
            g.transform.localScale *= 5f;
            Destroy(g.GetComponent<BoxCollider>());
            Destroy(g.GetComponent<MeshRenderer>());

            var gc = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gc.transform.parent = g.transform;
            gc.transform.localPosition = Vector3.zero;

            Material m = new Material(PointerMat);

            gc.GetComponent<MeshRenderer>().materials = new Material[] {m};
            gc.transform.localScale = new Vector3(
                gc.transform.localScale.x  * 5.0f,
                gc.transform.localScale.y  * 10.0f,
                gc.transform.localScale.z  * 10.0f);

            m.SetColor("_Color", new Color(Red.Evaluate(data.Flows.magnitude) / 255.0f, 0, Blue.Evaluate(data.Flows.magnitude) / 255.0f, 0.015f));

            Destroy(gc.GetComponent<BoxCollider>());

            if (LineRenderers)
                AddLineRenderer(data, g);

            var sphereCollider = g.GetComponent<SphereCollider>();

            if (sphereCollider == null)
            {
                sphereCollider = g.AddComponent<SphereCollider>();
            }

            sphereCollider.isTrigger = true;
            sphereCollider.radius = sphereCollider.radius * 1.3f * 2;
            var field = g.AddComponent<FlowInfluenceRadius>();
            field.ForceToApplay = data.Flows;

            //g.AddComponent<ParticleSpawner>();
        }

	    Time.timeScale = 100;

        //CreateParticleAtMaxFlow();

	}

    private void CreateCurves(float MinTemp, float MaxTemp)
    {
        Blue.AddKey(new Keyframe(MinTemp, 255));
        Blue.AddKey(new Keyframe(MaxTemp, 0));

        Red.AddKey(new Keyframe(MinTemp, 0));
        Red.AddKey(new Keyframe(MaxTemp, 255));
    }


    public void CreateParticleAtPosition(Vector3 position)
    {
        var g = GameObject.Instantiate(LineTracer, position, Quaternion.identity);
        g.GetComponent<FlowLine>().MaxFlow = FlowData.MaxFlow;
        g.GetComponent<FlowLine>().MinFlow = FlowData.MinFlow;
    }

    private void CreateParticleAtMaxFlow()
    {
        for (int i = 1; i < 21; i++)
        {
            GameObject.Instantiate(LineTracer, FlowData.PositionAndFlow[FlowData.PositionAndFlow.Count - i].Positions, Quaternion.identity);
        }
        
    }

    private void AddLineRenderer(ReadingFlowData.Reading flowDataPosition, GameObject g)
    {
        LineRenderer line = g.AddComponent<LineRenderer>();

        line.startWidth = 0.01f;
        line.endWidth = 0.1f;
        line.startColor = Color.yellow;
        line.endColor = Color.red;
        line.materials = new Material[] { ArrowMat };

        line.numCapVertices = 6;

        line.SetPositions(new Vector3[] { flowDataPosition.Positions, flowDataPosition.Positions + flowDataPosition.Flows });
    }

}
