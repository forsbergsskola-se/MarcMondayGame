using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace Editor
{

    [CustomEditor(typeof(Grid))]
    public class GridInspector : UnityEditor.Editor
    {
       public override void OnInspectorGUI()
       {
           base.OnInspectorGUI();
           if (GUILayout.Button("Generate Grid"))
           {
               Grid grid = target as Grid;
               grid.width = 99;
               EditorUtility.SetDirty(grid);
           }
           
           
       }  
    } 
}

