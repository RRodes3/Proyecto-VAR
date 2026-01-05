using UnityEngine;
using UnityEngine.SceneManagement;

public class AltoLogica : MonoBehaviour
{
    [SerializeField] TriggerBasicLogic enAreaDeAlto;
    [SerializeField] TriggerBasicLogic areaEnd;
    [SerializeField] Rigidbody carro;

    bool pierde = true;

    void Update() {
        if(areaEnd.atLeastOneCollision) {
            if(pierde) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else {
                Debug.Log("Pasa nivel!");
                Application.Quit();
            }
        }

        if(!enAreaDeAlto.currentCollision) return;

        Vector2 velocityWOY = new Vector2(carro.linearVelocity.x, carro.linearVelocity.z);
        
        if(velocityWOY.magnitude == 0) pierde = false; // Se detuvo, puede ganar
    }
}
