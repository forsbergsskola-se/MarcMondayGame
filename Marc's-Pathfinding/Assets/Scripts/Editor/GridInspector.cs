using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace Editor
{

    [CustomEditor(typeof(Grid))]
    public class GridInspector : UnityEditor.Editor
    {
        private UnityEngine.Object cellPrefab;
        
        
        // all the blocks
       public override void OnInspectorGUI() // override = adding more elements to the inspector
       {
           base.OnInspectorGUI(); // this is the base inspector that you always find the unity
           cellPrefab = EditorGUILayout.ObjectField("Cell Prefab", cellPrefab, typeof(GridCell));
           EditorGUI.BeginDisabledGroup(cellPrefab==null);
           
           if (GUILayout.Button("Generate Grid")) // Generate Grid button in the inspector
           {
               Grid grid = target as Grid;
              
               
               
               foreach (var gridCell in grid.walkableGrid)
               {
                   if (gridCell!=null)
                       DestroyImmediate(gridCell);
               }
               
               EditorUtility.SetDirty(grid);
               int i = 0;
               for (int x = 0; x< grid.width; x++)
               {
                   
                   for (int y = 0; y < grid.walkableGrid.Length / grid.width; y++)
                   {
                       GridCell cell  = (GridCell)PrefabUtility.InstantiatePrefab(cellPrefab,grid.transform);
                        cell.transform.position= new Vector3(x, y, 0);

                        grid.walkableGrid[i++] = cell;

                   }
                   
               }
           }

           EditorGUI.EndDisabledGroup();
           GUI.enabled = true; // reset


       }  
    } 
}

