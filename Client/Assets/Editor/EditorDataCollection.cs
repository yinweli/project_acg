using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataCollection))]
public class EditorDataCollection : Editor
{
	private int Collection = 0;

	private bool ShowData = false;
	
	private DataCollection Target
	{
		get
		{
			return (DataCollection)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;

		// show edit area
		{
			GUILayout.BeginHorizontal("box");
			
			if(GUILayout.Button("Set", GUILayout.Width(60.0f)))
				Target.Data.Add(Collection);

			if(GUILayout.Button("Del", GUILayout.Width(60.0f)))
				Target.Data.Remove(Collection);
			
			Collection = EditorGUILayout.IntField(Collection, GUILayout.Width(150.0f));
			
			GUILayout.EndHorizontal();
		}

		ShowData = EditorGUILayout.Toggle("Show Collection", ShowData);
		
		if(ShowData == false)
			return;
		
		// show content
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("ItemID", GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		foreach(int Itor in Target.Data)
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(Itor.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}//for
	}
}