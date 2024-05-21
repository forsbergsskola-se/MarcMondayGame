using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grid grid = FindObjectOfType<Grid>();
            GridCell start = null;
            GridCell end = null;
            var path = FindPath(grid, start, end);
            // start coroutine
            //     traverse the path
        }
    }

    // Update is called once per frame
    static IEnumerable<GridCell> FindPath(Grid grid, GridCell start, GridCell end)
    {
        // track visited cells
        // track cells that need to be visited
        // track the goal
        // decide what cells to visit next
        
        // if current cell == goal
        // reconstruct path
        // return path
        throw new NotImplementedException();
    }
}