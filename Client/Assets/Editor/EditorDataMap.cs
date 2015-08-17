using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataMap))]
public class EditorDataMap : Editor
{
	private int RoadCount = 0;
	private int ObjtCount = 0;

	void OnEnable()
	{
		if(EditorApplication.isPlaying == false)
			return;
		
		EditorApplication.update += new EditorApplication.CallbackFunction(Update);
	}
	void Update()
	{
		if(EditorApplication.isPlaying == false)
			return;

		RoadCount = Target.DataRoad.Count;
		ObjtCount = Target.DataObjt.Count;
	}
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
		GUILayout.Label(RoadCount.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Objt Count", GUILayout.Width(150.0f));
		GUILayout.Label(ObjtCount.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
	}
}