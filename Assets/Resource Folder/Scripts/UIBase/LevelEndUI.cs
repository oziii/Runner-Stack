using System.Collections;
using System.Collections.Generic;
using OziLib;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndUI : UIBase
{
    [SerializeField] private Text _totalCoinText;

    public override void ShowUI()
    {
        base.ShowUI();
        var currentCoin = DataManager.instance.GetCurrentCoin();
        _totalCoinText.text = "Collect " + currentCoin + "X";
    }

    public void NextLevelButton()
    {
        EventManager.TriggerEvent(EventTags.NEXT_LEVEL, this);
    }
}
