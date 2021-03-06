﻿using System;
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

    
    public static void LocalMove(Transform transform, Vector3 position, float duration, Action onComplete = null,bool isSpeedbased =false, float delay = 0)
    {
        transform.DOLocalMove(position, duration).SetEase(Ease.Linear).SetDelay(delay).SetSpeedBased(isSpeedbased).OnComplete(delegate
        {
            onComplete?.Invoke();
        });
    }
    
    public static void Move(Transform transform, Vector3 position, float duration, Action onComplete = null, bool isSpeedbased =false, float delay = 0)
    {
        transform.DOLocalMove(position, duration).SetEase(Ease.Linear).SetDelay(delay).SetSpeedBased(isSpeedbased).OnComplete(delegate
        {
            onComplete?.Invoke();
        });
    }
    
    
}
