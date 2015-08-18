using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataReward))]
public class EditorDataReward : Editor
{
	private int MemberLooks = 0;
	private int MemberInits = 0;
	private int WeaponLevel = 0;
	private ENUM_Weapon WeaponType = ENUM_Weapon.Null;
	
	private DataReward Target
	{
		get
		{
			return (DataReward)target;
		}
	}
	public override void OnInspectorGUI()
	{
		//if(EditorApplication.isPlaying == false)
		//	return;

		{
			GUILayout.BeginVertical("box");
			GUILayout.Label("Member Looks", GUILayout.Width(100.0f));

			{
				GUILayout.BeginHorizontal("box");
				
				if(GUILayout.Button("Set", GUILayout.Width(60.0f)))
					Target.MemberLooks.Add(MemberLooks);
				
				if(GUILayout.Button("Del", GUILayout.Width(60.0f)))
					Target.MemberLooks.Remove(MemberLooks);
				
				MemberLooks = EditorGUILayout.IntField(MemberLooks, GUILayout.Width(100.0f));
				
				GUILayout.EndHorizontal();
			}
						
			if(Target.MemberLooks.Count > 0)
			{
				GUILayout.BeginVertical("box");
				
				foreach(int Itor in Target.MemberLooks)
					GUILayout.Label(Itor.ToString(), GUILayout.Width(100.0f));
				
				GUILayout.EndVertical();
			}//if
			
			GUILayout.EndVertical();
		}

		{
			GUILayout.BeginVertical("box");
			GUILayout.Label("Member Inits", GUILayout.Width(100.0f));

			{
				GUILayout.BeginHorizontal("box");
				
				if(GUILayout.Button("Set", GUILayout.Width(60.0f)))
					Target.MemberInits.Add(MemberInits);
				
				if(GUILayout.Button("Del", GUILayout.Width(60.0f)))
					Target.MemberInits.Remove(MemberInits);
				
				MemberInits = EditorGUILayout.IntField(MemberInits, GUILayout.Width(100.0f));
				
				GUILayout.EndHorizontal();
			}
						
			if(Target.MemberInits.Count > 0)
			{
				GUILayout.BeginVertical("box");
				
				foreach(int Itor in Target.MemberInits)
					GUILayout.Label(Itor.ToString(), GUILayout.Width(100.0f));
				
				GUILayout.EndVertical();
			}//if
			
			GUILayout.EndVertical();
		}

		{
			GUILayout.BeginVertical("box");
			GUILayout.Label("Weapon Level", GUILayout.Width(100.0f));

			{
				GUILayout.BeginHorizontal("box");
				
				if(GUILayout.Button("Set", GUILayout.Width(60.0f)))
					Target.WeaponLevel.Add((int)WeaponType, WeaponLevel);
				
				if(GUILayout.Button("Del", GUILayout.Width(60.0f)))
					Target.WeaponLevel.Remove((int)WeaponType);
				
				WeaponType = (ENUM_Weapon)EditorGUILayout.EnumPopup(WeaponType, GUILayout.Width(100.0f));
				WeaponLevel = EditorGUILayout.IntField(WeaponLevel, GUILayout.Width(100.0f));
				
				GUILayout.EndHorizontal();
			}

			{
				GUILayout.BeginHorizontal("box");
				GUILayout.Label("Weapon", GUILayout.Width(100.0f));
				GUILayout.Label("Level", GUILayout.Width(100.0f));
				GUILayout.EndHorizontal();
			}
						
			foreach(KeyValuePair<int, int> Itor in Target.WeaponLevel)
			{
				GUILayout.BeginHorizontal("box");
				GUILayout.Label(((ENUM_Weapon)Itor.Key).ToString(), GUILayout.Width(100.0f));
				GUILayout.Label(Itor.Value.ToString(), GUILayout.Width(100.0f));
				GUILayout.EndHorizontal();
			}//for
			
			GUILayout.EndVertical();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Init Currency", GUILayout.Width(100.0f));
			Target.iInitCurrency = EditorGUILayout.IntField(Target.iInitCurrency, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Init Battery", GUILayout.Width(100.0f));
			Target.iInitBattery = EditorGUILayout.IntField(Target.iInitBattery, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Init LightAmmo", GUILayout.Width(100.0f));
			Target.iInitLightAmmo = EditorGUILayout.IntField(Target.iInitLightAmmo, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Init HeavyAmmo", GUILayout.Width(100.0f));
			Target.iInitHeavyAmmo = EditorGUILayout.IntField(Target.iInitHeavyAmmo, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Init Bomb", GUILayout.Width(100.0f));
			Target.iInitBomb = EditorGUILayout.IntField(Target.iInitBomb, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Crystal", GUILayout.Width(100.0f));
			Target.iCrystal = EditorGUILayout.IntField(Target.iCrystal, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}
	}
}