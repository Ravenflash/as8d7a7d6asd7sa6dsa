using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    public enum ScoringSystem { Simple, Combo}

    [CreateAssetMenu(fileName = "New App Settings", menuName = "Ravenflash/App Settings", order = 20)]
    public class AppSettings : ScriptableObject
    {
        public int fps = 30;

        [Header("Scoring")]
        public ScoringSystem scoringSystem;
        public int matchSuccessScore = 100;
        public int stageCompleteScore = 1000;
    }
}
