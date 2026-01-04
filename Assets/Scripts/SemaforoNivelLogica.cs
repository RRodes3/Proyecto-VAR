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
            if(pierde) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else {
                Debug.Log("Pasa nivel!");
                Application.Quit();
            }
        }
    }

    IEnumerator Siga() {
        yield return new WaitForSeconds(10f);
        pierde = false;
        luzRoja.SetActive(false);
        luzAmarilla.SetActive(false);
        luzVerde.SetActive(true);
    }
}
