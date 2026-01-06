using System;
using System.Collections;
using UnityEngine;

public class Fade_Control : MonoBehaviour
{
    [Header("General")]
    [SerializeField] CanvasGroup grupo;
    [SerializeField] float duración = 5f;

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanva(grupo, 1, 0, duración));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanva(grupo, 0, 1, duración));
    }

    private IEnumerator FadeCanva(CanvasGroup grupo, float inicio, float final, float duración)
    {
        float tiempoTranscurrido = 0f;
        grupo.alpha = inicio;

        while (tiempoTranscurrido < duración)
        {
            tiempoTranscurrido += Time.deltaTime;
            grupo.alpha = Mathf.Lerp(inicio, final, tiempoTranscurrido / duración);
            yield return null;
        }

        grupo.alpha = final;
    }
}
