using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ReadingFlowData : MonoBehaviour
{

    public TextAsset Data;

    public int LinesToSkip;

    public int LinesToRead;

    public List<Reading> PositionAndFlow = new List<Reading>();

    public Vector3 MaxFlowPos;

    public Vector3 Middle;

    public float MaxFlow = 0;

    public float MinFlow = 100;

    public void ReadStringFromFile()
    {
        if (LinesToRead == 0)
        {
            LinesToRead = 100000;
        }

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(Application.dataPath + "/Resources/flow.data"); //load text file with data
        //Debug.Log(reader.ReadToEnd());

        string line;

        do
        {
            if (LinesToSkip > 0)
            {
                line = reader.ReadLine();
                LinesToSkip--;
                continue;
            }

            line = reader.ReadLine();
            if(line == null)
                break;

            LinesToRead--;

            string[] numbers = line.Split(' ');

            PositionAndFlow.Add(new Reading()
            {
                Positions = new Vector3((float)Decimal.Parse(numbers[1], System.Globalization.NumberStyles.Float)        ,
                                        (float)Decimal.Parse(numbers[3], System.Globalization.NumberStyles.Float) /4.0f  ,
                                        (float)Decimal.Parse(numbers[2], System.Globalization.NumberStyles.Float))/2.0f  ,
                Flows = new Vector3((float)Decimal.Parse(numbers[4], System.Globalization.NumberStyles.Float)  *200.0f,
                                    (float)Decimal.Parse(numbers[6], System.Globalization.NumberStyles.Float)  *200.0f,
                                    (float)Decimal.Parse(numbers[5], System.Globalization.NumberStyles.Float)  *200.0f),
            });
        } while (line != null && LinesToRead > 0);

        reader.Close();
    }

    public void ReadString()
    {
        if (!Data)
        {
            return;
        }

        if (LinesToRead == 0)
        {
            LinesToRead = 100000;
        }

       
        

        string line;
        string[] DataArray = Data.text.Split('\n');
        int index = 0;

        do
        {
            if (LinesToSkip > 0)
            {
                line = DataArray[index];
                index++;
                LinesToSkip--;
                continue;
            }

            line = DataArray[index];
            index++;
            if (line == null)
                break;

            LinesToRead--;

            string[] numbers = line.Split(' ');
            Reading r;
            PositionAndFlow.Add(r = new Reading()
            {
                Positions = new Vector3(((float)Decimal.Parse(numbers[1], System.Globalization.NumberStyles.Float) / 2.0f     )  /2.0f,
                                        ((float)Decimal.Parse(numbers[3], System.Globalization.NumberStyles.Float) / 4.0f     )  /2.0f,
                                        (-(float)Decimal.Parse(numbers[2], System.Globalization.NumberStyles.Float) / 1.0f     )  /2.0f),
                Flows = new Vector3((float)Decimal.Parse(numbers[4], System.Globalization.NumberStyles.Float) *  1.0f,
                                    (float)Decimal.Parse(numbers[6], System.Globalization.NumberStyles.Float) *  1.0f,
                                    -(float)Decimal.Parse(numbers[5], System.Globalization.NumberStyles.Float) * 1.0f),
            });

            if (r.Flows.magnitude > MaxFlow)
            {
                MaxFlow = r.Flows.magnitude;
                MaxFlowPos = r.Positions;
            }

            if (r.Flows.magnitude < MinFlow)
            {
                MinFlow = r.Flows.magnitude;
            }

            if (Middle == Vector3.zero)
            {
                Middle = r.Positions;
            }
            else
            {
                Middle = Vector3.Lerp(Middle, r.Positions, 0.5f);
            }

        } while (line != null && LinesToRead > 0 && index < DataArray.Length - LinesToSkip-1);

        List<Reading> SortedList = PositionAndFlow.OrderBy(o => o.Flows.magnitude).ToList();

        PositionAndFlow = SortedList;

    }

    public class Reading
    {
       public Vector3 Positions;
       public Vector3 Flows;
    }
}
