using System;
using UnityEngine;

namespace Grids{
    public class Grid : MonoBehaviour
    {
        public GridCell[] walkableGrid = new GridCell[100];

        public int width = 10;

        public bool isWalkable(int x, int y)
        {
            return walkableGrid[y*width+x].walkable;
        }

        public Vector2Int GetCellIndexForPosition(Vector3 position)
        {
            return new Vector2Int(
                Mathf.FloorToInt(position.x + 0.5f),
                Mathf.FloorToInt(position.y + 0.5f)
            );
        }

        public GridCell GetCellForPosition(Vector3 position)
        {
            var index = GetCellIndexForPosition(position);
            
            return walkableGrid[index.x + index.y * width];

            throw new NotImplementedException();
        }
       
    }
}
