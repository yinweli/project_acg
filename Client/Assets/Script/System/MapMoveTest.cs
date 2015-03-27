using UnityEngine;
using System.Collections;

public class MapMoveTest : MonoBehaviour
{
	public float TimeRemain = 0.0f;
	public int RoadCount = 0;
	
	void Update()
	{
		/*
		TimeRemain -= Time.deltaTime;
		
		if(TimeRemain > 0)
			return;
		
		TimeRemain = 1.0f;
		
		Vector2 Pos = MapCreater.This.Refresh(RoadCount);

		Pos.x = -Pos.x;
		Pos.y = -Pos.y;

		gameObject.transform.localPosition = Pos;

		++RoadCount;
		*/
	}
}
