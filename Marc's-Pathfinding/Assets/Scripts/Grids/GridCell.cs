using System;

using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridCell : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public bool walkable;

        private void Start()
        {
            OnValidate();
        }

        public void OnValidate()
        {
            spriteRenderer.color = walkable ? Color.white : Color.black;
        }
        
    }
}