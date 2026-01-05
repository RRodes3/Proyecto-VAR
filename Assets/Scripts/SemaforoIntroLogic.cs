using UnityEngine;
using UnityEngine.SceneManagement;

public class SemaforoIntroLogic : MonoBehaviour
{
    [SerializeField] TriggerBasicLogic amarilloTrigger;
    [SerializeField] TriggerBasicLogic rojoTrigger;
    [SerializeField] TriggerBasicLogic verdeTrigger;
    [SerializeField] TriggerBasicLogic final;

    [SerializeField] GameObject luzRoja;
    [SerializeField] GameObject luzAmarilla;
    [SerializeField] GameObject luzVerde;

    void Update() {
        if(final.atLeastOneCollision) {
            SceneManager.LoadScene("Juego_Semaforo");
        }

        if(verdeTrigger.atLeastOneCollision && !amarilloTrigger.atLeastOneCollision && !rojoTrigger.atLeastOneCollision) {
            luzRoja.SetActive(false);
            luzAmarilla.SetActive(false);
            luzVerde.SetActive(true);
            return;
        }

        if(amarilloTrigger.atLeastOneCollision && !rojoTrigger.atLeastOneCollision) {
            luzRoja.SetActive(false);
            luzAmarilla.SetActive(true);
            luzVerde.SetActive(false);
            return;
        }

        if(rojoTrigger.atLeastOneCollision) {
            luzRoja.SetActive(true);
            luzAmarilla.SetActive(false);
            luzVerde.SetActive(false);
        }
    }
}
