using UnityEngine;
using UnityEngine.InputSystem;

public class Pausa : MonoBehaviour
{
    [Header("Mensaje")]
    [SerializeField] Mensaje_Control mensaje;
    [SerializeField] InputActionReference boton;

    bool pausado = false;

    private void OnEnable()
    {
        boton.action.Enable();
    }

    private void OnDisable()
    {
        boton.action.Disable();
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

        mensaje.Mostrar();
    }

    private void Despausar()
    {
        pausado = false;
        Time.timeScale = 1f;

        mensaje.Ocultar();
    }
}
