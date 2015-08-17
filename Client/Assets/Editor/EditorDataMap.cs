using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataMap))]
public class EditorDataMap : Editor
{
	private DataMap Target
	{
		get
		{
			return (DataMap)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;

		// show content
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Road Count", GUILayout.Width(150.0f));
		GUILayout.Label(Target.DataRoad.Count.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Objt Count", GUILayout.Width(150.0f));
		GUILayout.Label(Target.DataObjt.Count.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
	}
}