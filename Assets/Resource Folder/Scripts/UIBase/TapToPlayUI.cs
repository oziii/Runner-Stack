using System.Collections;
using System.Collections.Generic;
using OziLib;
using UnityEngine;
using UnityEngine.UI;

public class TapToPlayUI : UIBase
{
    [SerializeField] private Text _stackUpgradeCountText;

    private int _upgradeValue;
    public override void ShowUI()
    {
        base.ShowUI();
        _upgradeValue = DataManager.instance.GetIntData(EventTags.STACK_UPGRADE_VALUE);
        StackTextUpdate();
    }

    private void StackTextUpdate()
    {
        _stackUpgradeCountText.text = _upgradeValue.ToString();
    }


    public void StackButtonUpgrade()
    {
        var currentStack = DataManager.instance.GetIntData(EventTags.START_STACK_COUNT);
        var maxStack =  DataManager.instance.GetIntData(EventTags.STACK_MAX_COUNTER);
        if (maxStack <= currentStack) return;
        var currentCoin = DataManager.instance.GetIntData(EventTags.COIN_COUNTER);
        if (currentCoin < _upgradeValue) return;
        var setCoin = currentCoin - _upgradeValue;
        DataManager.instance.SetIntData(EventTags.COIN_COUNTER, setCoin);
        
        currentStack++;
        DataManager.instance.SetIntData(EventTags.START_STACK_COUNT, currentStack);
        _upgradeValue += EventTags.UPGRADE_RATIO;
        DataManager.instance.SetIntData(EventTags.STACK_UPGRADE_VALUE, _upgradeValue);
        StackTextUpdate();
        EventManager.TriggerEvent(EventTags.STACK_COUNT_BUTTON, this);

    }
}
