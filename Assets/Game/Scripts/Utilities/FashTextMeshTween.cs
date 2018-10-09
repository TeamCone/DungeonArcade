using TMPro;
using UnityEngine;

public class FashTextMeshTween : MonoBehaviour {

	
	// Use this for initialization
	void Start () 
	{
		TweenFacade.FlashTextMesh(GetComponent<TextMeshProUGUI>(), 1);
	}

}
