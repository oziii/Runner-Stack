using System.Collections;
using System.Collections.Generic;
using OziLib;
using UnityEngine;

public class TriggerEventTest : MonoBehaviour, IData
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventManager.TriggerEvent(EventTags.TEST, this);
        }  
        if (Input.GetKeyDown(KeyCode.D))
        {
            var data = new DataSend
            {
                floatData = .5f,
                intData = 5,
                ssData = "mrb"
            };
            EventManager.TriggerEvent(EventTags.TEST_DATA, data);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            EventManager.TriggerEvent(EventTags.TEST_I, this);
        }
    }

    public int GetData()
    {
        return 55;
    }
}

public struct DataSend
{
    public int intData;
    public float floatData;
    public string ssData;
}

public interface IData
{
    int GetData();
}