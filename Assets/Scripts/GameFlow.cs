using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow
{
    public event Action GameOver;
    public event Action GameWon;

    private LevelSettingsSO _levelSettings;
    private LevelLoader _levelLoader;
    private List<GameItem> _selectedGameItems;
    private GameItem[,] _gameItems;
    private readonly int _maxTurns;
    private int _turn;

    public GameFlow(LevelLoader levelLoader, LevelSettingsSO levelSettings)
    {
        _levelLoader = levelLoader;
        _levelSettings = levelSettings;

        _maxTurns = levelSettings.NumberOfTurns;
    }

    public void StartGame()
    {
        _selectedGameItems = new List<GameItem>();
        _gameItems = _levelLoader.LoadLevel(_levelSettings);
        _turn = 0;

        foreach (var gameItem in _gameItems)
        {
            gameItem.Pressed += OnGameItemPress;
        }
    }

    public void RestartGame()
    {
        for (int i = 0; i < _gameItems.GetLength(0); i++)
        {
            for (int j = 0; j < _gameItems.GetLength(1); j++)
            {
                _gameItems[i, j].Destroy();
            }
        }

        StartGame();
    }

    private void OnGameItemPress(GameItem pressedGameItem)
    {
        if (pressedGameItem.IsMoving) return;

        _selectedGameItems.Add(pressedGameItem);

        if (_selectedGameItems.Count == 2)
        {
            SwapSelectedGameItems();
            bool isGameWon = CheckWinCondition();
            if (isGameWon) return;

            _turn++;
            CheckGameOverCondition();
        }
    }

    private void CheckGameOverCondition()
    {
        if (_turn >= _maxTurns)
        {
            OnGameOver();
        }
    }

    private void OnGameOver()
    {
        GameOver?.Invoke();
    }

    private void SwapSelectedGameItems()
    {
        var firstItem = _selectedGameItems[0];
        var secondItem = _selectedGameItems[1];

        firstItem.MoveToPoint(secondItem.transform.position);
        secondItem.MoveToPoint(firstItem.transform.position);

        _gameItems[firstItem.ShelfNumber, firstItem.PositionOnShelf] = secondItem;
        _gameItems[secondItem.ShelfNumber, secondItem.PositionOnShelf] = firstItem;

        var firstPosition = firstItem.transform.position;
        firstItem.transform.position = secondItem.transform.position;
        secondItem.transform.position = firstPosition;

        var firstShelfNumber = firstItem.ShelfNumber;
        var firstShelfPosition = firstItem.PositionOnShelf;

        firstItem.SetShelfNumber(secondItem.ShelfNumber);
        secondItem.SetShelfNumber(firstShelfNumber);

        firstItem.SetPositionOnShelf(secondItem.PositionOnShelf);
        secondItem.SetPositionOnShelf(firstShelfPosition);

        _selectedGameItems.Clear();
    }

    private bool CheckWinCondition()
    {
        for (int i = 0; i < _gameItems.GetLength(0); i++)
        {
            var horizontalColorIndex = _gameItems[i, 0].ColorIndex;
            for (int j = 1; j < _gameItems.GetLength(1); j++)
            {
                if (_gameItems[i, j].ColorIndex != horizontalColorIndex)
                {
                    return false;
                }
            }
        }

        GameWon?.Invoke();

        return true;
    }
}