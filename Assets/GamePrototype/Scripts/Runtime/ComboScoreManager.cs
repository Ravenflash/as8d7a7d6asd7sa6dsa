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
        }

        protected override void HandleMatchSuccess()
        {
            _combo++;
            StageScore += _combo * successScore;
            //Debug.Log($"Score: {StageScore}");
        }

        protected override void HandleMatchFailed()
        {
            base.HandleMatchFailed();
            _combo = 0;
        }

    }
}
