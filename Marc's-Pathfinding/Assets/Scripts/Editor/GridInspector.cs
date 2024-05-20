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
        public UnityEngine.Object cellPrefab;

        private int arrayLength;
        
        // all the blocks
       public override void OnInspectorGUI() // override = adding more elements to the inspector
       {
           base.OnInspectorGUI(); // this is the base inspector that you always find the unity
           cellPrefab = EditorGUILayout.ObjectField("Cell Prefab", cellPrefab, typeof(GridCell));
           EditorGUI.BeginDisabledGroup(cellPrefab==null);
           
           if (GUILayout.Button("Generate Grid")) // Generate Grid button in the inspector
           {
               Grid grid = target as Grid;
               int height = arrayLength / grid.width;
               GridCell [] cellArray = new GridCell [arrayLength];
               
               foreach (var gridCell in grid.walkableGrid)
               {
                   if (gridCell!=null)
                       DestroyImmediate(gridCell);
               }
               
               EditorUtility.SetDirty(grid);
               for (int i = 0; i < grid.width; i++)
               {
                   
                   for (int j = 0; j < height; j++)
                   {
                       GridCell cell  = (GridCell)PrefabUtility.InstantiatePrefab(cellPrefab);
                        cell.transform.position= new Vector3(i, j, 0);

                       bool index = grid.isWalkable(i,j);
                       
                   }
                   
               }
           }

           EditorGUI.EndDisabledGroup();
           GUI.enabled = true; // reset


       }  
    } 
}

