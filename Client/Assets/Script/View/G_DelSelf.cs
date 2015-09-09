using UnityEngine;
using System.Collections;

public class G_DelSelf : MonoBehaviour 
{
    public GameObject ObjTarget = null;

	public void DelSelf()
    {
        Destroy(gameObject);
    }

    public void DelbyTarget()
    {
        if (ObjTarget)
            Destroy(ObjTarget);
    }
}
