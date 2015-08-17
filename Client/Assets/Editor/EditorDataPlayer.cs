using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DataPlayer))]
public class EditorDataPlayer : Editor
{
	private bool ShowMemberParty = false;
	private bool ShowMemberDepot = false;
	private bool ShowFeatureParty = false;
	private bool ShowFeatureDepot = false;
	private int PosMemberParty = 0;
	private int PosMemberDepot = 0;

	private string[] NameMemberParty = new string[GameDefine.iMaxMemberParty];
	private string[] NameMemberDepot = new string[GameDefine.iMaxMemberDepot];
	private int[] SelMemberParty = new int[GameDefine.iMaxMemberParty];
	private int[] SelMemberDepot = new int[GameDefine.iMaxMemberDepot];

	void OnEnable()
	{
		for(int iPos = 0; iPos < GameDefine.iMaxMemberParty; ++iPos)
		{
			NameMemberParty[iPos] = System.Convert.ToString(iPos);
			SelMemberParty[iPos] = iPos;
		}//for

		for(int iPos = 0; iPos < GameDefine.iMaxMemberDepot; ++iPos)
		{
			NameMemberDepot[iPos] = System.Convert.ToString(iPos);
			SelMemberDepot[iPos] = iPos;
		}//for
	}
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
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Stage", GUILayout.Width(200.0f));
		Target.iStage = EditorGUILayout.IntField(Target.iStage, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Style", GUILayout.Width(200.0f));
		GUILayout.Label(Target.iStyle.ToString(), GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Currency", GUILayout.Width(200.0f));
		Target.iCurrency = EditorGUILayout.IntField(Target.iCurrency, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Battery", GUILayout.Width(200.0f));
		Target.iBattery = EditorGUILayout.IntField(Target.iBattery, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("LightAmmo", GUILayout.Width(200.0f));
		Target.iLightAmmo = EditorGUILayout.IntField(Target.iLightAmmo, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("HeavyAmmo", GUILayout.Width(200.0f));
		Target.iHeavyAmmo = EditorGUILayout.IntField(Target.iHeavyAmmo, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Bomb", GUILayout.Width(200.0f));
		Target.iBomb = EditorGUILayout.IntField(Target.iBomb, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("Stamina", GUILayout.Width(200.0f));
		Target.iStamina = EditorGUILayout.IntField(Target.iStamina, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("StaminaLimit", GUILayout.Width(200.0f));
		Target.iStaminaLimit = EditorGUILayout.IntField(Target.iStaminaLimit, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("StaminaRecovery", GUILayout.Width(200.0f));
		Target.iStaminaRecovery = EditorGUILayout.IntField(Target.iStaminaRecovery, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("DamageLv", GUILayout.Width(200.0f));
		Target.iDamageLv = EditorGUILayout.IntField(Target.iDamageLv, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("PlayTime", GUILayout.Width(200.0f));
		GUILayout.Label(Target.iPlayTime.ToString(), GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("EnemyKill", GUILayout.Width(200.0f));
		GUILayout.Label(Target.iEnemyKill.ToString(), GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("PlayerLost", GUILayout.Width(200.0f));
		GUILayout.Label(Target.iPlayerLost.ToString(), GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		GUILayout.Label("AdsWatch", GUILayout.Width(200.0f));
		Target.iAdsWatch = EditorGUILayout.IntField(Target.iAdsWatch, GUILayout.Width(200.0f));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal("box");
		ShowMemberParty = EditorGUILayout.Toggle("Show Member Party", ShowMemberParty);
		ShowFeatureParty = EditorGUILayout.Toggle("Show Feature Party", ShowFeatureParty);
		GUILayout.EndHorizontal();
		
		if(ShowMemberParty)
		{
			PosMemberParty = EditorGUILayout.IntPopup("Party Pos", PosMemberParty, NameMemberParty, SelMemberParty);

			if(PosMemberParty >= 0 && PosMemberParty < Target.MemberParty.Count)
			{
				Member Temp = Target.MemberParty[PosMemberParty];

				GUILayout.BeginVertical("box");

				GUILayout.BeginHorizontal("box");
				GUILayout.Label("Looks", GUILayout.Width(47.0f));
				GUILayout.Label("Equip", GUILayout.Width(47.0f));
				GUILayout.Label("LiveStage", GUILayout.Width(47.0f));
				GUILayout.Label("Shield", GUILayout.Width(47.0f));
				GUILayout.Label("Invincible", GUILayout.Width(47.0f));
				GUILayout.Label("Critical", GUILayout.Width(47.0f));
				GUILayout.Label("AddDamge", GUILayout.Width(47.0f));
				GUILayout.Label("Features", GUILayout.Width(47.0f));
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal("box");
				Temp.iLooks = EditorGUILayout.IntField(Temp.iLooks, GUILayout.Width(47.0f));
				Temp.iEquip = EditorGUILayout.IntField(Temp.iEquip, GUILayout.Width(47.0f));
				Temp.iLiveStage = EditorGUILayout.IntField(Temp.iLiveStage, GUILayout.Width(47.0f));
				Temp.iShield = EditorGUILayout.IntField(Temp.iShield, GUILayout.Width(47.0f));
				Temp.iInvincibleTime = EditorGUILayout.IntField(Temp.iInvincibleTime, GUILayout.Width(47.0f));
				Temp.fCriticalStrike = EditorGUILayout.FloatField(Temp.fCriticalStrike, GUILayout.Width(47.0f));
				Temp.iAddDamage = EditorGUILayout.IntField(Temp.iAddDamage, GUILayout.Width(47.0f));
				GUILayout.Label(Temp.Feature.Count.ToString(), GUILayout.Width(47.0f));
				GUILayout.EndHorizontal();

				GUILayout.EndVertical();

				if(ShowFeatureParty)
				{
					GUILayout.Label("Feature List");
					GUILayout.BeginHorizontal("box");

					foreach(int Itor in Temp.Feature)
						GUILayout.Label(Itor.ToString(), GUILayout.Width(65.0f));

					GUILayout.EndHorizontal();
				}//if
			}//if
		}//if

		GUILayout.BeginHorizontal("box");
		ShowMemberDepot = EditorGUILayout.Toggle("Show Member Depot", ShowMemberDepot);
		ShowFeatureDepot = EditorGUILayout.Toggle("Show Feature Depot", ShowFeatureDepot);
		GUILayout.EndHorizontal();
		
		if(ShowMemberDepot)
		{
			PosMemberDepot = EditorGUILayout.IntPopup("Depot Pos", PosMemberDepot, NameMemberDepot, SelMemberDepot);
			
			if(PosMemberDepot >= 0 && PosMemberDepot < Target.MemberDepot.Count)
			{
				Member Temp = Target.MemberDepot[PosMemberDepot];

				GUILayout.BeginVertical("box");
				
				GUILayout.BeginHorizontal("box");
				GUILayout.Label("Looks", GUILayout.Width(47.0f));
				GUILayout.Label("Equip", GUILayout.Width(47.0f));
				GUILayout.Label("LiveStage", GUILayout.Width(47.0f));
				GUILayout.Label("Shield", GUILayout.Width(47.0f));
				GUILayout.Label("Invincible", GUILayout.Width(47.0f));
				GUILayout.Label("Critical", GUILayout.Width(47.0f));
				GUILayout.Label("AddDamge", GUILayout.Width(47.0f));
				GUILayout.Label("Features", GUILayout.Width(47.0f));
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal("box");
				Temp.iLooks = EditorGUILayout.IntField(Temp.iLooks, GUILayout.Width(47.0f));
				Temp.iEquip = EditorGUILayout.IntField(Temp.iEquip, GUILayout.Width(47.0f));
				Temp.iLiveStage = EditorGUILayout.IntField(Temp.iLiveStage, GUILayout.Width(47.0f));
				Temp.iShield = EditorGUILayout.IntField(Temp.iShield, GUILayout.Width(47.0f));
				Temp.iInvincibleTime = EditorGUILayout.IntField(Temp.iInvincibleTime, GUILayout.Width(47.0f));
				Temp.fCriticalStrike = EditorGUILayout.FloatField(Temp.fCriticalStrike, GUILayout.Width(47.0f));
				Temp.iAddDamage = EditorGUILayout.IntField(Temp.iAddDamage, GUILayout.Width(47.0f));
				GUILayout.Label(Temp.Feature.Count.ToString(), GUILayout.Width(47.0f));
				GUILayout.EndHorizontal();
				
				GUILayout.EndVertical();

				if(ShowFeatureDepot)
				{
					GUILayout.Label("Feature List");
					GUILayout.BeginHorizontal("box");
					
					foreach(int Itor in Temp.Feature)
						GUILayout.Label(Itor.ToString(), GUILayout.Width(65.0f));
					
					GUILayout.EndHorizontal();
				}//if
			}//if
		}//if
	}
}