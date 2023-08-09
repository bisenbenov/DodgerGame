using System;
using UnityEngine;

public class MenuModel : MonoBehaviour
{
    [SerializeField] private MenuViewModel _menuViewModel;
    [SerializeField] private GameObject _menuButtons;
    [SerializeField] private Goodie _goodie;
    [SerializeField] private Score _score;
    [SerializeField] private BaddieSpawner _baddieSpawner;

    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    private void OnEnable()
    {
        _menuViewModel.OnStartButton += StartGame;
        _menuViewModel.OnQuitButton += QuitGame;
        _goodie.onGoodieDied += ActivateMenu;
    }

    private void StartGame(object sender, EventArgs e)
    {
        Time.timeScale = 1.0f;
        _menuButtons.SetActive(false);
        _baddieSpawner.enabled = true;
        _goodie.ResetGoodie();
        _score.SetDefault();
    }

    private void ActivateMenu(object sender, EventArgs e)
    {
        Time.timeScale = 0.0f;
        _baddieSpawner.enabled = false;
        _menuButtons.SetActive(true);
    }

    private void QuitGame(object sender, EventArgs e)
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        _menuViewModel.OnStartButton -= StartGame;
        _menuViewModel.OnQuitButton -= QuitGame;
        _goodie.onGoodieDied -= ActivateMenu;
    }
}
