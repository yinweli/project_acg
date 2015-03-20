using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SysMain : MonoBehaviour 
{
    public static SysMain pthis = null;

    public bool bIsGaming = true;

    public int iStage = 1;

    // 人物佇列.
    public List<GameObject> Role = new List<GameObject>();
    // 敵人佇列.
    public List<GameObject> Enemy = new List<GameObject>();
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }

    void OnDestroy()
    {
        pthis = null;
    }
    // ------------------------------------------------------------------
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
