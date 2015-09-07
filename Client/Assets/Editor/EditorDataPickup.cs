using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataPickup))]
public class EditorDataPickup : Editor
{
	private bool ShowData = false;

	private DataPickup Target
	{
		get
		{
			return (DataPickup)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;
		
		// show content
		ShowData = EditorGUILayout.Toggle("Show Pickup", ShowData);
			
		if(ShowData == false)
			return;

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("PosX", GUILayout.Width(60.0f));
			GUILayout.Label("PosY", GUILayout.Width(60.0f));
			GUILayout.Label("Type", GUILayout.Width(60.0f));
			GUILayout.Label("Count", GUILayout.Width(60.0f));
			GUILayout.Label("Looks", GUILayout.Width(60.0f));
			GUILayout.Label("Pickup", GUILayout.Width(60.0f));
			GUILayout.EndHorizontal();
		}
		
		foreach(Pickup Itor in Target.Data)
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(Itor.Pos.X.ToString(), GUILayout.Width(60.0f));
			GUILayout.Label(Itor.Pos.Y.ToString(), GUILayout.Width(60.0f));
			GUILayout.Label(((ENUM_Pickup)Itor.iType).ToString(), GUILayout.Width(60.0f));
			GUILayout.Label(Itor.iCount.ToString(), GUILayout.Width(60.0f));
			GUILayout.Label(Itor.iLooks.ToString(), GUILayout.Width(60.0f));
			GUILayout.Label(Itor.bPickup.ToString(), GUILayout.Width(60.0f));
			GUILayout.EndHorizontal();
		}//for
	}
}
