using UnityEngine;

namespace UniTools.Extensions
{
    public static class GameObjectExtensions
    {
        public static void Show(this GameObject self) => self.SetActive(true);
        public static void Hide(this GameObject self) => self.SetActive(false);

        public static void Show(this Component self) => self.SetActive(true);
        public static void Hide(this Component self) => self.SetActive(false);

        public static T Show<T>(this T self) where T : Component
        {
            self.SetActive(true);
            return self;
        }

        public static T Hide<T>(this T self) where T : Component
        {
            self.SetActive(false);
            return self;
        }

        public static void SetActive(this Component self, bool isActive) => self.gameObject.SetActive(isActive);

        public static void DisableComponent(this Behaviour behaviour)
        {
            behaviour.SetComponentEnabled(false);
        }

        public static void EnableComponent(this Behaviour behaviour)
        {
            behaviour.SetComponentEnabled(true);
        }

        public static void SetComponentEnabled(this Behaviour behaviour, bool isEnabled)
        {
            behaviour.enabled = isEnabled;
        }
        
        public static void DestroyChildren(this Transform self)
        {
            foreach (Transform item in self)
            {
                Object.Destroy(item.gameObject);
            }
        }
    }
}