using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    public static class GameEventManager
    {
        // Card Events
        public static event Action<Card> onCardSelected;
        public static event Action<Card> onCardFlipped;

        public static void InvokeCardSelected(Card card) => onCardSelected?.Invoke(card);
        public static void InvokeCardFlipped(Card card) => onCardFlipped?.Invoke(card);

        // Gameplay Events
        public static event Action onMatchSuccess, onMatchFailed;
        public static event Action onNewGameStarted, onGameOver;
        public static event Action<int> onStageStarted, onStageCompleted;

        public static void InvokeNewGameStarted() => onNewGameStarted?.Invoke();
        public static void InvokeStageStarted(int stageId) => onStageStarted?.Invoke(stageId);
        public static void InvokeStageCompleted(int stageId) => onStageCompleted?.Invoke(stageId);
        public static void InvokeMatchSuccess() => onMatchSuccess?.Invoke();
        public static void InvokeMatchFailed() => onMatchFailed?.Invoke();
        public static void InvokeGameOver() => onGameOver?.Invoke();
    }
}
