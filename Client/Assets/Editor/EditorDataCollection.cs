using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataCollection))]
public class EditorDataCollection : Editor
{
	private int DataSet = 0;
	private int DataDel = 0;

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

		// show set area
		{
			GUILayout.BeginHorizontal("box");
			
			if(GUILayout.Button("Set", GUILayout.Width(60.0f)))
				Target.Data.Add(DataSet);
			
			DataSet = EditorGUILayout.IntField(DataSet);
			
			GUILayout.EndHorizontal();
		}
		
		// show del area
		{
			GUILayout.BeginHorizontal("box");
			
			if(GUILayout.Button("Del", GUILayout.Width(60.0f)))
				Target.Data.Remove(DataDel);
			
			DataDel = EditorGUILayout.IntField(DataDel);
			
			GUILayout.EndHorizontal();
		}

		ShowData = EditorGUILayout.Toggle("Show Collection", ShowData);
		
		if(ShowData == false)
			return;
		
		// show content
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("ItemID", GUILayout.Width(300.0f));
		GUILayout.EndHorizontal();

		foreach(int Itor in Target.Data)
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(Itor.ToString(), GUILayout.Width(300.0f));
			GUILayout.EndHorizontal();
		}//for
	}
}