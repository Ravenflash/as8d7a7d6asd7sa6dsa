using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Ravenflash.GamePrototype
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup _layout;
        [SerializeField] Card _cardPrefab;
        [SerializeField] GameManagerSettings _settings;


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
            try
            {
                // Setup Layout
                _layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                _layout.constraintCount = _settings.layouts[0].cols;

                // Spawn Cards
                RemoveAllChildren(_layout.transform);
                SpawnCards(_settings.layouts[0].CardCount);

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
            if (cardsCount % 2f != 0) throw new Exception("Card Count can't be odd! You need pairs.");

            List<string> allCardNames = _settings.cardNames.ToList();
            List<string> randomCardPairs = new List<string>();
            string[] shuffledCardNames = new string[cardsCount];

            int rand;
            for (int i = 0; i < cardsCount / 2; i++)
            {
                rand = Random.Range(0, allCardNames.Count);
                randomCardPairs.Add(allCardNames[rand]);
                randomCardPairs.Add(allCardNames[rand]);
                allCardNames.RemoveAt(rand);
            }

            for (int i = 0; i < cardsCount; i++)
            {
                rand = Random.Range(0, randomCardPairs.Count);
                shuffledCardNames[i] = randomCardPairs[rand];
                randomCardPairs.RemoveAt(rand);
            }

            _cards = new List<Card>();
            Card card;
            for (int i = 0; i < shuffledCardNames.Length; i++)
            {
                card = Instantiate(_cardPrefab, _layout.transform);

                card.SpriteName = shuffledCardNames[i];
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
            while (this && Time.timeSinceLevelLoad - startTime < _settings.cardDisplayDuration)
            {
                yield return null;
            }
            c1.Unflip();
            c2.Unflip();
        }
        #endregion

    }
}
