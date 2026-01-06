using System.Collections;
using UnityEngine;

public class Fade_Controlador : MonoBehaviour
{
    [Header("General")]
    [SerializeField] CanvasGroup grupo;
    [SerializeField] float duración = 5f;
    [SerializeField] bool fadeIn = false;

    private void Start()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut(); 
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanva(grupo, grupo.alpha, 0, duración));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanva(grupo, grupo.alpha, 1, duración));
    }

    private IEnumerator FadeCanva(CanvasGroup grupo, float inicio, float final, float duración)
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duración)
        {
            tiempoTranscurrido += Time.deltaTime;
            grupo.alpha = Mathf.Lerp(inicio, final, tiempoTranscurrido / duración);
            yield return null;
        }
        grupo.alpha = final;
    }
}
