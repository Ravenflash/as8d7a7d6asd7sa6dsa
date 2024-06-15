using UnityEngine;

namespace Ravenflash.GamePrototype
{
    [CreateAssetMenu(fileName = "New Sound Settings", menuName = "Ravenflash/Sound Settings", order = 20)]
    public class SoundSettings : ScriptableObject
    {
        [Header("Sound Effects")]
        [SerializeField] AudioClip _flipSfx;
        [SerializeField] AudioClip _matchSfx;
        [SerializeField] AudioClip _errorSfx;

        [Header("Music")]
        [SerializeField] AudioClip _completeMusic;
        [SerializeField] AudioClip _gameoverMusic;

        public AudioClip FlipSfx { get { return _flipSfx; } }
        public AudioClip MatchSfx { get { return _matchSfx; } }
        public AudioClip ErrorSfx { get { return _errorSfx; } }
        public AudioClip CompleteMusic { get { return _completeMusic; } }
        public AudioClip GameoverMusic { get {  return _gameoverMusic; } }



    }
}
