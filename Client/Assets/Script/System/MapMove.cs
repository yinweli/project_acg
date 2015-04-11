using UnityEngine;
using System.Collections;

public class MapMove : MonoBehaviour 
{
    static public MapMove pthis = null;

    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    public void StartNew()
    {
        transform.localPosition = new Vector3(-MapCreater.pthis.GetRoadObj(0).transform.localPosition.x, -MapCreater.pthis.GetRoadObj(0).transform.localPosition.y, 0);
    }
    // ------------------------------------------------------------------
}