﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataAchievement))]
public class EditorDataAchievement : Editor
{
	private SaveAchievement DataSet = new SaveAchievement();
	private SaveAchievement DataDel = new SaveAchievement();

	private bool ShowData = false;

	private DataAchievement Target
	{
		get
		{
			return (DataAchievement)target;
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
			{
				if(Target.Data.ContainsKey(DataSet.Key))
					Target.Data[DataSet.Key] = DataSet.Value;
				else
					Target.Data.Add(DataSet.Key, DataSet.Value);
			}//if
			
			DataSet.Key = (int)(ENUM_Achievement)EditorGUILayout.EnumPopup((ENUM_Achievement)DataSet.Key, GUILayout.Width(150.0f));
			DataSet.Value = EditorGUILayout.IntField(DataSet.Value, GUILayout.Width(150.0f));

			GUILayout.EndHorizontal();
		}

		// show del area
		{
			GUILayout.BeginHorizontal("box");
			
			if(GUILayout.Button("Del", GUILayout.Width(60.0f)))
				Target.Data.Remove(DataDel.Key);
			
			DataDel.Key = (int)(ENUM_Achievement)EditorGUILayout.EnumPopup((ENUM_Achievement)DataDel.Key, GUILayout.Width(150.0f));
			
			GUILayout.EndHorizontal();
		}

		ShowData = EditorGUILayout.Toggle("Show Achievement", ShowData);
		
		if(ShowData == false)
			return;

		// show content
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Type", GUILayout.Width(150.0f));
			GUILayout.Label("Value", GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		foreach(KeyValuePair<int, int> Itor in Target.Data)
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(((ENUM_Achievement)Itor.Key).ToString(), GUILayout.Width(150.0f));
			GUILayout.Label(Itor.Value.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}//for
	}
}