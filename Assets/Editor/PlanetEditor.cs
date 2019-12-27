using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor {

  public override void OnInspectorGUI() {
    var planet = (Planet)target;
    if (DrawDefaultInspector()) {
      if (planet.autoUpdate) {
        planet.ConstructMesh();
      }
    }

    if (GUILayout.Button("Generate Planet")) {
      planet.ConstructMesh();
    }
  }
}
