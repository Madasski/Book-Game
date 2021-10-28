using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : Screen
{
    public event Action RestartPressed;

    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartClicked);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartClicked);
    }

    private void OnRestartClicked()
    {
        RestartPressed?.Invoke();
    }
}