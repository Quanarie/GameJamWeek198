using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _objWinLose, _objKillMessage, _objKillCounters;

    [SerializeField]
    private TextMeshProUGUI _txtTitle, _txtKillCounter, _txtKillCounterMessage;

    [SerializeField]
    private string _title;

    [SerializeField]
    private int _killCounter;

    [SerializeField]
    private string _killCounterMessage;

    [SerializeField]
    private bool _isGameOver;

    [SerializeField]
    private Button _btnTryAgain, _btnMainMenu;

    [SerializeField]
    private BackgroundPanner _backgroundPanner;

    private void Start()
    {
        LandingAreaManager.Current.SubscribeToOnLandingInWinArea(LandingAreaManager_OnLandingWinArea);
        CatHealth catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();
        catHealth.SubscribeToOnDeath(CatHealth_OnDeath);
        _btnTryAgain.onClick.AddListener(BtnTryAgain_OnPress);
        _btnMainMenu.onClick.AddListener(BtnMainMenu_OnPress);
    }

    private void BtnMainMenu_OnPress()
    {
        GameStateManager.Current.ChangeGameState(GameState.MainMenu);
        SceneManager.LoadScene(0);
    }

    private void BtnTryAgain_OnPress()
    {
        ResetGameEndUI();
    }

    public void ResetGameEndUI()
    {
        _isGameOver = false;
        Time.timeScale = 1;
        LevelResetter.Current.ResetLevel();
        _backgroundPanner.EnablePanning(true);
        _objWinLose.SetActive(false);
    }
    private void CatHealth_OnDeath(bool isDead)
    {
        if (isDead)
        {
            if (!_isGameOver)
            {
                _isGameOver = true;
                Time.timeScale = 0;
                _title = "CAT IS DEAD";
                _objWinLose.SetActive(true);
                EnableKillCounterAndKillMessage(false);
                UpdateEndGamePage();
            }
        }
    }

    private void LandingAreaManager_OnLandingWinArea(bool isLandedSafely)
    {
        if (!_isGameOver)
        {
            _isGameOver = true;
            _objWinLose.SetActive(true);

            if (isLandedSafely)
            {
                _title = "CAT LANDED SAFELY";
                EnableKillCounterAndKillMessage(true);
                UpdateKillCounterValueAndMessage();
            }

            else
            {
                _title = "CAT FAILED THE LANDING";
                EnableKillCounterAndKillMessage(false);
            }

            UpdateEndGamePage();
        }
    }

    private void UpdateKillCounterValueAndMessage()
    {
        _killCounter = PlayerBirdKillTracker.Current.GetCurrentKillCount();

        if (_killCounter < 20)
            _killCounterMessage = "You are a Stray cat! It shows..";
        else if (_killCounter >= 20 && _killCounter < 40)
            _killCounterMessage = "You are a House cat! You could've done better..";
        else if (_killCounter >= 40 && _killCounter < 60)
            _killCounterMessage = "You are a Fighter cat! You have an aptitude for this!";
        else if (_killCounter >= 60 && _killCounter < 80)
            _killCounterMessage = "You are an Assassin cat! Let your enemies cower in fear!";
        else if (_killCounter >= 80 && _killCounter < 100)
            _killCounterMessage = "You are the cat King! All hail the true King!";
        else if (_killCounter >= 100 && _killCounter < 120)
            _killCounterMessage = "You are the cat Emperor! Why be a king when you can be the Emperor?";
        else if (_killCounter >= 120)
            _killCounterMessage = "You are the Divine cat! The one and only! Praise to thee!";
    }

    private void EnableKillCounterAndKillMessage(bool isEnabled)
    {
        _objKillCounters.SetActive(isEnabled);
        _objKillMessage.SetActive(isEnabled);
    }
    private void UpdateEndGamePage()
    {
        _txtTitle.text = _title;
        _txtKillCounter.text = _killCounter.ToString();
        _txtKillCounterMessage.text = _killCounterMessage;
    }
}
