using UnityEngine;
using System.Collections;

public class G_Light : MonoBehaviour 
{
    public UIDragObject pDrag;
    public LightFollow pFollow;

    public void SetLightFollow(GameObject pObj)
    {
        pDrag.panelRegion = Camera.main.gameObject.transform.parent.GetComponent<UIPanel>();
        pFollow.ObjTarget = pObj;

        Destroy(this);
    }
}
