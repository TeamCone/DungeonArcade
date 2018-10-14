
using UnityEngine;

public static class ResourceFacade
{

	public static GameObject LoadPrefab(string prefabName)
	{
		return Resources.Load<GameObject>(prefabName);
	}
	
	public static AudioClip LoadAudioClip(string audioClipName)
	{
		return Resources.Load<AudioClip>(audioClipName);
	}
}
