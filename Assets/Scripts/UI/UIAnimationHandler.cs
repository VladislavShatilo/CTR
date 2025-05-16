using DG.Tweening;
using System.Collections;
using UnityEngine;

public class UIAnimationHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _defaultDuration = 0.3f;

    [SerializeField] private Ease _defaultEase = Ease.OutCubic;

    public IEnumerator AnimateMove(RectTransform rectTransform, Vector2 targetPos,
        float delay = 0.05f, float? duration = null, Ease? ease = null)
    {
        yield return new WaitForSeconds(delay);

        yield return rectTransform
        .DOAnchorPos(targetPos, duration ?? _defaultDuration)
        .SetEase(ease ?? _defaultEase)
        .SetUpdate(true)
        .WaitForCompletion();
    }
}