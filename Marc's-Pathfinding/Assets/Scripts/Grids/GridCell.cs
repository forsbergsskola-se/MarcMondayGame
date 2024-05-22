using System;

using UnityEngine;

namespace Grids
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
        public override string ToString()
        {
            return $"{nameof(GridCell)} ({Mathf.FloorToInt(transform.position.x)}|{Mathf.FloorToInt(transform.position.y)})";
        }

        public void OnValidate()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = walkable ? Color.white : Color.black;
        }
        
    }
}