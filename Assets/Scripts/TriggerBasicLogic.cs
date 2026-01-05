using UnityEngine;

public class TriggerBasicLogic : MonoBehaviour
{

    public bool atLeastOneCollision = false;
    public bool currentCollision = false;
    Collider m_collider;

    void Start() {
        m_collider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag != "Player" && other.tag != "Carro") return;

        atLeastOneCollision = true;
        currentCollision = true;
    }

    void OnTriggerExit(Collider other) {
        if(other.tag != "Player" && other.tag != "Carro") return;

        currentCollision = false;
    }
}
