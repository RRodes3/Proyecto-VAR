using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mensaje_Control : MonoBehaviour
{
    [Header("General")]
    [SerializeField] string mensaje;
    [SerializeField] float ancho;
    [SerializeField] float alto;
    [SerializeField] float margen;

    [Header("Color")]
    [SerializeField] Color fondo;
    [SerializeField] Color letra;

    Canvas canvas;
    TextMeshProUGUI texto;
    Image imagen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        texto = canvas.GetComponentInChildren<TextMeshProUGUI>();
        imagen = canvas.GetComponentInChildren<Image>();
        ConstruirMensaje();
    }
    private void ConstruirMensaje()
    {

        Vector2 tamañoCompleto = new(ancho, alto);
        Vector2 tamañoMargen = new(ancho - margen, alto - margen);

        RectTransform rectanguloCanvas = canvas.GetComponent<RectTransform>();
        rectanguloCanvas.sizeDelta = tamañoCompleto;

        RectTransform rectanguloTexto = texto.GetComponent<RectTransform>();
        rectanguloTexto.sizeDelta = tamañoMargen;

        RectTransform rectanguloImagen = imagen.GetComponent<RectTransform>();
        rectanguloImagen.sizeDelta = tamañoCompleto;


        imagen.color = fondo;

        texto.color = letra;
        texto.text = mensaje;

        Ocultar();

    }

    public void Mostrar()
    {
        canvas.gameObject.SetActive(true);
    }

    public void Ocultar()
    {
        canvas.gameObject.SetActive(false);

    }
}
