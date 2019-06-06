using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatingGrid : MonoBehaviour {

    public ReadingData FlowData;

    public Material PointerMat;
    public Material LineMat;
    public Material BubbleMat;
    public Material ArrowMat;

    public bool LineRenderers;

    public AnimationCurve Blue = new AnimationCurve();
    public AnimationCurve Red = new AnimationCurve();
    public AnimationCurve Pressure = new AnimationCurve();

    public GameObject Arrow;

    public float ScanPosition = 2;

    public Slider Slider;

    // Use this for initialization
    void Start () {

        FlowData.ReadString();

        CreateCurves();

        foreach (var data in FlowData.Position_Temp_Press)
        {
            var g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.position = data.Position;
            g.transform.localScale *= 0.1f;
            Destroy(g.GetComponent<SphereCollider>());
            g.GetComponent<MeshRenderer>().materials = new Material[] { PointerMat };
            Destroy(g.GetComponent<MeshRenderer>());

            AddTempBubble(data, g);

            AddArrows(data, g);

            if (LineRenderers)
                AddLineRenderer(data, g);
        }

        Time.timeScale = 100;
    }

    public void Update()
    {
        ScanPosition = Slider.value * 15 + 1f;
    }

    private void CreateCurves()
    {
        Blue.AddKey(new Keyframe(FlowData.MinTemp, 255));
        Blue.AddKey(new Keyframe(FlowData.MaxTemp, 0));

        Red.AddKey(new Keyframe(FlowData.MinTemp, 0));
        Red.AddKey(new Keyframe(FlowData.MaxTemp, 255));

        Pressure.AddKey(new Keyframe(FlowData.MinPress, 0));
        Pressure.AddKey(new Keyframe(FlowData.MaxPress, 255));
    }


    private void AddLineRenderer(ReadingData.Reading Data, GameObject g)
    {
        LineRenderer line = g.AddComponent<LineRenderer>();

        line.startWidth = 0.01f;
        line.endWidth = 0.1f;
        line.startColor = Color.green;
        line.endColor = Color.green;
        line.materials = new Material[] { LineMat };

        line.numCapVertices = 6;

        line.SetPositions(new Vector3[] { Data.Position, Data.Position + Data.Flow });
    }

    private void AddArrows(ReadingData.Reading Data, GameObject g)
    {

        if (Data.Flow.magnitude > 0.1f)
        {
            var arrow = GameObject.Instantiate(Arrow, Data.Position, Quaternion.identity);
            arrow.transform.LookAt(Data.Position + Data.Flow);
            arrow.transform.position = Data.Position + Data.Flow;
            arrow.transform.localScale *= 0.05f;
            arrow.transform.localScale = new Vector3(
                arrow.transform.localScale.x * (Data.Pressure + 1) * 1, 
                arrow.transform.localScale.y * (Data.Pressure + 1) * 1, 
                arrow.transform.localScale.z * (Data.Pressure + 1) * 1);

            Material arrowMat = new Material(ArrowMat);

            arrowMat.SetColor("_Color", new Color(Pressure.Evaluate(Data.Pressure) / 255.0f, 1, 0, 1));

            arrow.GetComponentInChildren<MeshRenderer>().materials = new Material[]
            {
                arrowMat
            };

            var visible = arrow.AddComponent<VisibleArrow>();
            visible.Grid = this;
            visible.meshRenderer = arrow.GetComponentInChildren<MeshRenderer>();
        }
    }

    private void AddTempBubble(ReadingData.Reading Data, GameObject g)
    {
        var bubble = GameObject.CreatePrimitive(PrimitiveType.Cube);

        bubble.transform.position = g.transform.position;

        bubble.transform.localScale *= 1f;

        Material bubbleMaterial = new Material(BubbleMat);

        bubble.GetComponent<MeshRenderer>().materials = new Material[]{ bubbleMaterial };

        bubbleMaterial.SetColor("_Color", new Color(Red.Evaluate(Data.Temp) / 255.0f, 0, Blue.Evaluate(Data.Temp) / 255.0f, 0.03f));
      
    }
}
