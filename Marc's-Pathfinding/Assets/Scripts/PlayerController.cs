using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Collections;
using Grids;
using UnityEngine;
using Grid = Grids.Grid;

public class PlayerController : MonoBehaviour
{
    public GameObject gold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grid grid = FindObjectOfType<Grid>();
            GridCell start = grid.GetCellForPosition(transform.position);
            GridCell end = grid.GetCellForPosition(gold.transform.position);
            var path = FindPath_BreadthFirst(grid, start, end);
            foreach (var node in path)
            {
                node.spriteRenderer.color = Color.green;
            }

            StartCoroutine(Co_WalkPath(path));
        }
    }
    

    IEnumerator Co_WalkPath(IEnumerable<GridCell> path)
    {
        foreach (var cell in path)
        {
            while (Vector2.Distance(transform.position, cell.transform.position) > 0.001f)
            {
                Vector3 targetPosition = Vector2.MoveTowards(transform.position, cell.transform.position, Time.deltaTime);
                targetPosition.z = transform.position.z;
                transform.position = targetPosition;
                yield return null;
            }
        }
    }

    // Update is called once per frame
    static IEnumerable<GridCell> FindPath_DepthFirst(Grid grid, GridCell start, GridCell end)
    {
        Stack<GridCell> path = new Stack<GridCell>();
        HashSet<GridCell> visited = new HashSet<GridCell>();
        path.Push(start);
        visited.Add(start);

        while (path.Count > 0)
        {
            bool foundNextNode = false;
            foreach (var neighbor in grid.GetWalkableNeighborsForCell(path.Peek()))
            {
                if (visited.Contains(neighbor)) continue;
                path.Push(neighbor);
                visited.Add(neighbor);
                neighbor.spriteRenderer.ShiftBrightness(0.4f);
                if (neighbor == end) return path.Reverse(); // <<<<<<<<<<
                foundNextNode = true;
                break;
            }

            if (!foundNextNode)
                path.Pop();
        }

        return null;
    }
    
    static IEnumerable<GridCell> FindPath_BreadthFirst(Grid grid, GridCell start, GridCell end)
    {
        Queue<GridCell> todo = new();                         
        HashSet<GridCell> visited = new();
        todo.Enqueue(start);                                  
        visited.Add(start);
        Dictionary<GridCell, GridCell> previous = new();      

        while (todo.Count > 0)                                 
        {
            
            var current = todo.Dequeue();               
            foreach (var neighbor in grid.GetWalkableNeighborsForCell(current)) 
            {
                if (visited.Contains(neighbor)) continue;
                todo.Enqueue(neighbor); 
                previous[neighbor] = current;                  
                visited.Add(neighbor);
                neighbor.spriteRenderer.ShiftBrightness(0.4f);
                if (neighbor == end) return BuildPath(neighbor, previous).Reverse(); 
            }
            
        }

        return null;
    }
    
    static IEnumerable<GridCell> FindPath_Dijkstra(Grid grid, GridCell start, GridCell end)
    {
        PriorityQueue<GridCell> todo = new();                         
        HashSet<GridCell> visited = new();
        todo.Enqueue(start,0);
        Dictionary<GridCell, int> costs = new();
        costs[start] = 0;
        Dictionary<GridCell, GridCell> previous = new();      

        while (todo.Count > 0)                                 
        {
            
            var current = todo.Dequeue(); 
            if (current == end) return BuildPath(current, previous).Reverse(); 

            foreach (var neighbor in grid.GetWalkableNeighborsForCell(current))
            {
                int newNeighborCosts = costs[current] + neighbor.Costs;
                if (!costs.TryGetValue(neighbor, out int neighborCosts) && neighborCosts <= newNeighborCosts) continue;
                
                todo.Enqueue(neighbor, newNeighborCosts); 
                previous[neighbor] = current;                  
                costs[neighbor] = newNeighborCosts;
                neighbor.spriteRenderer.ShiftBrightness(0.4f);
            }
            
        }

        return null;
    }
    

    private static IEnumerable<GridCell> BuildPath(GridCell neighbor, Dictionary<GridCell, GridCell> previous)
    {
        while (true)
        {
            yield return neighbor;
            if (!previous.TryGetValue(neighbor, out neighbor))
                yield break;
        }
    }
}

public static class SpriteRendererExtensions
{
    public static void ShiftHue(this SpriteRenderer spriteRenderer, float hue)
    {
        Color.RGBToHSV(spriteRenderer.color, out var h, out var s, out var v);
        spriteRenderer.color = Color.HSVToRGB((h + hue)%1, s, v);
    }
    
    public static void ShiftBrightness(this SpriteRenderer spriteRenderer, float brightness)
    {
        Color.RGBToHSV(spriteRenderer.color, out var h, out var s, out var v);
        if (v > 0.5f)
        {
            spriteRenderer.color = Color.HSVToRGB(h, s, (v-brightness)%1f);
        }
        else
        {
            spriteRenderer.color = Color.HSVToRGB(h, s, (v+brightness)%1f);
        }
    }
}