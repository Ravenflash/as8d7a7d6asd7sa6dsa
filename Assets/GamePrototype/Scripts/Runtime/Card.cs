using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Ravenflash.GamePrototype
{
    public class Card : MonoBehaviour, IFlippable, IEquatable<Card>
    {
        const float SCALE_ZOOMED = 1.1f;

        [SerializeField] SpriteAtlas _atlas;
        [SerializeField] string _reverseSpriteName;
        [SerializeField] float animationDuration = .2f;

        Coroutine _animationCoroutine;
        Coroutine _updateImageCoroutine;

        #region Properties
        [field: SerializeField]
        public string SpriteName { get; set; }

        [Header("Optional")]
        [SerializeField] Camera _camera;
        Camera Camera { get { if (!_camera) _camera = Camera.main; return _camera; } }

        [SerializeField] Button _button;
        Button Button { get { if (!_button) _button = GetComponent<Button>(); return _button; } }

        [SerializeField] Image _image;
        Image Image { get { if (!_image) _image = GetComponent<Image>(); return _image; } }

        public bool IsClickable { get => Button.interactable; set => Button.interactable = value; }
        public bool IsActiveAndClickable
        {
            get => IsClickable && Image.isActiveAndEnabled;
            set
            {
                IsClickable = value;
                Image.enabled = value;
            }
        }

        public bool IsFacingCamera => Vector3.Dot(transform.forward, Camera.transform.forward) < 0;
        #endregion

        void Start()
        {
            SetSprite(_reverseSpriteName);

            _updateImageCoroutine = StartCoroutine(UpdateImage());
        }

        public void Flip()
        {
            if (_animationCoroutine is object) StopCoroutine(_animationCoroutine);
            _animationCoroutine = StartCoroutine(FlipAnimation(animationDuration));
        }

        public void Unflip(float delay = 0)
        {
            if (_animationCoroutine is object) StopCoroutine(_animationCoroutine);
            _animationCoroutine = StartCoroutine(UnflipAnimation(animationDuration, delay));
        }

        public void Hide()
        {
            IsActiveAndClickable = false;
        }

        public bool Equals(Card other)
        {
            return other.SpriteName == SpriteName;
        }


        private void SetSprite(string spriteName)
        {
            try
            {
                Image.sprite = _atlas.GetSprite(spriteName);
            }
            catch { throw; }
        }


        #region Animations
        IEnumerator FlipAnimation(float duration)
        {
            float progress = 0;
            Vector3 finalScale = SCALE_ZOOMED * Vector3.one;
            Quaternion finalRotation = Quaternion.Euler(0, 180f, 0);

            GameEventManager.InvokeCardSelected(this);

            while (this && duration > 0 && progress < 1f)
            {
                transform.localScale = Vector3.Lerp(Vector3.one, finalScale, progress);
                transform.rotation = Quaternion.Lerp(Quaternion.identity, finalRotation, progress);
                progress += Time.deltaTime / duration;
                yield return null;
            }
            transform.localScale = SCALE_ZOOMED * Vector3.one;

            GameEventManager.InvokeCardFlipped(this);
            //Unflip(2f);
        }

        IEnumerator UnflipAnimation(float duration, float delay = 0)
        {
            float progress = 0;
            Vector3 startScale = SCALE_ZOOMED * Vector3.one;
            Quaternion startRotation = Quaternion.Euler(0, 180f, 0);

            if (delay > 0) yield return new WaitForSeconds(delay);

            while (this && duration > 0 && progress < 1f)
            {
                transform.localScale = Vector3.Lerp(startScale, Vector3.one, progress);
                transform.rotation = Quaternion.Lerp(startRotation, Quaternion.identity, progress);
                progress += Time.deltaTime / duration;
                yield return null;
            }
            transform.localScale = Vector3.one;

        }
        IEnumerator UpdateImage()
        {
            while (this)
            {
                SetSprite(IsFacingCamera ? SpriteName : _reverseSpriteName);
                yield return null;
            }
        }

        #endregion

    }
}
