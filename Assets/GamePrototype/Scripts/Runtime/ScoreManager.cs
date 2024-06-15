using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    public class ScoreManager
    {
        public event Action<int> onScoreUpdate, onTotalScoreUpdate;

        protected int successScore, stageCompleteScore;

        private int _stageScore, _totalScore;
        public int StageScore
        {
            get => _stageScore;
            protected set
            {
                _stageScore = value;
                onScoreUpdate?.Invoke(_stageScore);
            }
        }
        public int TotalScore
        {
            get => _totalScore;
            protected set
            {
                _totalScore = value;
                onTotalScoreUpdate?.Invoke(_totalScore);
            }
        }

        public ScoreManager(int successScore = 100, int stageCompleteScore = 1000)
        {
            this.successScore = successScore;
            this.stageCompleteScore = stageCompleteScore;
            GameEventManager.onMatchSuccess += HandleMatchSuccess;
            GameEventManager.onMatchFailed += HandleMatchFailed;
            GameEventManager.onStageStarted += HandleStageStarted;
            GameEventManager.onStageCompleted += HandleStageCompleted;
        }

        ~ScoreManager()
        {
            GameEventManager.onMatchSuccess -= HandleMatchSuccess;
            GameEventManager.onMatchFailed -= HandleMatchFailed;
            GameEventManager.onStageStarted -= HandleStageStarted;
            GameEventManager.onStageCompleted -= HandleStageCompleted;
        }

        public void Reset() => StageScore = 0;

        protected virtual void HandleMatchSuccess()
        {
            StageScore += successScore;
            Debug.Log($"Score: {StageScore}");
        }

        protected virtual void HandleMatchFailed()
        {
            // Not implemented, ready to override
        }

        protected virtual void HandleStageCompleted(int stageId)
        {
            TotalScore += StageScore + stageCompleteScore * (1 + stageId);
            Debug.Log($"Total Score: {StageScore}");
        }

        protected virtual void HandleStageStarted(int obj)
        {
            StageScore = 0;
        }

    }
}
