using System;
using System.Collections;
using System.Collections.Generic;
using OziLib;
using UnityEngine;
using UnityEngine.UI;

public class StaticUIElements : UIBase
{
    [SerializeField] private Text _coinText;
    [SerializeField] private Text _levelText;

    #region UNITY_METHODS

    private void OnEnable()
    {
        EventManager.StartListening(EventTags.COIN_COLLECT, onCoinCollect);
        EventManager.StartListening(EventTags.STACK_COUNT_BUTTON, onStackUpgrade);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventTags.COIN_COLLECT, onCoinCollect);
        EventManager.StopListening(EventTags.STACK_COUNT_BUTTON, onStackUpgrade);
    }



    #endregion

    #region OVERRIDE

    public override void ShowUI()
    {
        base.ShowUI();
        TextSet();
    }

    #endregion

    #region METHODS

    private void TextSet()
    {
        var coinData = DataManager.instance.GetIntData(EventTags.COIN_COUNTER);
        var levelData = DataManager.instance.GetIntData(EventTags.LEVEL_COUNTER);
        _coinText.text = coinData.ToString();
        _levelText.text = "LEVEL " + levelData;
    }

    #endregion
    
    #region ACTIONS

    private void onCoinCollect(object arg0)
    {
        var data = DataManager.instance.GetIntData(EventTags.COIN_COUNTER);
        data++;
        _coinText.text = data.ToString();
        DataManager.instance.SetIntData(EventTags.COIN_COUNTER, data);
    }
    
    private void onStackUpgrade(object arg0)
    {
        TextSet();
    }
    
    #endregion
}
