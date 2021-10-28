using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings")]
public class LevelSettingsSO : ScriptableObject
{
    [SerializeField] private int _booksPerShelf;
    [SerializeField] private int numberOfTurns;
    [SerializeField] private List<Color> _colors;
    [SerializeField] private Sprite _gameItemIcon;

    public int BooksPerShelf => _booksPerShelf;
    public int NumberOfTurns => numberOfTurns;
    public List<Color> Colors => _colors;
    public Sprite GameItemIcon => _gameItemIcon;
}