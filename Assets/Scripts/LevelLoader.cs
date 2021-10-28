using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameItem GameItemPrefab;

    public GameItem[,] LoadLevel(LevelSettingsSO settingsSO)
    {
        var availableColorIndexes = GenerateAvailableColorIndexes(settingsSO);
        var horizontalSize = settingsSO.BooksPerShelf;
        var verticalSize = 3;

        var gameItems = new GameItem[verticalSize, horizontalSize];

        var offset = new Vector3(horizontalSize-1, verticalSize-1) / 2f;
        for (var i = 0; i < verticalSize; i++)
        {
            for (int j = 0; j < horizontalSize; j++)
            {
                var gameItem = Instantiate(GameItemPrefab, new Vector3(j, i) - offset, Quaternion.identity, transform);

                gameItem.SetShelfNumber(i);
                gameItem.SetPositionOnShelf(j);
                gameItems[i, j] = gameItem;
                gameItem.SetIcon(settingsSO.GameItemIcon);

                var randomColorIndex = Random.Range(0, availableColorIndexes.Count);
                var randomColor = settingsSO.Colors[availableColorIndexes[randomColorIndex]];
                gameItem.SetColor(randomColor, availableColorIndexes[randomColorIndex]);
                availableColorIndexes.RemoveAt(randomColorIndex);
            }
        }

        return gameItems;
    }

    private List<int> GenerateAvailableColorIndexes(LevelSettingsSO settingsSO)
    {
        var availableColorIndexes = new List<int>();

        for (int i = 0; i < settingsSO.Colors.Count; i++)
        {
            for (int j = 0; j < settingsSO.BooksPerShelf; j++)
            {
                availableColorIndexes.Add(i);
            }
        }

        return availableColorIndexes;
    }
}