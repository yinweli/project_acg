using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Statistics	))]
public class EditorStatistics : Editor
{
	private Statistics Target
	{
		get
		{
			return (Statistics)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;

		// Resource
		{
			GUILayout.BeginVertical("box");

			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Type", GUILayout.Width(80.0f));
			GUILayout.Label("Initial", GUILayout.Width(80.0f));
			GUILayout.Label("Available", GUILayout.Width(80.0f));
			GUILayout.Label("Obtain", GUILayout.Width(80.0f));
			GUILayout.Label("Used", GUILayout.Width(80.0f));
			GUILayout.EndHorizontal();
			
			foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Pickup)))
			{
				if(Itor < 0 || Itor >= Target.DataResource.Count)
					continue;
				
				GUILayout.BeginHorizontal("box");
				GUILayout.Label(((ENUM_Pickup)Itor).ToString(), GUILayout.Width(80.0f));
				GUILayout.Label(Target.DataResource[Itor].iInitial.ToString(), GUILayout.Width(80.0f));
				GUILayout.Label(Target.DataResource[Itor].iAvailable.ToString(), GUILayout.Width(80.0f));
				GUILayout.Label(Target.DataResource[Itor].iObtain.ToString(), GUILayout.Width(80.0f));
				GUILayout.Label(Target.DataResource[Itor].iUsed.ToString(), GUILayout.Width(80.0f));
				GUILayout.EndHorizontal();
			}//for

			GUILayout.EndVertical();
		}

		// Damage
		{
			GUILayout.BeginVertical("box");

			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Type", GUILayout.Width(80.0f));
			GUILayout.Label("Damage", GUILayout.Width(80.0f));
			GUILayout.EndHorizontal();

			foreach(KeyValuePair<ENUM_Damage, int> Itor in Target.DataDamage)
			{
				GUILayout.BeginHorizontal("box");
				GUILayout.Label(Itor.Key.ToString(), GUILayout.Width(80.0f));
				GUILayout.Label(Itor.Value.ToString(), GUILayout.Width(80.0f));
				GUILayout.EndHorizontal();
			}//for

			GUILayout.EndVertical();
		}
	}
}