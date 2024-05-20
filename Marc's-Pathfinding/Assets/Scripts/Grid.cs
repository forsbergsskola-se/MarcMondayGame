using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridCell[] walkableGrid = new GridCell[100];

    public int width = 10;

    public bool isWalkable(int x, int y)
    {
        return walkableGrid[y*width+x].walkable;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
