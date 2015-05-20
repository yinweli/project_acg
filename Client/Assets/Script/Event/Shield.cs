using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour 
{
    public int iCount = 0;

    void Start()
    {
        if (transform.parent.gameObject.GetComponent<AIPlayer>())
            iCount = PlayerData.pthis.Members[GetComponent<AIPlayer>().iPlayer].iShield;
        else
            Destroy(gameObject);
    }

    public void CostShield()
    {
        iCount--;

        if (iCount <= 0)
            Destroy(gameObject);
    }
}
