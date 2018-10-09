using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public static class TweenFacade  {

    public static void FlashTextMesh(TextMeshProUGUI text, float duration)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(text.DOFade(0, duration));
        sequence.Append(text.DOFade(1, duration));
        sequence.SetLoops(-1);
    }
    
    public static void FlashImageMesh(Image image, float duration)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOFade(0, duration));
        sequence.Append(image.DOFade(1, duration));
        sequence.SetLoops(-1);
    }
    
}
