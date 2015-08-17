using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataGame))]
public class EditorDataGame : Editor
{
	private ENUM_Language Language = ENUM_Language.enUS;
	private int StageTime = 0;
	private int Kill = 0;
	private int Alive = 0;
	private int Dead = 0;
	private int Position = 0;
	private bool Victory = false;
	private float RunSpeed = 0.0f;
	private List<Pickup> Data = new List<Pickup>();

	private bool ShowData = false;

	void OnEnable()
	{
		if(EditorApplication.isPlaying == false)
			return;
		
		EditorApplication.update += new EditorApplication.CallbackFunction(Update);
	}
	void Update()
	{
		if(EditorApplication.isPlaying == false)
			return;

		Language = Target.Language;
		StageTime = Target.iStageTime;
		Kill = Target.iKill;
		Alive = Target.iAlive;
		Dead = Target.iDead;
		Position = Target.iRoad;
		Victory = Target.bVictory;
		RunSpeed = Target.fRunDouble;
		Data = new List<Pickup>(Target.PickupList);
	}
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
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Language", GUILayout.Width(150.0f));
		GUILayout.Label(Language.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("StageTime", GUILayout.Width(150.0f));
		GUILayout.Label(StageTime.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Kill", GUILayout.Width(150.0f));
		GUILayout.Label(Kill.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Alive", GUILayout.Width(150.0f));
		GUILayout.Label(Alive.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Dead", GUILayout.Width(150.0f));
		GUILayout.Label(Dead.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Position", GUILayout.Width(150.0f));
		GUILayout.Label(Position.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Victory", GUILayout.Width(150.0f));
		GUILayout.Label(Victory.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("RunSpeed", GUILayout.Width(150.0f));
		GUILayout.Label(RunSpeed.ToString(), GUILayout.Width(150.0f));
		GUILayout.EndHorizontal();

		ShowData = EditorGUILayout.Toggle("Show Pickup", ShowData);

		if(ShowData == false)
			return;

		GUILayout.BeginVertical("box");
		
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("PosX", GUILayout.Width(50.0f));
		GUILayout.Label("PosY", GUILayout.Width(50.0f));
		GUILayout.Label("Type", GUILayout.Width(50.0f));
		GUILayout.Label("Count", GUILayout.Width(50.0f));
		GUILayout.Label("Looks", GUILayout.Width(50.0f));
		GUILayout.Label("Pickup", GUILayout.Width(50.0f));
		GUILayout.EndHorizontal();

		foreach(Pickup Itor in Data)
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(Itor.Pos.X.ToString(), GUILayout.Width(50.0f));
			GUILayout.Label(Itor.Pos.Y.ToString(), GUILayout.Width(50.0f));
			GUILayout.Label(((ENUM_Pickup)Itor.iType).ToString(), GUILayout.Width(50.0f));
			GUILayout.Label(Itor.iCount.ToString(), GUILayout.Width(50.0f));
			GUILayout.Label(Itor.iLooks.ToString(), GUILayout.Width(50.0f));
			GUILayout.Label(Itor.bPickup.ToString(), GUILayout.Width(50.0f));
			GUILayout.EndHorizontal();
		}//for
		
		GUILayout.EndVertical();
	}
}