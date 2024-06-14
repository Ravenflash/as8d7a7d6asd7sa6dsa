using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    public class ScoreManager
    {
        protected int score;
        protected int successScore, stageCompleteScore;

        public ScoreManager(int successScore = 100, int stageCompleteScore = 1000)
        {
            this.successScore = successScore;
            this.stageCompleteScore = stageCompleteScore;
            GameEventManager.onMatchSuccess += HandleMatchSuccess;
            GameEventManager.onMatchFailed += HandleMatchFailed;
            GameEventManager.onStageCompleted += HandleStageCompleted;
        }

        ~ScoreManager()
        {
            GameEventManager.onMatchSuccess -= HandleMatchSuccess;
            GameEventManager.onMatchFailed -= HandleMatchFailed;
            GameEventManager.onStageCompleted -= HandleStageCompleted;
        }

        public void Reset() => score = 0;

        protected virtual void HandleMatchSuccess()
        {
            score += successScore;
            Debug.Log($"Score: {score}");
        }

        protected virtual void HandleMatchFailed()
        {
            // Not implemented, ready to override
        }

        protected virtual void HandleStageCompleted(int stageId)
        {
            score += stageCompleteScore * (1 + stageId);
            Debug.Log($"Final Score: {score}");
        }

    }
}
