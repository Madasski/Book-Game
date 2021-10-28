using UnityEngine;

public class Boot : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoaderPrefab;
    [SerializeField] private LevelSettingsSO _levelSettingsSO;
    [SerializeField] private GameUI _gameUIPrefab;


    private LevelLoader _levelLoader;
    private GameFlow _gameFlow;
    private GameUI _gameUI;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _gameUI = Instantiate(_gameUIPrefab);
        _levelLoader = Instantiate(_levelLoaderPrefab);

        _gameFlow = new GameFlow(_levelLoader, _levelSettingsSO);
        
        _gameUI.StartPressed += _gameFlow.StartGame;
        _gameUI.RestartPressed += _gameFlow.RestartGame;
        
        _gameFlow.GameOver += _gameUI.ShowGameOverScreen;
        _gameFlow.GameWon += _gameUI.ShowGameWonScreen;
    }

    private void OnDisable()
    {
        _gameUI.StartPressed -= _gameFlow.StartGame;
        _gameUI.RestartPressed -= _gameFlow.RestartGame;
        
        _gameFlow.GameOver -= _gameUI.ShowGameOverScreen;
        _gameFlow.GameWon -= _gameUI.ShowGameWonScreen;
    }
}