using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class DBFAchievement : DBF
{
	public int Name = 0; // 成就名稱
	public int Cond = 0; // 條件列舉
	public int CondValue = 0; // 條件值
	public int Reward = 0; // 獎勵列舉
	public int RewardValue = 0; // 獎勵值

	public static string GetGUID(int iGUID, int iLevel)
	{
		return iGUID + "-" + iLevel;
	}
	public static Tuple<int, int> GetGUID(string szGUID)
	{
		string[] szTemp = szGUID.Split(new char[] {'-'});

		return szTemp.Length >= 2 ? new Tuple<int, int>(System.Convert.ToInt32(szTemp[0]), System.Convert.ToInt32(szTemp[1])) : null;
	}
}