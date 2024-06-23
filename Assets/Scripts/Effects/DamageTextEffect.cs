using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextEffect : MonoBehaviour
{
    TextMeshPro enemyDamageText;

    [Header("Text Animation Settings")]
    [SerializeField] float textParticleDuration = 0.2f;
    [SerializeField] float textScaleAnimation = 0.08f;
    [SerializeField] float textMoveAnimation = 0.4f;

    Vector3 textOriginalScale;
    Vector3 textOriginalPosition;
    List<LTDescr> currentTweens = new List<LTDescr>();

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

        currentTweens.Add(LeanTween.scale(enemyDamageText.rectTransform, new Vector3(textScaleAnimation, textScaleAnimation, textScaleAnimation), textParticleDuration).setLoopPingPong(1).setOnComplete(OnAnimationComplete));
        currentTweens.Add(LeanTween.move(enemyDamageText.rectTransform, enemyDamageText.rectTransform.localPosition + new Vector3(0, textMoveAnimation, 0), textParticleDuration).setLoopPingPong(1).setOnComplete(OnAnimationComplete));
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
