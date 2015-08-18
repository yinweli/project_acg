using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class EditorDataPlayerMember
{
	private int Pos = -1;
	private int Feature = 0;

	public void Show(string szType, List<Member> Data)
	{
		Pos = EditorGUILayout.IntSlider(szType + " Pos", Pos, -1, Data.Count - 1, GUILayout.Width(300.0f));		
		
		if(Pos >= 0 && Pos < Data.Count)
		{
			Member Temp = Data[Pos];

			{
				GUILayout.BeginHorizontal("box");
				GUILayout.Label("Looks", GUILayout.Width(100.0f));
				Temp.iLooks = EditorGUILayout.IntField(Temp.iLooks, GUILayout.Width(100.0f));
				GUILayout.EndHorizontal();
			}

			{
				GUILayout.BeginHorizontal("box");
				GUILayout.Label("Equip", GUILayout.Width(100.0f));
				Temp.iEquip = EditorGUILayout.IntField(Temp.iEquip, GUILayout.Width(100.0f));
				GUILayout.EndHorizontal();
			}

			{
				GUILayout.BeginHorizontal("box");
				GUILayout.Label("LiveStage", GUILayout.Width(100.0f));
				Temp.iLiveStage = EditorGUILayout.IntField(Temp.iLiveStage, GUILayout.Width(100.0f));
				GUILayout.EndHorizontal();
			}

			{
				GUILayout.BeginHorizontal("box");
				GUILayout.Label("Shield", GUILayout.Width(100.0f));
				Temp.iShield = EditorGUILayout.IntField(Temp.iShield, GUILayout.Width(100.0f));
				GUILayout.EndHorizontal();
			}

			{
				GUILayout.BeginHorizontal("box");
				GUILayout.Label("Invincible", GUILayout.Width(100.0f));
				Temp.iInvincibleTime = EditorGUILayout.IntField(Temp.iInvincibleTime, GUILayout.Width(100.0f));
				GUILayout.EndHorizontal();
			}

			{
				GUILayout.BeginHorizontal("box");
				GUILayout.Label("Critical", GUILayout.Width(100.0f));
				Temp.fCriticalStrike = EditorGUILayout.FloatField(Temp.fCriticalStrike, GUILayout.Width(100.0f));
				GUILayout.EndHorizontal();
			}

			{
				GUILayout.BeginHorizontal("box");
				GUILayout.Label("AddDamge", GUILayout.Width(100.0f));
				Temp.iAddDamage = EditorGUILayout.IntField(Temp.iAddDamage, GUILayout.Width(100.0f));
				GUILayout.EndHorizontal();
			}

			{
				GUILayout.BeginVertical("box");
				GUILayout.Label("Feature", GUILayout.Width(100.0f));

				{
					GUILayout.BeginHorizontal("box");
					
					if(GUILayout.Button("Set", GUILayout.Width(60.0f)) && Temp.Feature.Contains(Feature) == false)
						Temp.Feature.Add(Feature);
					
					if(GUILayout.Button("Del", GUILayout.Width(60.0f)))
						Temp.Feature.Remove(Feature);
					
					Feature = EditorGUILayout.IntField(Feature, GUILayout.Width(100.0f));
					
					GUILayout.EndHorizontal();
				}

				{
					GUILayout.BeginHorizontal("box");
					
					string szFeature = "";
					
					foreach(int Itor in Temp.Feature)
						szFeature += Itor + ", ";
					
					GUILayout.Label(szFeature, GUILayout.Width(200.0f));
					GUILayout.EndHorizontal();
				}

				GUILayout.EndVertical();
			}
		}//if
	}
}

[CustomEditor(typeof(DataPlayer))]
public class EditorDataPlayer : Editor
{
	private EditorDataPlayerMember MemberParty = new EditorDataPlayerMember();
	private EditorDataPlayerMember MemberDepot = new EditorDataPlayerMember();

	private DataPlayer Target
	{
		get
		{
			return (DataPlayer)target;
		}
	}
	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying == false)
			return;
		
		// show content
		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Stage", GUILayout.Width(100.0f));
			Target.iStage = EditorGUILayout.IntField(Target.iStage, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Style", GUILayout.Width(100.0f));
			GUILayout.Label(Target.iStyle.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Currency", GUILayout.Width(100.0f));
			Target.iCurrency = EditorGUILayout.IntField(Target.iCurrency, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Battery", GUILayout.Width(100.0f));
			Target.iBattery = EditorGUILayout.IntField(Target.iBattery, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("LightAmmo", GUILayout.Width(100.0f));
			Target.iLightAmmo = EditorGUILayout.IntField(Target.iLightAmmo, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("HeavyAmmo", GUILayout.Width(100.0f));
			Target.iHeavyAmmo = EditorGUILayout.IntField(Target.iHeavyAmmo, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Bomb", GUILayout.Width(100.0f));
			Target.iBomb = EditorGUILayout.IntField(Target.iBomb, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("Stamina", GUILayout.Width(100.0f));
			Target.iStamina = EditorGUILayout.IntField(Target.iStamina, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("StaminaLimit", GUILayout.Width(100.0f));
			Target.iStaminaLimit = EditorGUILayout.IntField(Target.iStaminaLimit, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("StaminaRecovery", GUILayout.Width(100.0f));
			Target.iStaminaRecovery = EditorGUILayout.IntField(Target.iStaminaRecovery, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("DamageLv", GUILayout.Width(100.0f));
			Target.iDamageLv = EditorGUILayout.IntField(Target.iDamageLv, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("PlayTime", GUILayout.Width(100.0f));
			GUILayout.Label(Target.iPlayTime.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("EnemyKill", GUILayout.Width(100.0f));
			GUILayout.Label(Target.iEnemyKill.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("PlayerLost", GUILayout.Width(100.0f));
			GUILayout.Label(Target.iPlayerLost.ToString(), GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		{
			GUILayout.BeginHorizontal("box");
			GUILayout.Label("AdsWatch", GUILayout.Width(100.0f));
			Target.iAdsWatch = EditorGUILayout.IntField(Target.iAdsWatch, GUILayout.Width(100.0f));
			GUILayout.EndHorizontal();
		}

		MemberParty.Show("Party", Target.MemberParty);
		MemberDepot.Show("Depot", Target.MemberDepot);
	}
}