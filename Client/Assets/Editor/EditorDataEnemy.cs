using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataEnemy))]
public class EditorDataEnemy : Editor
{
	private List<SaveEnemy> Data = new List<SaveEnemy>();

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

		Data.Clear();

		foreach(KeyValuePair<GameObject,int> Itor in SysMain.pthis.Enemy)
		{
			if(Itor.Key)
			{
				AIEnemy EnemyTemp = Itor.Key.GetComponent<AIEnemy>();
				
				if (EnemyTemp && EnemyTemp.iHP > 0)
				{
					SaveEnemy Temp = new SaveEnemy();
					
					Temp.iMonster = EnemyTemp.iMonster;
					Temp.iHP = EnemyTemp.iHP;
					Temp.fPosX = Itor.Key.transform.localPosition.x - CameraCtrl.pthis.gameObject.transform.localPosition.x;
					Temp.fPosY = Itor.Key.transform.localPosition.y - CameraCtrl.pthis.gameObject.transform.localPosition.y;
					
					Data.Add(Temp);
				}//if
			}//if
		}//for
	}
	private DataEnemy Target
	{
		get
		{
			return (DataEnemy)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;

		ShowData = EditorGUILayout.Toggle("Show Enemy", ShowData);
		
		if(ShowData == false)
			return;

		// show content
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Monster", GUILayout.Width(75.0f));
		GUILayout.Label("HP", GUILayout.Width(75.0f));
		GUILayout.Label("PosX", GUILayout.Width(75.0f));
		GUILayout.Label("PosY", GUILayout.Width(75.0f));
		GUILayout.EndHorizontal();

		for(int iPos = 0; iPos < Data.Count; ++iPos)
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(Data[iPos].iMonster.ToString(), GUILayout.Width(75.0f));
			GUILayout.Label(Data[iPos].iHP.ToString(), GUILayout.Width(75.0f));
			GUILayout.Label(Data[iPos].fPosX.ToString(), GUILayout.Width(75.0f));
			GUILayout.Label(Data[iPos].fPosY.ToString(), GUILayout.Width(75.0f));
			GUILayout.EndHorizontal();
		}//for
	}
}