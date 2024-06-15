using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ravenflash.GamePrototype
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] Transform _header;
        [SerializeField] Button _continueButton;

        private void OnEnable()
        {
            SaveData data = AppManager.Instance.SaveSystem.Load();
            _continueButton?.gameObject.SetActive(data.totalScore > 0);

        }

        

    }
}
