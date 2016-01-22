using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataCollection))]
public class EditorDataCollection : Editor
{
	private ENUM_Weapon WeaponShow = ENUM_Weapon.Null;
	private ENUM_Weapon Weapon = ENUM_Weapon.Null;
	private int Level = 0;
	private int Index = 0;
	
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
				Target.Add(Weapon, Level, Index);

			if(GUILayout.Button("Set All", GUILayout.Width(60.0f)))
				Target.Add(Weapon, Level);

			if(GUILayout.Button("Del", GUILayout.Width(60.0f)))
				Target.Del(Weapon, Level, Index);

			Weapon = (ENUM_Weapon)EditorGUILayout.EnumPopup(Weapon, GUILayout.Width(150.0f));
			Level = EditorGUILayout.IntField(Level, GUILayout.Width(60.0f));
			Index = EditorGUILayout.IntField(Index, GUILayout.Width(60.0f));
			
			GUILayout.EndHorizontal();
		}

		// show content
		WeaponShow = (ENUM_Weapon)EditorGUILayout.EnumPopup(WeaponShow, GUILayout.Width(150.0f));

		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Level", GUILayout.Width(100.0f));
		GUILayout.Label("A", GUILayout.Width(60.0f));
		GUILayout.Label("B", GUILayout.Width(60.0f));
		GUILayout.Label("C", GUILayout.Width(60.0f));
		GUILayout.Label("D", GUILayout.Width(60.0f));
		GUILayout.Label("E", GUILayout.Width(60.0f));
		GUILayout.EndHorizontal();

		for(int iLv = 1; iLv <= GameDefine.iMaxCollectionLv; ++iLv)
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(iLv.ToString(), GUILayout.Width(100.0f));

			for(int iID = 1; iID <= GameDefine.iMaxCollectionCount; ++iID)
				GUILayout.Label(Target.IsExist(WeaponShow, iLv, iID) ? "O" : "X", GUILayout.Width(60.0f));

			GUILayout.EndHorizontal();
		}//for
	}
}