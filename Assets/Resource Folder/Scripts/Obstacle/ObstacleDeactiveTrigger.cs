using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Obstacle;
using UnityEngine;

public class ObstacleDeactiveTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IObstacle>() is IObstacle obstacle)
        {
            obstacle.setObstacleState(false);
        }
    }
}
