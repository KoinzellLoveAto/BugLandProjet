using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private SpriteRenderer m_spriteRenderer;

    [SerializeField]
    private float TimeToFade = 2f;


    public UnityEvent OnEndFade;

    private float currentFadeTime = 0;
    int m_triggerDestroy = Animator.StringToHash("Destroy");


    public void PlayDestroy()
    {

        m_animator.SetTrigger(m_triggerDestroy);
        FadeRoutine();
    }

    private IEnumerator FadeRoutine()
    {
        while (currentFadeTime <= TimeToFade)
        {
            yield return new WaitForEndOfFrame();
            currentFadeTime += Time.deltaTime;
            float spriteFade = Tools.Remap(currentFadeTime, 0, TimeToFade, 1, 0);

            Color spriteColor = m_spriteRenderer.color;
            spriteColor.a = spriteFade;
            m_spriteRenderer.color = spriteColor;
        
        }

        OnEndFade?.Invoke();
    }

}
