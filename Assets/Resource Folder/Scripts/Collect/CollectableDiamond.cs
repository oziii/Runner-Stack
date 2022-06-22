using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Collect;
using _Game.Obstacle;
using OziLib;
using UnityEngine;

public class CollectableDiamond : Collectable, ObstacleDelegate
{
    [SerializeField] private ParticleSystem _groundParticle;
    [SerializeField] private ParticleSystem _destroyPartice;

    private void OnEnable()
    {
        EventManager.StartListening(EventTags.LEVEL_END, onLevelEnd);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventTags.LEVEL_END, onLevelEnd);
    }

    private void onLevelEnd(object arg0)
    {
        StartCoroutine(ScaleLerp(_collectModel, Vector3.one * 0.1f, .3f));
    }

    protected override void onTriggerCompleted()
    {
        base.onTriggerCompleted();
        _groundParticle.gameObject.SetActive(false);
    }

    public void onObstacleInteract(IObstacle obstacle)
    {
        EventManager.TriggerEvent(EventTags.DIA_DESTROY, gameObject);
        _collectModel.gameObject.SetActive(false);
        //_destroyPartice.Play();
    }

    IEnumerator ScaleLerp(Transform transformObject, Vector3 endValue, float duration)
    {
        float time = 0;
        Vector3 startScale = transformObject.localScale;
        while (time < duration)
        {
            var scaleModifier = Mathf.Lerp(startScale.x, endValue.x,  time / duration);
            transformObject.localScale = startScale * scaleModifier;
            time += Time.deltaTime;
            yield return null;
        }
        transformObject.localScale = endValue;
        transformObject.gameObject.SetActive(false);
    }
}
