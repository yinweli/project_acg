using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataGame))]
public class EditorDataGame : Editor
{
	private bool ShowData = false;

	private DataGame Target
	{
		get
		{
			return (DataGame)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;

		// show content
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Language", GUILayout.Width(100.0f));
			GUILayout.Label(Target.Language.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("StageTime", GUILayout.Width(100.0f));
			GUILayout.Label(Target.iStageTime.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Kill", GUILayout.Width(100.0f));
			GUILayout.Label(Target.iKill.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Alive", GUILayout.Width(100.0f));
			GUILayout.Label(Target.iAlive.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Dead", GUILayout.Width(100.0f));
			GUILayout.Label(Target.iDead.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Position", GUILayout.Width(100.0f));
			GUILayout.Label(Target.iRoad.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Victory", GUILayout.Width(100.0f));
			GUILayout.Label(Target.bVictory.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("RunSpeed", GUILayout.Width(100.0f));
			GUILayout.Label(Target.fRunDouble.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			ShowData = EditorGUILayout.Toggle("Show Pickup", ShowData);
			
			if(ShowData == false)
				return;
		}

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

		foreach(Pickup Itor in Target.PickupList)
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