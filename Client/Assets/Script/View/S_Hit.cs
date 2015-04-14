using UnityEngine;
using System.Collections;

public class S_Hit : MonoBehaviour 
{
	public UISpriteAnimation pAni = null;
	// Update is called once per frame
	void Update () 
	{
		if(!pAni.isPlaying)
			Destroy(gameObject);
	}
}
