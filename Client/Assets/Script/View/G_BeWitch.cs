using UnityEngine;
using System.Collections;

public class G_BeWitch : MonoBehaviour
{
    AIEnemy pAI = null;

    public void AddHP(int iValue, bool IsCrit)
    {
        // 播放護盾被擊中特效.

        HurtMonster(iValue);
    }

    // 傷害轉移到怪物身上.
    public void HurtMonster(int iValue)
    {
        pAI.iHP -= iValue;
    }
}
