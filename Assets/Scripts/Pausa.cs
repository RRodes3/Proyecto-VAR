using UnityEngine;
using UnityEngine.InputSystem;

public class Pausa : MonoBehaviour
{
    [Header("Mensaje")]
    [SerializeField] GameObject mensaje;
    [SerializeField] InputActionReference boton;

    bool pausado;

    private void Start()
    {
        boton.action.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carro") && !pausado)
        {
            Pausar();
        }
    }

    private void Update()
    {
        if (!pausado) return;

        if (boton.action.WasPressedThisFrame())
        {
            Despausar();
        }

    }

    private void Pausar()
    {
        pausado = true;
        Time.timeScale = 0f;

        mensaje.gameObject.SetActive(true);
    }

    private void Despausar()
    {
        pausado = false;
        Time.timeScale = 1f;

        mensaje.gameObject.SetActive(false);
    }
}
