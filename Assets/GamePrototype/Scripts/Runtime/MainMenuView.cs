using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ravenflash.GamePrototype
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] Button _continueButton;

        private void OnEnable()
        {
            // TODO: continue button condition
             _continueButton?.gameObject.SetActive(false);
        }

    }
}
