using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{

    [SerializeField] private Image _transitionImage;
    [SerializeField] private Text _transitionText;
    private string _unloadScene;
    private string _loadScene;
    private Vector3 _imageOriginalPosition;

    private void Awake()
    {
        _imageOriginalPosition = _transitionImage.transform.position;
    }

    private void OnEnable()
    {
        _transitionImage.transform.position = new Vector3(_imageOriginalPosition.x + 100,_imageOriginalPosition.y,_imageOriginalPosition.z);
		
        var transitionText = PlayerPrefs.GetString("TransitionText", "");
        _transitionText.text = transitionText;
		
        _unloadScene = PlayerPrefs.GetString("UnloadScene", "");
        _loadScene = PlayerPrefs.GetString("LoadScene", "Map1Scene");

        Debug.Log(_unloadScene);
        Debug.Log(_loadScene);
        AnimateTransition();
    }

    private void AnimateTransition()
    {
        TweenFacade.LocalMove(_transitionImage.transform, _imageOriginalPosition, 0.5f, async delegate
        {
            if (!string.IsNullOrEmpty(_unloadScene))
            {
                await SceneManager.UnloadSceneAsync(_unloadScene);
            }

            await AnimationDelay();
            await SceneManager.LoadSceneAsync(_loadScene, LoadSceneMode.Additive);
		
            TweenFacade.LocalMove(_transitionImage.transform, new Vector3(_imageOriginalPosition.x - 5000,_imageOriginalPosition.y, _imageOriginalPosition.z), 0.5f, delegate
            {
                SceneManager.UnloadSceneAsync("TransitionScene");
            });
        });
		
		
    }

    private IEnumerator AnimationDelay()
    {
        yield return  new WaitForSeconds(0.5f);
    }


}