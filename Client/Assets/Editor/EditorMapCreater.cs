using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(MapCreater))]
public class EditorMapCreater : Editor
{
	private MapCreater Target
	{
		get
		{
			return (MapCreater)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;
		

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Object Count", GUILayout.Width(100.0f));
			GUILayout.Label(Target.ObjectCount().ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}
	}
}