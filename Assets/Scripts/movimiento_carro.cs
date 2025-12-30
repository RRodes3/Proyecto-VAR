using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class movimiento_carro : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject volante;
    [SerializeField] GameObject ejeDelantero;
    [SerializeField] GameObject ejeTrasero;

    [Header("Movimiento")]
    [SerializeField] float relacion = 16.0f;
    [SerializeField] float velocidad = 2.0f;
    [SerializeField] float velocidadGiro = 50f;
    [SerializeField] InputActionReference Move;

    CharacterController cController;
    float limite;
    float anguloAnterior = 0f;
    float anguloReal = 0f;
    private void OnEnable()
    {
        Move.action.Enable();
    }

    private void OnDisable()
    {
        Move.action.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cController = GetComponent<CharacterController>();
        limite = 360.0f / relacion;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mVector = Move.action.ReadValue<Vector2>();

        float movimientoVertical = mVector.y;
        float movimientoHorizontal = mVector.x;

        //Esto es solo para probar el movimineto con las teclas, eliminarlo
        if (Mathf.Abs(movimientoHorizontal) > 0.01f)
        {
            volante.transform.Rotate(0f, 0f, movimientoHorizontal * velocidadGiro * Time.deltaTime);
        }

        //Toma los grados de rotación del volante, puede ir de 0 a 360 grados.
        float gradosVolante = volante.transform.eulerAngles.z;

        /* 
         * Los grados en unity se reinician si superamos el limite, en este caso si pasamos de 360 vamos al 0 nuevamente (de 361 pasamos a 1 por ejemplo o de -1 a 359) 
         * DeltaAngle toman los grados más cortos para ir de a -> b, en este caso para ir de 0 grados a los grados de rotación del volante
         * Como se reinician los grados, pasar de 0 a 359 de golpe toma tan solo -1 grados, lo que representa girar el volante a la izquierda
         * Por ello podemos pasar de 0 - 360 a -180 - 180 grados.
        */
        float anguloCorrecto = Mathf.DeltaAngle(0f, volante.transform.eulerAngles.z);

        /* 
         * En este caso aplicamos lo de DeltaAngle pero para el reinicio de grados de -180 a 180
         * Se guarda los grados anteriores y los actuales (ya corregidos) para saber si se aplico el reinicio y cuantos grados hay de diferencia
         * Estos grados se suman para obtener los grados reales que llevamos
         * Lo que nos permite pasar de -180 - 180 a -360 - 360 grados
         * Clamp es solo para limitar el valor de la operacion, que no se pase de -360 o de 360
        */
        anguloReal += Mathf.Clamp(Mathf.DeltaAngle(anguloAnterior, anguloCorrecto), -360f, 360f);

        /*
         * Segun google, hay un valor llamado steering ratio, que es la relacion de grados rotados por el volante con los grados rotados por las llantas
         * En este caso las relaciones para coches cotidianos van de 12:1 a 20:1 y la más común es de 16:1 (donde el 16 son los grados del volante y 1 el de las llantas)
         * Osea por cada 16 grados de rotación del volante, las llantas rotaran 1 grado
         * El clamp es para que no pasen el limite, este solo se calcula de dividir 360 grados entre 16
        */
        float giroRuedas = Mathf.Clamp((anguloReal / relacion), -limite, limite);

        //Se acutualiza el angulo anterior
        anguloAnterior = anguloCorrecto;


        //De un objeto padre se obtienen las llantas delanteras y a cada una se le aplica la rotación 
        foreach (Transform rueda in ejeDelantero.transform)
        {
            rueda.localRotation = Quaternion.Euler(0f, giroRuedas, 90f);
        }

        //Cambiar la condición a cuando se apriete el boton para avanzar
        if (Mathf.Abs(movimientoVertical) > 0.01f)
        {
            Vector3 movimiento = movimientoVertical * velocidad * transform.forward;
            cController.Move(movimiento * Time.deltaTime);

            float distancia = (ejeDelantero.transform.position - ejeTrasero.transform.position).magnitude;
            float radianes = Mathf.Deg2Rad * giroRuedas;
            float giroCarro = (velocidad / distancia) * Mathf.Tan(radianes);

            cController.transform.rotation *= Quaternion.Euler(0f, giroCarro * Mathf.Rad2Deg * Time.deltaTime, 0f);

        }



    }
}
