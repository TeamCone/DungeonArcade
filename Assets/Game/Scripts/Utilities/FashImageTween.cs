using UnityEngine;
using UnityEngine.UI;

public class FashImageTween : MonoBehaviour {

	
	// Use this for initialization
	void Start () 
	{
		TweenFacade.FlashImageMesh(GetComponent<Image>(), 1);
	}

}
