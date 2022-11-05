using System;
using Assets.Scripts.Managers.ScreensManager;
using Cysharp.Threading.Tasks;
using Events;
using Managers.ScreensManager;
using SaveLoadSystem;
using Screens.LoadScreen;
using SimpleEventBus.Disposables;
using Tools.SimpleEventBus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController: IDisposable
{
    public float LoadingProgress { get; private set; }

    private readonly SaveController _save;
    private readonly int _loopingLevelNumber;
    private readonly ScreenManager _screenManager;
    private readonly CompositeDisposable _subscription;
    private readonly LoadController _loadController;

    private int _nextLevelId;
        
    public LoadSceneController(ScreenManager screenManager, SaveController save, LoadController loadController, Settings settings)
    {
        _screenManager = screenManager;
        _save = save;
        _loopingLevelNumber = settings.LoopingLevelNumber;
        _loadController = loadController;

        _subscription = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<EventVictory>(SetNextLevel),
            EventStreams.UserInterface.Subscribe<EventLoadNextLevel>(StartLoadNextLevel),
            EventStreams.UserInterface.Subscribe<EventRestartLevel>(RestartCurrentLevel)
        };
    }

    public void StartLoadSaveLevel()
    {
        var level = _loadController.GetLevel();
        if (level != SceneManager.GetActiveScene().buildIndex)
        {
            LoadLevel(level);
        }
    }

    private void SetNextLevel(EventVictory eventVictory)
    {
        var currentIDScene = SceneManager.GetActiveScene().buildIndex;
        _nextLevelId = currentIDScene + 1 >= SceneManager.sceneCountInBuildSettings ? _loopingLevelNumber : ++currentIDScene;
        _save.SaveNumberLevel(_nextLevelId);
    }
        
    private void StartLoadNextLevel(EventLoadNextLevel eventLoadNextLevel)
    {
        LoadLevel(_nextLevelId);
    }
        
    private void RestartCurrentLevel(EventRestartLevel eventRestartLevel)
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
        
    private async UniTask LoadLevel(int id)
    {
        var operation = SceneManager.LoadSceneAsync(id);
        while (!operation.isDone)
        {
            LoadingProgress = Mathf.Clamp01(operation.progress / 1f);
            await UniTask.Yield();
        }
        
        EventStreams.UserInterface.Publish(new EventNewLevelDownloadCompleted());
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }

    [Serializable]
    public class Settings
    {
        public int LoopingLevelNumber;
    }
        
}