using UnityEngine;
using System.Collections;

public class MapMove : MonoBehaviour 
{
	private int RoadIndex = 0;
	private Vector3 DestPos = new Vector3();

	void Start()
	{
		//MapCreater.This.Create();
	}
	void Update()
    {
	    if(SysMain.pthis.bIsGaming == false)
			return;

		if(MapCreater.This.RoadCount() <= RoadIndex)
			return;

		if(DestPos == transform.localPosition)
		{
			Vector2 Pos = MapCreater.This.Refresh(RoadIndex++);

			DestPos.x = -Pos.x;
			DestPos.y = -Pos.y;
			DestPos.z = 0.0f;

			Debug.Log(DestPos);
		}
		else
			transform.position = Vector3.Lerp(transform.localPosition, DestPos, Time.deltaTime * 0.1f);
	}
}
