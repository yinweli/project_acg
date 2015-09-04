using UnityEngine;
using System.Collections;

public class P_AddMember : MonoBehaviour
{
    static public P_AddMember pthis = null;

    public G_RandRole pRandRole = null;
    public Animator pAni_RandRole;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    public void GetNewRole()
    {
        pRandRole.StartRand();
        pAni_RandRole.Play("RandRole");        
    }
    // ------------------------------------------------------------------
}
