using UnityEngine;
using System.Collections;

public class G_RandHire : MonoBehaviour {

	public void CheckEnd(int iIndex)
    {
        PlayRSound();
        P_AddMember.pthis.CheckShowEnd(iIndex);
    }

    public void PlayRSound()
    {
        NGUITools.PlaySound(Resources.Load("Sound/FX/RRole") as AudioClip, 1, 1);
    }
}
