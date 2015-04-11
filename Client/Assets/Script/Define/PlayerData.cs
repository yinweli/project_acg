using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour 
{
    static public PlayerData pthis = null;

	/* Save */
	public int iStage = 0; // 關卡編號
	public int iCurrency = 0; // 通貨
	public int iEnemyKill = 0; // 殺怪數量
	public int iPlayTime = 0; // 遊戲時間
	public List<int> Resource = new List<int>(); // 資源列表
	public List<Member> Data = new List<Member>(); // 成員列表

	/* Not Save */
	public int iStamina = 0; // 耐力
	public int iStaminaLimit = 0; // 耐力上限
	public int iStaminaRecovery = 0; // 耐力回復

    void Awake()
    {
        pthis = this;
    }

    // 讀檔.

    // 存檔.
}