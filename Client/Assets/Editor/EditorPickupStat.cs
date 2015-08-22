using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(PickupStat	))]
public class EditorPickupStat : Editor
{
	private PickupStat Target
	{
		get
		{
			return (PickupStat)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;
		
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Type", GUILayout.Width(80.0f));
		GUILayout.Label("Initial", GUILayout.Width(80.0f));
		GUILayout.Label("Available", GUILayout.Width(80.0f));
		GUILayout.Label("Obtain", GUILayout.Width(80.0f));
		GUILayout.Label("Used", GUILayout.Width(80.0f));
		GUILayout.EndHorizontal();
		
		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Pickup)))
		{
			if(Itor < 0 || Itor >= Target.Data.Count)
				continue;

			GUILayout.BeginHorizontal("box");
			GUILayout.Label(((ENUM_Pickup)Itor).ToString(), GUILayout.Width(80.0f));
			GUILayout.Label(Target.Data[Itor].iInitial.ToString(), GUILayout.Width(80.0f));
			GUILayout.Label(Target.Data[Itor].iAvailable.ToString(), GUILayout.Width(80.0f));
			GUILayout.Label(Target.Data[Itor].iObtain.ToString(), GUILayout.Width(80.0f));
			GUILayout.Label(Target.Data[Itor].iUsed.ToString(), GUILayout.Width(80.0f));
			GUILayout.EndHorizontal();
		}//for
	}
}