using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] SoundSettings _soundSettings;
        [SerializeField, Range(0,1)] float _volume = 1f;

        // Note: So we want to have one Audio source for every sound clip. 
        // This way the audio source will not be cut when another clip is starting to be played (unless it's the same one).
        // Otherwise music might be cut by card flipping and that feels bad.
        Dictionary<string,AudioSource> _audioDictionary;

        private void Start()
        {
            _audioDictionary = new Dictionary<string,AudioSource>();

            GameEventManager.onCardSelected += HandleCarSelected;
            GameEventManager.onMatchSuccess += HandleMatchSuccess;
            GameEventManager.onMatchFailed += HandleMatchFailed;
            GameEventManager.onStageCompleted += HandleStageCompleted;
            GameEventManager.onGameOver += HandleGameOver;
        }

        private void OnDestroy()
        {
            GameEventManager.onCardSelected -= HandleCarSelected;
            GameEventManager.onMatchSuccess -= HandleMatchSuccess;
            GameEventManager.onMatchFailed -= HandleMatchFailed;
            GameEventManager.onStageCompleted -= HandleStageCompleted;
            GameEventManager.onGameOver -= HandleGameOver;
        }
        void PlayAudio(AudioClip clip)
        {
            if(!_audioDictionary.TryGetValue(clip.name, out var audioSource))
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.loop = false;
                audioSource.clip = clip;
                _audioDictionary[clip.name] = audioSource;
            }

            audioSource.volume = _volume;
            audioSource.Play();
        }

        private void HandleCarSelected(Card card) => PlayAudio(_soundSettings?.FlipSfx);
        private void HandleMatchSuccess() => PlayAudio(_soundSettings?.MatchSfx);
        private void HandleGameOver() => PlayAudio(_soundSettings?.CompleteMusic);
        private void HandleStageCompleted(int stageId) => PlayAudio(_soundSettings?.CompleteMusic);
        private void HandleMatchFailed() => PlayAudio(_soundSettings?.ErrorSfx);

        
    }
}
