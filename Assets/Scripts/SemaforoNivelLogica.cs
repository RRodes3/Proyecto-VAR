using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SemaforoNivelLogica : MonoBehaviour
{
    [SerializeField] TriggerBasicLogic amarilloTrigger;
    [SerializeField] TriggerBasicLogic rojoTrigger;
    [SerializeField] GameObject finalDeJuego;
    TriggerBasicLogic finalDeJuegoTrigger;

    [SerializeField] GameObject luzRoja;
    [SerializeField] GameObject luzAmarilla;
    [SerializeField] GameObject luzVerde;

    [SerializeField] GameObject canvasGanar;
    [SerializeField] GameObject canvasPerder;

    bool pierde = true;
    
    void Start() {
        finalDeJuegoTrigger = finalDeJuego.GetComponent<TriggerBasicLogic>();
    }

    void Update() {
        if(amarilloTrigger.atLeastOneCollision && !rojoTrigger.atLeastOneCollision) {
            luzRoja.SetActive(false);
            luzAmarilla.SetActive(true);
            luzVerde.SetActive(false);
        }

        if(rojoTrigger.atLeastOneCollision == true && pierde) {
            luzRoja.SetActive(true);
            luzAmarilla.SetActive(false);
            luzVerde.SetActive(false);
            StartCoroutine(Siga());
        }

        if(finalDeJuegoTrigger.atLeastOneCollision) {
            if(pierde) StartCoroutine(Perder());
            else StartCoroutine(Ganar());
        }
    }

    IEnumerator Ganar() {
        canvasGanar.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menú inicial");
    }

    IEnumerator Perder() {
        canvasPerder.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Siga() {
        yield return new WaitForSeconds(10f);
        pierde = false;
        luzRoja.SetActive(false);
        luzAmarilla.SetActive(false);
        luzVerde.SetActive(true);
    }
}
