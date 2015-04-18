using UnityEngine;
using System.Collections;

public class BtnDragLight : MonoBehaviour
{
    public GameObject ObjRange;
    public GameObject ObjRange_2;

    void OnDrag(Vector2 delta)
    {
        Vector3 diff = transform.position - ObjRange.transform.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        ObjRange.transform.rotation = Quaternion.Euler(0, 0, rot_z - 90);
        ObjRange_2.transform.rotation = Quaternion.Euler(0, 0, rot_z - 90);
    }
}