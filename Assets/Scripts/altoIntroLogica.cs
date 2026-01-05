using UnityEngine;
using UnityEngine.SceneManagement;

public class altoIntroLogica : MonoBehaviour
{
    [SerializeField] TriggerBasicLogic nextRoom;

    void Update() {
        if(nextRoom.atLeastOneCollision) SceneManager.LoadScene("Juego_Alto");
    }
}
