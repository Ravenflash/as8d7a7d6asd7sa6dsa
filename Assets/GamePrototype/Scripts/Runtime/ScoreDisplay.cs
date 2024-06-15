using System;
using TMPro;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    public class ScoreDisplay : MonoBehaviour
    {
        public enum ScoreDisplayType { Stage, Total };
        [SerializeField] ScoreDisplayType displayType;

        [SerializeField] TMP_Text _textField;
        TMP_Text TextField { get { if (!_textField) _textField = GetComponent<TMP_Text>(); return _textField; } }

        AppManager App => AppManager.Instance;

        private void OnEnable()
        {
            if (App.IsReady)
            {
                UpdateScoreValue(displayType == ScoreDisplayType.Stage ? App.Score.StageScore : App.Score.TotalScore);
                SubscribeEvents();
            }
            else App.onAppReady += SubscribeEvents;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnApplicationQuit()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            if (!App) return;
            App.onAppReady -= SubscribeEvents;

            if (App.Score is object)
            {
                App.Score.onScoreUpdate -= UpdateScoreValue;
                App.Score.onTotalScoreUpdate -= UpdateScoreValue;
            }
        }

        private void SubscribeEvents()
        {
            App.onAppReady -= SubscribeEvents;
            if (displayType == ScoreDisplayType.Stage)
                App.Score.onScoreUpdate += UpdateScoreValue;
            else
                App.Score.onTotalScoreUpdate += UpdateScoreValue;
        }

        private void UpdateScoreValue(int value)
        {
            TextField.text = value.ToString();
        }
    }
}
