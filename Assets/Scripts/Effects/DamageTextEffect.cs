using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextEffect : MonoBehaviour
{
    private TextMeshPro enemyDamageText;

    [Header("Text Animation Settings")]
    [SerializeField] private float textParticleDuration = 0.2f;
    [SerializeField] private float textScaleAnimation = 0.08f;
    [SerializeField] private float textMoveAnimation = 0.4f;

    private Vector3 textOriginalScale;
    private Vector3 textOriginalPosition;
    private List<LTDescr> currentTweens = new List<LTDescr>();

    private void Start()
    {
        enemyDamageText = GetComponent<TextMeshPro>();
        enemyDamageText.text = "";
        enemyDamageText.color = Color.white;
        textOriginalScale = enemyDamageText.rectTransform.localScale;
        textOriginalPosition = enemyDamageText.rectTransform.localPosition;
    }

    public void AnimateDamageText(float damage)
    {
        if (currentTweens.Count > 0)
            OnAnimationComplete();

        enemyDamageText.text = damage.ToString();

        currentTweens.Add(LeanTween.scale(enemyDamageText.rectTransform, Vector3.one * textScaleAnimation, textParticleDuration).setLoopPingPong(1).setOnComplete(OnAnimationComplete));
        currentTweens.Add(LeanTween.move(enemyDamageText.rectTransform, enemyDamageText.rectTransform.localPosition + Vector3.up * textMoveAnimation, textParticleDuration).setLoopPingPong(1).setOnComplete(OnAnimationComplete));
    }

    private void OnAnimationComplete()
    {
        enemyDamageText.text = "";
        enemyDamageText.rectTransform.localScale = textOriginalScale;
        enemyDamageText.rectTransform.localPosition = textOriginalPosition;
        currentTweens.ForEach(tween => LeanTween.cancel(tween.id));
        currentTweens.Clear();
    }
}
