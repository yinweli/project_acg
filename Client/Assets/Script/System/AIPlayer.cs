using UnityEngine;
using System.Collections;

public class AIPlayer : MonoBehaviour 
{
    // 手上武器type.
    // 攻擊範圍.
    public float fRange = 1;
    // 射速.
    public float fShotSpeed = 0.1f;

    // 目標
    public GameObject ObjTarget = null;
    // 是否被抓住.
    public bool bBeCaught = false;
    // 無敵狀態剩餘秒數.
    public float fInvincibleSec = 0;

	// Use this for initialization
	void Start ()
    {
        SysMain.pthis.Role.Add(gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
