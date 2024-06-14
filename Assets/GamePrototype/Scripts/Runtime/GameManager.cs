using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ravenflash.GamePrototype
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup _layout;
        [SerializeField] Card _cardPrefab;


        List<Card> _cards;
        Queue<Card> _queue;

        #region Unity Methods
        private void OnDestroy()
        {
            EndGame();
        }
        #endregion

        #region Public Methods
        public void NewGame()
        {
            // Setup Layout
            _layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _layout.constraintCount = 3;

            try
            {
                // Spawn Cards
                RemoveAllChildren(_layout.transform);
                SpawnCards(12);

                // Start Game
                StartGame();

                // Score 
                // Complete Level
            }
            catch { throw; }
        }
        #endregion

        #region Private Methods
        private void StartGame()
        {
            _queue = new Queue<Card>();
            GameEventManager.onCardSelected += HandleCardSelected;
            GameEventManager.onCardFlipped += HandleCardFlipped;

            foreach (var card in _cards) { card.IsActiveAndClickable = true; }
        }
        private void EndGame()
        {
            GameEventManager.onCardSelected -= HandleCardSelected;
            GameEventManager.onCardFlipped -= HandleCardFlipped;
        }

        private void SpawnCards(int cardsCount)
        {
            _cards = new List<Card>();
            Card card;
            for (int i = 0; i < cardsCount; i++)
            {
                card = Instantiate(_cardPrefab, _layout.transform);

                card.IsClickable = false;
                _cards.Add(card);
            }
        }

        private void RemoveAllChildren(Transform parentTransform)
        {
            for (int i = 0; i < parentTransform.childCount; i++)
                Destroy(parentTransform.GetChild(i).gameObject);
        }

        private void CompareLastTwoCards()
        {
            Card c1 = _queue.Dequeue();
            Card c2 = _queue.Dequeue();

            if (c1.Equals(c2))
            {
                c1.Hide();
                c2.Hide();
            }
            else
            {
                StartCoroutine(UnflipDelayed(c1, c2));
            }
        }
        #endregion

        #region Event Handlers
        private void HandleCardSelected(Card card)
        {
            Debug.Log($"Card {card.name} Selected.");
        }

        private void HandleCardFlipped(Card card)
        {
            Debug.Log($"Card {card.name} Displayed.");
            _queue.Enqueue(card);
            if (_queue.Count >= 2) CompareLastTwoCards();
        }
        private IEnumerator UnflipDelayed(Card c1, Card c2)
        {
            float startTime = Time.timeSinceLevelLoad;
            while (this && Time.timeSinceLevelLoad - startTime < 2f)
            {
                yield return null;
            }
            c1.Unflip();
            c2.Unflip();
        }
        #endregion

    }
}
