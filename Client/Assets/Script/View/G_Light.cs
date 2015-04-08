using UnityEngine;
using System.Collections;

public class G_Light : MonoBehaviour 
{
    public UIDragObject pDrag;
    public LightFollow pFollow;
    public Animator pAni;

    void Start()
    {
        P_UI.pthis.pListLight.Add(this);
    }

    void OnDestroy()
    {
        P_UI.pthis.pListLight.Remove(this);
    }

    public void SetLightFollow(GameObject pObj)
    {
        pDrag.panelRegion = Camera.main.gameObject.transform.parent.GetComponent<UIPanel>();
        pFollow.ObjTarget = pObj;

        //Destroy(this);
    }
}
