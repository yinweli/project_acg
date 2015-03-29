using UnityEngine;
using System.Collections;

public class MapMove : MonoBehaviour 
{
	public int RoadIndex = 0;
	public Vector3 NextPos = new Vector3();

	void Start()
	{
		MapCreater.This.Create();
		MapCreater.This.Refresh(new Vector2(-100, -100));
	}
	void Update()
    {
	}
}
