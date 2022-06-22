using System;
using System.Collections;
using System.Collections.Generic;
using OziLib;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            EventManager.TriggerEvent(EventTags.LEVEL_END, this);
        }
    }
}
