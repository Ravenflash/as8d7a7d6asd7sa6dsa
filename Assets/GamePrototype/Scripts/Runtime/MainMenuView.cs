using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ravenflash.GamePrototype
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] Button _continueButton;

        private void Start()
        {
            SaveData data = AppManager.Instance.SaveSystem.Load();

            Debug.Log($"Saved stage {data.stageId}, score {data.totalScore}");
            _continueButton?.gameObject.SetActive(data.totalScore > 0);
        }

    }
}
