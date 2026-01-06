using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AltoLogica : MonoBehaviour
{
    [SerializeField] TriggerBasicLogic enAreaDeAlto;
    [SerializeField] TriggerBasicLogic areaEnd;
    [SerializeField] Rigidbody carro;

    [SerializeField] GameObject canvasGanar;
    [SerializeField] GameObject canvasPerder;

    [SerializeField] Fade_Control fade;

    bool pierde = true;

    void Update() {
        if(areaEnd.atLeastOneCollision) {
            if(pierde) StartCoroutine(Perder());
            else StartCoroutine(Ganar());
        }

        if(!enAreaDeAlto.currentCollision) return;

        Vector2 velocityWOY = new Vector2(carro.linearVelocity.x, carro.linearVelocity.z);
        
        if(velocityWOY.magnitude == 0) pierde = false; // Se detuvo, puede ganar
    }

    IEnumerator Ganar() {
        canvasGanar.SetActive(true);
        fade.FadeOut();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menú inicial");
    }

    IEnumerator Perder() {
        canvasPerder.SetActive(true);
        fade.FadeOut();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
