using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class TweenFacade  
{
    private static Sequence _throwItemEffectSequence;

    public static void FlashTextMesh(TextMeshProUGUI text, float duration)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(text.DOFade(0, duration));
        sequence.Append(text.DOFade(1, duration));
        sequence.SetLoops(-1);
    }
    
    public static void FlashImage(Image image, float duration)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOFade(0, duration));
        sequence.Append(image.DOFade(1, duration));
        sequence.SetLoops(-1);
    }
    
    public static async void CharacterInvulnerable(SpriteRenderer spriteRenderer, float duration)
    {
        await CharacterInvulnerableCoroutine(spriteRenderer, duration);
    }

    private static IEnumerator CharacterInvulnerableCoroutine(SpriteRenderer spriteRenderer, float duration)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(spriteRenderer.DOFade(0, 0.1f));
        sequence.Append(spriteRenderer.DOFade(1, 0.1f));
        sequence.SetLoops(-1);
        
        yield return  new WaitForSeconds(duration);
        sequence.Kill();
        spriteRenderer.DOFade(1, 0.1f);
    }
    
    public static void ThrowItemEffect(SpriteRenderer spriteRenderer)
    {
        _throwItemEffectSequence = DOTween.Sequence();
        _throwItemEffectSequence.Append(spriteRenderer.DOColor(Color.red, 0.1f));
        _throwItemEffectSequence.Append(spriteRenderer.DOColor(Color.white, 0.1f));
        _throwItemEffectSequence.SetLoops(-1);
    }

    public static void StopThrowItemEffect(SpriteRenderer spriteRenderer)
    {
        _throwItemEffectSequence.Kill();
        spriteRenderer.DOColor(Color.white, 0.1f);
    }
    
    
}
