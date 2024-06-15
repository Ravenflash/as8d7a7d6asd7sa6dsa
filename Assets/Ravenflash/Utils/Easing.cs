using UnityEngine;

namespace Ravenflash.Utilities
{
    public class Easing
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;
        const float c3 = c1 + 1f;
        const float c4 = (2f * Mathf.PI) / 3f;
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        // Ease Sine
        public static float EaseInSine(float x) => 1f - Mathf.Cos((x * Mathf.PI) / 2f);
        public static float EaseOutSine(float x) => Mathf.Sin((x * Mathf.PI) / 2f);
        public static float EaseInOutSine(float x) => -(Mathf.Cos(Mathf.PI * x) - 1f) / 2f;

        // Ease Quad
        public static float EaseInQuad(float x) => x * x;
        public static float EaseOutQuad(float x) => 1f - (1f - x) * (1f - x);
        public static float EaseInOutQuad(float x) => x < 0.5f ? 2f * x * x : 1f - Mathf.Pow(-2f * x + 2f, 2f) / 2f;

        // Ease Cubic
        public static float EaseInCubic(float x) => x * x * x;
        public static float EaseOutCubic(float x) => 1f - Mathf.Pow(1f - x, 3f);
        public static float EaseInOutCubic(float x) => x < 0.5f ? 4f * x * x * x : 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;

        // Ease Back
        public static float EaseInBack(float x) => c3* x * x* x - c1* x * x;
        public static float EaseOutBack(float x) => 1f + c3 * Mathf.Pow(x - 1f, 3f) + c1 * Mathf.Pow(x - 1f, 2f);
        public static float EaseInOutBack(float x) => x < 0.5f   ? (Mathf.Pow(2f * x, 2f) * ((c2 + 1f) * 2f * x - c2)) / 2f  : (Mathf.Pow(2f * x - 2f, 2f) * ((c2 + 1) * (x * 2f - 2f) + c2) + 2) / 2f;

        // Ease Elastic
        public static float EaseInElastic(float x) => x == 0 ? 0 : x == 1f ? 1f : -Mathf.Pow(2f, 10f * x - 10f) * Mathf.Sin((x * 10f - 10.75f) * c4);
        public static float EaseOutElastic(float x) => x == 0 ? 0 : x == 1f ? 1f : Mathf.Pow(2f, -10f * x) * Mathf.Sin((x * 10f - 0.75f) * c4) + 1f;

        // Ease Bounce
        public static float EaseInBounce(float x) => 1f - EaseOutBounce(1f - x);
        public static float EaseOutBounce(float x)
        {
            if (x < 1f / d1)
                return n1 * x * x;
            else if (x < 2f / d1)
                return n1 * (x -= 1.5f / d1) * x + 0.75f;
            else if (x < 2.5 / d1)
                return n1 * (x -= 2.25f / d1) * x + 0.9375f;
            else
                return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
        public static float EaseInOutBounce(float x) => x < 0.5f ? (1f - EaseOutBounce(1f - 2f * x)) / 2f : (1f + EaseOutBounce(2f * x - 1f)) / 2f;

    }
}