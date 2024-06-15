using Ravenflash.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    // Note: This could be handled by a DI Framework
    // but as long as we are creating a simple prototype,
    // unit tests are not required,
    // and usage of frameworks was restricted during this assignment
    // Singleton is an acceptable solution
    // (although some might argue it's an anti-pattern)

    public class AppManager : Singleton<AppManager>
    {
        public event Action onAppReady;

        [SerializeField] AppSettings _settings;

        public bool IsReady { get; private set; } = false;
        public ScoreManager Score { get; private set; }

        public ISaveSystem SaveSystem { get; private set; }

        // Note: Find methods are costly but you can cache it.
        // Also it will not be called as long as the GameManager ref is provided in the editor.
        [SerializeField] GameManager _game;
        public GameManager Game { get { if (!_game) _game = FindObjectOfType<GameManager>(); return _game; } }

        [SerializeField] ViewManager _view;
        public ViewManager View { get { if (!_view) _view = FindObjectOfType<ViewManager>(); return _view; } }

        private void Start()
        {
            base.Awake();
            Score = GetScoringSystem();
            SaveSystem = new SaveSystem();
            Application.targetFrameRate = _settings.fps;

            IsReady = true;
            onAppReady?.Invoke();

            View?.DisplayMainMenu();
        }

        public void StartNewGame()
        {
            try
            {
                SaveSystem.Clear();
                View.DisplayGameplay();
                Game.StartNewGame();
            }
            catch { throw; }
        }

        public void LoadAndContinueGame()
        {
            SaveData data = SaveSystem.Load();
            try
            {
                Score.TotalScore = data.totalScore;
                Game.CurrentStageId = data.stageId;
                SaveSystem.Clear();
                View.DisplayGameplay(); 
                Game.StartNextStage();
            }
            catch { throw; }
        }

        public void StartNextStage()
        {
            try
            {
                View.DisplayGameplay();
                Game.StartNextStage();
            }
            catch { throw; }
        }

        public void SaveAndQuit()
        {
            try
            {
                SaveSystem.Save(new SaveData(Game.CurrentStageId, Score.TotalScore));
                View.DisplayMainMenu();
            }
            catch { throw; }
        }

        public void QuitApp()
        {
            Application.Quit();
        }

        private ScoreManager GetScoringSystem()
        {
            switch (_settings.scoringSystem)
            {
                case ScoringSystem.Combo:
                    return new ComboScoreManager(_settings.matchSuccessScore, _settings.stageCompleteScore);
                default:
                    return new ScoreManager(_settings.matchSuccessScore, _settings.stageCompleteScore);
            }
        }
    }

    public struct SaveData
    {
        public int stageId;
        public int totalScore;

        public SaveData(int stageId, int totalScore)
        {
            this.stageId = stageId; 
            this.totalScore = totalScore;
        }
    }
}
