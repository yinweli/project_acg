using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataGame))]
public class EditorDataGame : Editor
{
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
	}
}