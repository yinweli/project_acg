using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(CtrlPanel))]
public class EditorCtrlPanel : Editor
{
	private CtrlPanel Target
	{
		get
		{
			return (CtrlPanel)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;
		
		if(GUILayout.Button("Clear Data", GUILayout.Width(200.0f)))
			Target.ClearData();

		if(GUILayout.Button("Clear Achievement", GUILayout.Width(200.0f)))
			Target.ClearAchievement();

		if(GUILayout.Button("Clear Collection", GUILayout.Width(200.0f)))
			Target.ClearCollection();

		if(GUILayout.Button("Clear Record", GUILayout.Width(200.0f)))
			Target.ClearRecord();

		if(GUILayout.Button("Clear Reward", GUILayout.Width(200.0f)))
			Target.ClearReward();
	}
}
