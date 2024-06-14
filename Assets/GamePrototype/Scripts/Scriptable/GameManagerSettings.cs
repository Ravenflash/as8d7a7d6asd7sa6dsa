using System;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    [CreateAssetMenu(fileName = "New Game Manager Settings", menuName = "Ravenflash/Game Manager Settings", order = 20)]
    public class GameManagerSettings : ScriptableObject
    {
        public float cardDisplayDuration;
        public String[] cardNames;
        public CardLayout[] layouts;
    }

    [Serializable]
    public struct CardLayout
    {
        public int cols;
        public int rows;

        public int CardCount => rows * cols;

        public CardLayout(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
        }
    }
}
