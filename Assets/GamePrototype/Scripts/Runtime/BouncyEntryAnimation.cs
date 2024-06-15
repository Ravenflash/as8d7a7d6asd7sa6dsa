using Ravenflash.Utilities;
using System.Collections;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    public class BouncyEntryAnimation : MonoBehaviour
    {
        [SerializeField] float _duration = 1f;

        private void OnEnable()
        {
            StartCoroutine(HeaderAnimation(_duration));
        }
        private IEnumerator HeaderAnimation(float duration = 1f)
        {
            Vector3 startScale = new Vector3(1.2f, 1.05f, 1f);
            float progress = 0;

            while (transform && progress < 1f && duration >0)
            {
                progress += Time.deltaTime / duration;
                transform.localScale = Vector3.LerpUnclamped(startScale, Vector3.one, Easing.EaseOutElastic(progress));
                yield return null;
            }
            transform.localScale = Vector3.one;
        }
    }
}
