using UnityEngine;
using UnityEngine.SceneManagement;

public class Botonesmenú : MonoBehaviour
{
    //Para hacer el commit de la rama o como se diga
    [Header("Canvas")]
    [SerializeField] GameObject principal;
    [SerializeField] GameObject niveles;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ElegirNivel()
    {
        principal.SetActive(false);
        niveles.SetActive(true);
    }

    public void Semaforo()
    {
        SceneManager.LoadScene("Juego_Semaforo");
    }

    public void Carriles()
    {
        SceneManager.LoadScene("Juego_CedaElPaso");
    }

    public void SeñalAlto()
    {
        SceneManager.LoadScene("Juego_Alto");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
