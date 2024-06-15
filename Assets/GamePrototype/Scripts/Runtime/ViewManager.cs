using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ravenflash.GamePrototype
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField] LayoutGroup _gameplayLayout, _mainMenuLayout, _stageSummaryLayout, _gameSummaryLayout;

        Coroutine delayedDisplayCoroutine;

        private void Start()
        {
            GameEventManager.onStageCompleted += HandleStageCompleted;
            GameEventManager.onGameOver += HandleGameOver;
        }

        private void OnDestroy()
        {
            GameEventManager.onStageCompleted -= HandleStageCompleted;
            GameEventManager.onGameOver -= HandleGameOver;
        }

        internal void DisplayMainMenu(float delay = 0) => DisplayView(_mainMenuLayout, delay);
        internal void DisplayGameplay(float delay = 0) => DisplayView(_gameplayLayout, delay);
        internal void DisplayStageSummary(float delay = 0) => DisplayView(_stageSummaryLayout, delay);
        internal void DisplayGameSummary(float delay = 0) => DisplayView(_gameSummaryLayout, delay);

        private void HideAllLayouts()
        {
            _gameplayLayout.gameObject.SetActive(false);
            _mainMenuLayout.gameObject.SetActive(false);
            _stageSummaryLayout.gameObject.SetActive(false);
            _gameSummaryLayout.gameObject.SetActive(false);
        }

        private void DisplayView(LayoutGroup view, float delay)
        {
            if (delayedDisplayCoroutine is object) StopCoroutine(delayedDisplayCoroutine);
            delayedDisplayCoroutine = StartCoroutine(DisplayViewEnumerator(view, delay));
        }
        private IEnumerator DisplayViewEnumerator(LayoutGroup view, float delay)
        {
            yield return new WaitForSeconds(delay);
            HideAllLayouts();
            view.gameObject.SetActive(true);
        }

        #region Event Handlers
        private void HandleStageCompleted(int obj) => DisplayStageSummary(1);
        private void HandleGameOver() => DisplayGameSummary(1);
        #endregion
    }
}
