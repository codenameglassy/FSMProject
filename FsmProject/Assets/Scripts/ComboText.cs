using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class ComboText : MonoBehaviour
{
    private CanvasGroup fadeCanvas;
    private RectTransform rectTransform;
    public float moveDistance;
    private TextMeshProUGUI comboTxt;
    private void Start()
    {
     
        rectTransform = GetComponent<RectTransform>();
        fadeCanvas = GetComponent<CanvasGroup>();
        comboTxt = GetComponent<TextMeshProUGUI>();

        fadeCanvas.alpha = 0.0f;
        StartCoroutine(TweenRoutine());
    }

    public void SetText(string string_)
    {
        comboTxt.text = string_;
    }

    IEnumerator TweenRoutine()
    {
        fadeCanvas.DOFade(1, 2f);
        yield return null;
        transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), .5f).SetEase(Ease.InOutBounce);

   
        // Move the text upwards using DOTween
        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + moveDistance, 1f)
            .SetEase(Ease.OutQuad) // Optional: Smooth easing
            .OnComplete(() => Destroy(gameObject)
            ); // Destroy the text after the animation*/
        yield return new WaitForSeconds(.5f);
        fadeCanvas.DOFade(0, 1f);
    }
}
