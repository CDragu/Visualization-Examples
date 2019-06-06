using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReadingData : MonoBehaviour {

    public TextAsset Data;

    public int EntriesToSkip;

    public int LinesToRead;

    public List<Reading> Position_Temp_Press = new List<Reading>();

    public float MaxTemp;
    public float MinTemp;
    public float MaxPress;
    public float MinPress;

    public Vector3 Middle;

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

        string entrie;
        string[] DataArray = Data.text.Split(new char[] {' ', '\n' ,'\r'});
        int index = 0;

        index = EntriesToSkip;

        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                for (int k = 0; k < 18; k++)
                {
                    string x;
                    if (DataArray[index] != "")
                    {
                        x = DataArray[index++];
                    }
                    else
                    {
                        while (DataArray[index] == "")
                        {
                            index++;
                        }

                        x = DataArray[index++];
                    }

                    string y;
                    if (DataArray[index] != "")
                    {
                        y = DataArray[index++];
                    }
                    else
                    {
                        while (DataArray[index] == "")
                        {
                            index++;
                        }
                        y = DataArray[index++];
                    }

                    string z;
                    if (DataArray[index] != "")
                    {
                        z = DataArray[index++];
                    }
                    else
                    {
                        while (DataArray[index] == "")
                        {
                            index++;
                        }
                        z = DataArray[index++];
                    }

                    string t;
                    if (DataArray[index] != "")
                    {
                        t = DataArray[index++];
                    }
                    else
                    {
                        while (DataArray[index] == "")
                        {
                            index++;
                        }
                        t = DataArray[index++];
                    }

                    string p;
                    if (DataArray[index] != "")
                    {
                        p = DataArray[index++];
                    }
                    else
                    {
                        while (DataArray[index] == "")
                        {
                            index++;
                        }
                        p = DataArray[index++];
                    }

                    Reading r;
                    Position_Temp_Press.Add(r = new Reading()
                    {
                        Position = new Vector3(k, j, i),


                        Flow = new Vector3((float)Decimal.Parse(x, System.Globalization.NumberStyles.Float) * 10.0f,
                                            (float)Decimal.Parse(y, System.Globalization.NumberStyles.Float) * 10.0f,
                                            -(float)Decimal.Parse(z, System.Globalization.NumberStyles.Float) * 10.0f),

                        Temp = (float)Decimal.Parse(t, System.Globalization.NumberStyles.Float),
                        Pressure = (float)Decimal.Parse(p, System.Globalization.NumberStyles.Float)

                    });

                    if (r.Temp > MaxTemp)
                    {
                        MaxTemp = r.Temp;
                    }

                    if (r.Temp < MinTemp)
                    {
                        MinTemp = r.Temp;
                    }

                    if (r.Pressure > MaxPress)
                    {
                        MaxPress = r.Pressure;
                    }

                    if (r.Pressure < MinPress)
                    {
                        MinPress = r.Pressure;
                    }

                    if (Middle != Vector3.zero)
                    {
                        Middle = Vector3.Lerp(Middle, r.Position, 0.5f);
                    }
                    else
                    {
                        Middle = r.Position;
                    }
                    
                }
            }
        }
    }

    public class Reading
    {
        public Vector3 Position;
        public Vector3 Flow;
        public float Temp;
        public float Pressure;
    }
}
