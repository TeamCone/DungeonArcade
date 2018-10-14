using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private GameObject _bgmPlayer;
    [SerializeField] private GameObject _sfxPlayer;
	
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

        
    public void PlayBgm(string bgmName)
    {
        var bgm = ResourceFacade.LoadAudioClip(bgmName);
        var bgmAudioSource = _bgmPlayer.GetComponent<AudioSource>();
        bgmAudioSource.clip = bgm;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public async void PlaySfx(string sfxName)
    {
        var sfx = ResourceFacade.LoadAudioClip(sfxName);
        var sfxAudioSource = _sfxPlayer.AddComponent<AudioSource>();
        sfxAudioSource.clip = sfx;
        sfxAudioSource.Play();
            
        await PlaySfxDelay(sfxAudioSource, sfxAudioSource.clip.length);
    }

    private IEnumerator PlaySfxDelay(AudioSource audioSource, float audioSourceLength)
    {
        yield return  new WaitForSeconds(audioSourceLength);
        Destroy(audioSource);
    }

	
 


	
}