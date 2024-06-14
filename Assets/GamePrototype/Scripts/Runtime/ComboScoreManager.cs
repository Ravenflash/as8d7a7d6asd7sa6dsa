using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenflash.GamePrototype
{

    public class ComboScoreManager : ScoreManager
    {
        private int _combo = 0;

        public ComboScoreManager(int successScore = 100, int stageCompleteScore = 1000) : base(successScore, stageCompleteScore) 
        {
            Debug.Log("Combo Score Manager Init");
        }

        protected override void HandleMatchSuccess()
        {
            _combo++;
            score += _combo * successScore;
            Debug.Log($"Score: {score}");
        }

        protected override void HandleMatchFailed()
        {
            base.HandleMatchFailed();
            _combo = 0;
        }

    }
}
