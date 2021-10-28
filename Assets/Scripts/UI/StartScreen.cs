using System;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : Screen
{
    public event Action StartPressed;

    [SerializeField] private Button _startButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartClicked);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartClicked);
    }

    private void OnStartClicked()
    {
        StartPressed?.Invoke();
    }
}