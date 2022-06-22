using System;
using System.Collections;
using System.Collections.Generic;
using OziLib;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIBase _tapToPlayScene;
    [SerializeField] private UIBase _inGameScene;
    [SerializeField] private UIBase _levelEndyScene;
    [SerializeField] private UIBase _staticElements;
    
    #region UNITY_METHODS

    private void OnEnable()
    {
        EventManager.StartListening(EventTags.LEVEL_START, LevelStart);
        EventManager.StartListening(EventTags.LEVEL_END, LevelEnd);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventTags.LEVEL_START, LevelStart);
        EventManager.StopListening(EventTags.LEVEL_END, LevelEnd);
    }

    #endregion

    #region METHODS

    public void init()
    {
        TapToPlayScene();
    }
    
    private void TapToPlayScene()
    {
        _levelEndyScene.CloseUI();
        _inGameScene.CloseUI();
        _tapToPlayScene.ShowUI();
        _staticElements.ShowUI();
    }

    private void InGameScene()
    {
        _levelEndyScene.CloseUI();
        _inGameScene.ShowUI();
        _tapToPlayScene.CloseUI();
        _staticElements.ShowUI();
    }

    private void LevelEndScene()
    {
        _levelEndyScene.ShowUI();
        _inGameScene.CloseUI();
        _tapToPlayScene.CloseUI();
        _staticElements.CloseUI();
    }
    

    #endregion

    #region BUTTON_ACTION
    

    #endregion

    #region ACTION

    private void LevelEnd(object arg0)
    {
        LevelEndScene();
    }

    private void LevelStart(object arg0)
    {
        InGameScene();
    }

    #endregion
}
