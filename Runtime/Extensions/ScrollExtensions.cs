// Copyright (c) 2021 Süleyman Yasir Kula
// License (MIT): https://gist.github.com/yasirkula/75ca350fb83ddcc1558d33a8ecf1483f?permalink_comment_id=4870142#gistcomment-4870142

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UniTools.Extensions
{
    public static partial class UIExtensions
    {
        private static bool _cancelOutPreviousCoroutine;
        private static int _runningLerpCoroutine;

        public static void FocusAtPoint(this ScrollRect scrollView, Vector2 focusPoint)
        {
            scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(focusPoint);
        }

        public static void FocusOnItem(this ScrollRect scrollView, RectTransform item)
        {
            scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(item);
        }

        private static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, Vector2 focusPoint)
        {
            var contentSize = scrollView.content.rect.size;

            // since scrollView.viewport can be null, get content.parent instead:
            var viewportSize = ((RectTransform) scrollView.content.parent).rect.size;
            Vector2 contentScale = scrollView.content.localScale;

            contentSize.Scale(contentScale);
            focusPoint.Scale(contentScale);

            var scrollPosition = scrollView.normalizedPosition;
            if (scrollView.horizontal && contentSize.x > viewportSize.x)
                scrollPosition.x = Mathf.Clamp01((focusPoint.x - viewportSize.x * 0.5f) / (contentSize.x - viewportSize.x));
            if (scrollView.vertical && contentSize.y > viewportSize.y)
                scrollPosition.y = Mathf.Clamp01((focusPoint.y - viewportSize.y * 0.5f) / (contentSize.y - viewportSize.y));

            return scrollPosition;
        }

        private static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, RectTransform item)
        {
            Vector2 itemCenterPoint = scrollView.content.InverseTransformPoint(item.transform.TransformPoint(item.rect.center));

            var contentSizeOffset = scrollView.content.rect.size;
            contentSizeOffset.Scale(scrollView.content.pivot);

            return scrollView.CalculateFocusedScrollPosition(itemCenterPoint + contentSizeOffset);
        }

        private static IEnumerator LerpToScrollPositionCoroutine(this ScrollRect scrollView, Vector2 targetNormalizedPos, float speed)
        {
            var initialNormalizedPos = scrollView.normalizedPosition;

            var t = 0.0f;
            while (t < 1.0f)
            {
                scrollView.normalizedPosition = Vector2.LerpUnclamped(
                    initialNormalizedPos,
                    targetNormalizedPos,
                    1.0f - (1.0f - t) * (1.0f - t));

                yield return null;
                t += speed * Time.unscaledDeltaTime;

                if (_cancelOutPreviousCoroutine)
                {
                    t = 1.0f;
                    _cancelOutPreviousCoroutine = false;
                }

                if (t < 1.0f)
                {
                    scrollView.normalizedPosition = targetNormalizedPos;
                }
            }

            _runningLerpCoroutine--;
        }

        public static IEnumerator FocusAtPointCoroutine(this ScrollRect scrollView, Vector2 focusPoint, float speed)
        {
            if (_runningLerpCoroutine > 0)
                _cancelOutPreviousCoroutine = true;
            _runningLerpCoroutine++;

            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(focusPoint), speed);
        }

        public static IEnumerator FocusOnItemCoroutine(this ScrollRect scrollView, RectTransform item, float speed)
        {
            if (_runningLerpCoroutine > 0)
                _cancelOutPreviousCoroutine = true;
            _runningLerpCoroutine++;

            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(item), speed);
        }
    }
}