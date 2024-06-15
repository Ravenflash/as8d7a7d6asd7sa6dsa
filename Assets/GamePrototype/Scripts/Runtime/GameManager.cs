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
        WaitForSeconds _cardStartingFlipInterval = new WaitForSeconds(.12f);
        WaitForSeconds _cardStartingUnflipInterval = new WaitForSeconds(.01f);

        public int CurrentStageId { get; internal set; } = 0;

        #region Unity Methods
        private void OnDestroy()
        {
            EndGame();
        }
        #endregion

        #region Public Methods
        internal void StartNewGame()
        {
            try
            {
                SetupStage(0);
                StartCoroutine(StartingAnimation());
                //StartGame();
            }
            catch { throw; }
        }

        internal void StartNextStage()
        {
            try
            {
                if (_cards is object && _cards.Count > 0) throw new Exception("Can't start next stage. Current stage is in progress.");
                SetupStage(CurrentStageId+1);
                StartCoroutine(StartingAnimation());
                //StartGame();
            }
            catch { throw; }
        }

        #endregion

        #region Private Methods
        private void SetupStage(int stageId)
        {
            CurrentStageId = stageId;
            CardLayout cardLayout = GetCardLayout(stageId);

            // Setup Layout
            _layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _layout.constraintCount = cardLayout.cols;

            // Spawn Cards
            RemoveAllChildren(_layout.transform);
            SpawnCards(cardLayout.CardCount);
            GameEventManager.InvokeStageStarted(CurrentStageId);
        }

        private CardLayout GetCardLayout(int stageId)
        {
            // Return CardLayout based on stageId or Loop the last CardLayout
            return stageId >= _settings.layouts.Length ? _settings.layouts[_settings.layouts.Length - 1] : _settings.layouts[stageId];
        }

        private void StartGame()
        {
            _queue = new Queue<Card>();
            GameEventManager.onCardSelected += HandleCardSelected;
            GameEventManager.onCardFlipped += HandleCardFlipped;
        }

        private void EndGame()
        {
            GameEventManager.onCardSelected -= HandleCardSelected;
            GameEventManager.onCardFlipped -= HandleCardFlipped;
        }

        private void SpawnCards(int cardsCount)
        {
            if (cardsCount % 2f != 0) throw new Exception("Card Count can't be odd! You need pairs.");
            if (cardsCount > _settings.cardNames.Length * 2f) throw new Exception("Not enought card types. Need more Sprites in the atlas.");

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
                GameEventManager.InvokeMatchSuccess();
                c1.Hide();
                c2.Hide();
                _cards.Remove(c1);
                _cards.Remove(c2);
                if (_cards.Count <= 0) StageComplete();
            }
            else
            {
                GameEventManager.InvokeMatchFailed();
                StartCoroutine(UnflipDelayed(c1, c2));
            }
        }

        private void StageComplete()
        {
            Debug.Log("Level Complete");
            EndGame();
            GameEventManager.InvokeStageCompleted(CurrentStageId);
            //StartNextStage();
        }

        #endregion

        #region Animations
        private IEnumerator StartingAnimation()
        {
            foreach (var card in _cards) { card.IsActiveAndClickable = true; card.IsClickable = false; }

            List<Card> allCards = new List<Card>(_cards);
            Card[] shuffledCards = new Card[allCards.Count];
            for (int i = 0; i < shuffledCards.Length; i++)
            {
                int rand = Random.Range(0, allCards.Count);
                shuffledCards[i] = allCards[rand];
                allCards.RemoveAt(rand);
            }

            yield return null;

            foreach (var card in shuffledCards)
            {
                card.Flip();
                yield return _cardStartingFlipInterval;
            }
            yield return new WaitForSeconds(_settings.cardDisplayDuration);
            foreach (var card in shuffledCards)
            {
                card.Unflip();
                yield return _cardStartingUnflipInterval;
            }
            foreach (var card in _cards) { card.IsActiveAndClickable = true; }

            StartGame();
        }
        #endregion

        #region Event Handlers
        private void HandleCardSelected(Card card)
        {
            //Debug.Log($"Card {card.name} Selected.");
        }

        private void HandleCardFlipped(Card card)
        {
            //Debug.Log($"Card {card.name} Displayed.");
            _queue.Enqueue(card);
            if (_queue.Count >= 2) CompareLastTwoCards();
        }
        private IEnumerator UnflipDelayed(Card c1, Card c2)
        {
            float startTime = Time.timeSinceLevelLoad;
            while (this && Time.timeSinceLevelLoad - startTime < _settings.cardDisplayDuration && _queue.Count <= 0)
            {
                yield return null;
            }
            c1.Unflip();
            c2.Unflip();
        }
        #endregion

    }
}
