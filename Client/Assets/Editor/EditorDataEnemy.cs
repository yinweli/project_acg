using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataEnemy))]
public class EditorDataEnemy : Editor
{
	private bool ShowData = false;

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
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Monster", GUILayout.Width(80.0f));
			GUILayout.Label("HP", GUILayout.Width(80.0f));
			GUILayout.Label("Move", GUILayout.Width(80.0f));
			GUILayout.Label("PosX", GUILayout.Width(80.0f));
			GUILayout.Label("PosY", GUILayout.Width(80.0f));
			GUILayout.EndHorizontal();
		}

		foreach(KeyValuePair<GameObject,int> Itor in SysMain.pthis.Enemy)
		{
			if(Itor.Key)
			{
				AIEnemy EnemyTemp = Itor.Key.GetComponent<AIEnemy>();

				if(EnemyTemp != null)
				{
					Vector3 Pos = Target.EnemyPos(Itor.Key.transform.localPosition);

					GUILayout.BeginHorizontal("box");
					GUILayout.Label(EnemyTemp.iMonster.ToString(), GUILayout.Width(80.0f));
					GUILayout.Label(EnemyTemp.iHP.ToString(), GUILayout.Width(80.0f));
					GUILayout.Label(EnemyTemp.GetSpeed().ToString(), GUILayout.Width(80.0f));
					GUILayout.Label(Pos.x.ToString(), GUILayout.Width(80.0f));
					GUILayout.Label(Pos.y.ToString(), GUILayout.Width(80.0f));
					GUILayout.EndHorizontal();
				}//if
			}//if
		}//for
	}
}