using System;
using System.Collections;
using System.Collections.Generic;
using OziLib;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.StartListening(EventTags.TEST,TestFunc);
        EventManager.StartListening(EventTags.TEST_DATA,TestDataFunc);
        EventManager.StartListening(EventTags.TEST_I,IData);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventTags.TEST,TestFunc);
        EventManager.StopListening(EventTags.TEST_DATA,TestDataFunc);
        EventManager.StopListening(EventTags.TEST_I,IData);
    }

    private void TestFunc(object arg0)
    {
        print("TEST FUNC");
    }

    private void TestDataFunc(object arg0)
    {
        if(arg0 is DataSend dataSend)
            print("TEST DATA "+ dataSend.floatData + " DATA 2 " + dataSend.ssData);
    }

    private void IData(object arg0)
    {
        if (arg0 is IData data)
        {
            print("TEST Interface DATA " + data.GetData());
        }
    }
}
