using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuViewModel : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

    public event EventHandler OnStartButton;
    public event EventHandler OnQuitButton; 

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    { 
        OnStartButton?.Invoke(this, EventArgs.Empty);
    }

    private void QuitGame()
    {
        OnQuitButton?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _quitButton.onClick.RemoveListener(QuitGame);
    }
}
