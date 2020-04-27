using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProceduralBuildingGenerator))]
public class ProceduralBuildingGeneratorEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        ProceduralBuildingGenerator targetScript = (ProceduralBuildingGenerator)target;

        if (GUILayout.Button("Generate Building")) {
            targetScript.GenerateBuildingForLevel(0, 7);
        }

    }
}
