using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataRecord))]
public class EditorDataRecord : Editor
{
	private bool ShowData = false;
	
	private DataRecord Target
	{
		get
		{
			return (DataRecord)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;

		ShowData = EditorGUILayout.Toggle("Show Record", ShowData);
		
		if(ShowData == false)
			return;

		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Stage", GUILayout.Width(80.0f));
		GUILayout.Label("GameTime", GUILayout.Width(80.0f));
		GUILayout.Label("Kill", GUILayout.Width(80.0f));
		GUILayout.Label("Lost", GUILayout.Width(80.0f));
		GUILayout.Label("Time", GUILayout.Width(80.0f));
		GUILayout.EndHorizontal();
		
		// show content
		foreach(SaveRecord Itor in Target.Data)
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(Itor.iStage.ToString(), GUILayout.Width(80.0f));
			GUILayout.Label(Itor.iPlayTime.ToString(), GUILayout.Width(80.0f));
			GUILayout.Label(Itor.iEnemyKill.ToString(), GUILayout.Width(80.0f));
			GUILayout.Label(Itor.iPlayerLost.ToString(), GUILayout.Width(80.0f));
			GUILayout.Label(Itor.szTime, GUILayout.Width(80.0f));
			GUILayout.EndHorizontal();
		}//for
	}
}