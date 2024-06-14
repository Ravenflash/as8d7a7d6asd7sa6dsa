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
        [SerializeField] AppSettings _settings;

        // Note: Find methods are costly but you can cache it.
        // Also it will not be called as long as the GameManager ref is provided in the editor.
        [SerializeField] GameManager _game;

        ScoreManager _score;

        public GameManager Game { get { if (!_game) _game = FindObjectOfType<GameManager>(); return _game; } }

        private void Start()
        {
            Application.targetFrameRate = _settings.fps;

            _score = GetScoringSystem();

            Game.StartNewGame();
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
}
