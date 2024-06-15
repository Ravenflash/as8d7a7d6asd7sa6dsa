using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    public class StageNumberDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text _textField;
        TMP_Text TextField { get { if (!_textField) _textField = GetComponent<TMP_Text>(); return _textField; } }

        private void OnEnable()
        {
            GameEventManager.onStageStarted += UpdateDisplay;
        }

        private void OnDisable()
        {
            GameEventManager.onStageStarted -= UpdateDisplay;
        }

        private void UpdateDisplay(int stageId)
        {
            TextField.text = (1 + stageId).ToString();
        }
    }
}
