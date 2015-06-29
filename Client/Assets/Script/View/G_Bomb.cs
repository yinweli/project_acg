using UnityEngine;
using System.Collections;

public class G_Bomb : MonoBehaviour 
{
    public void EndBomb()
    {
        SysBomb.pthis.BombDamage();
        Destroy(gameObject);
    }

    public void BombSound()
    {
        NGUITools.PlaySound(Resources.Load("Sound/FX/Bomb") as AudioClip);
    }
}
