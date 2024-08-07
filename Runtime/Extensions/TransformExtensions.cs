using System.Collections.Generic;
using UnityEngine;

namespace UniTools.Extensions
{
    public static class TransformExtensions
    {
        public static IEnumerable<Transform> GetChildren(this Transform parent)
        {
            foreach (Transform child in parent)
            {
                yield return child;
            }
        }
        
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        public static void DestroyChildren(this Transform parent)
        {
            parent.ForEachChild(child => Object.Destroy(child.gameObject));
        }
        
        public static void DestroyChildrenImmediate(this Transform parent)
        {
            parent.ForEachChild(child => Object.DestroyImmediate(child.gameObject));
        }
        
        public static void ShowChildren(this Transform parent)
        {
            parent.ForEachChild(child => child.Show());
        }
        
        public static void HideChildren(this Transform parent)
        {
            parent.ForEachChild(child => child.Hide());
        }
        
        public static void ForEachChild(this Transform parent, System.Action<Transform> action)
        {
            for (var i = parent.childCount - 1; i >= 0; i--)
            {
                action(parent.GetChild(i));
            }
        }
    }
}