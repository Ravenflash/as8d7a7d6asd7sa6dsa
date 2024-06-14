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
        public static event Action onStageCompleted;

        public static void InvokeStageCompleted() => onStageCompleted?.Invoke();
    }
}
