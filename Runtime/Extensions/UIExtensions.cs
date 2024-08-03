using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UniTools.Extensions
{
    public static partial class UIExtensions
    {
        public static void SetColor(this Graphic image, Color color)
        {
            image.color = color;
        }

        public static void Clear(this TMP_Text tmpText)
        {
            tmpText.text = string.Empty;
        }

        public static void Bind(this Button button, Action onClick)
        {
            button.onClick.AddListener(() => onClick?.Invoke());
        }

        public static void Clear(this Button button)
        {
            button.onClick.RemoveAllListeners();
        }

        public static void ClearBind(this Button button, Action onClick)
        {
            button.Clear();
            button.Bind(onClick);
        }

        public static void SetColor(this Selectable selectable, Color color)
        {
            selectable.image.SetColor(color);
        }

        public static void Enable(this Selectable selectable)
        {
            selectable.SetInteractable(true);
        }

        public static void Disable(this Selectable selectable)
        {
            selectable.SetInteractable(false);
        }

        public static void SetInteractable(this Selectable selectable, bool isEnabled)
        {
            selectable.interactable = isEnabled;
        }

        public static void Clear(this Toggle toggle)
        {
            toggle.onValueChanged.RemoveAllListeners();
        }

        public static void Bind(this Toggle toggle, Action<bool> onClick)
        {
            toggle.onValueChanged.AddListener((isOn) => onClick?.Invoke(isOn));
        }

        public static void ClearBind(this Toggle toggle, Action<bool> onClick)
        {
            toggle.Clear();
            toggle.Bind(onClick);
        }

        public static void TurnOn(this Toggle toggle)
        {
            toggle.isOn = true;
        }

        public static void TurnOff(this Toggle toggle)
        {
            toggle.isOn = false;
        }

        public static void Turn(this Toggle toggle, bool isOn)
        {
            toggle.isOn = isOn;
        }

        public static void CopySizeAndPosition(this RectTransform to, RectTransform from)
        {
            var sourceParent = to.parent;
            var siblingIndex = to.GetSiblingIndex();

            to.SetParent(from.parent);
            to.position = from.position;
            to.sizeDelta = from.sizeDelta;
            to.anchoredPosition = from.anchoredPosition;
            to.anchorMin = from.anchorMin;
            to.anchorMax = from.anchorMax;
            to.pivot = from.pivot;
            to.localScale = from.localScale;

            to.SetParent(sourceParent, worldPositionStays: true);
            to.SetSiblingIndex(siblingIndex);
        }

        public static void SetAnchorMaxX(this RectTransform self, float value)
        {
            var anchorMax = self.anchorMax;
            anchorMax.x = value;
            self.anchorMax = anchorMax;
        }
        public static void SetAnchorMaxY(this RectTransform self, float value)
        {
            var anchorMax = self.anchorMax;
            anchorMax.y = value;
            self.anchorMax = anchorMax;
        }
        public static void SetAnchorMinX(this RectTransform self, float value)
        {
            var anchorMin = self.anchorMin;
            anchorMin.x = value;
            self.anchorMin = anchorMin;
        }
        public static void SetAnchorMinY(this RectTransform self, float value)
        {
            var anchorMin = self.anchorMin;
            anchorMin.y = value;
            self.anchorMin = anchorMin;
        }
    }
}