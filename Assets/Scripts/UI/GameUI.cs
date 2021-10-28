using System;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public event Action StartPressed;
    public event Action RestartPressed;

    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private GameOverScreen _gameWonScreen;

    private void OnEnable()
    {
        _startScreen.StartPressed += OnStartPressed;
        _gameOverScreen.RestartPressed += OnRestartPressed;
        _gameWonScreen.RestartPressed += OnRestartPressed;
    }

    private void OnDisable()
    {
        _startScreen.StartPressed -= OnStartPressed;
        _gameOverScreen.RestartPressed -= OnRestartPressed;
        _gameWonScreen.RestartPressed -= OnRestartPressed;
    }

    public void ShowGameOverScreen()
    {
        _gameOverScreen.Show();
    }

    private void OnStartPressed()
    {
        _startScreen.Hide();
        StartPressed?.Invoke();
    }

    private void OnRestartPressed()
    {
        _gameOverScreen.Hide();
        _gameWonScreen.Hide();
        RestartPressed?.Invoke();
    }

    public void ShowGameWonScreen()
    {
        _gameWonScreen.Show();
    }
}