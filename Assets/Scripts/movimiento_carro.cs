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
    [SerializeField] InputActionReference Accelerate;
    [SerializeField] InputActionReference Break;

    Rigidbody rigidBody;
    float limite;
    float distanciaEntreEjes;
    float anguloAnterior = 0f;
    float anguloReal = 0f;
    float giroRuedas = 0f;
    float accelerate;
    float breaking;
    private void OnEnable()
    {
        Accelerate.action.Enable();
        Break.action.Enable();
    }

    private void OnDisable()
    {
        Accelerate.action.Disable();
        Break.action.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        limite = 360.0f / relacion;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        distanciaEntreEjes = (ejeDelantero.transform.position - ejeTrasero.transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        accelerate = Accelerate.action.ReadValue<float>();
        breaking = Break.action.ReadValue<float>();

        /* 
         * Los grados en unity se reinician si superamos el limite, en este caso si pasamos de 360 vamos al 0 nuevamente (de 361 pasamos a 1 por ejemplo o de -1 a 359) 
         * DeltaAngle toman los grados más cortos para ir de a -> b, en este caso para ir de 0 grados a los grados de rotación del volante
         * Como se reinician los grados, pasar de 0 a 359 de golpe toma tan solo -1 grados, lo que representa girar el volante a la izquierda
         * Por ello podemos pasar de 0 - 360 a -180 - 180 grados.
        */
        float anguloCorrecto = Mathf.DeltaAngle(0f, volante.transform.localEulerAngles.y + 90f);

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
        giroRuedas = Mathf.Clamp((anguloReal / relacion), -limite, limite);

        anguloAnterior = anguloCorrecto;


        //De un objeto padre se obtienen las llantas delanteras y a cada una se le aplica la rotación 
        foreach (Transform rueda in ejeDelantero.transform)
        {
            rueda.localRotation = Quaternion.Euler(0f, 0f, giroRuedas);
        }
    }

    void FixedUpdate()
    {
        //Cambiar la condición a cuando se apriete el boton para avanzar
        if(accelerate == 1 || breaking == 1)
        {
            Vector3 movimiento = velocidad * transform.forward;
            if(accelerate == 0 && breaking == 1) movimiento *= -1f;
            rigidBody.AddForce(movimiento, ForceMode.Force);
            
            float radianes = Mathf.Deg2Rad * giroRuedas;
            float giroCarro = (rigidBody.linearVelocity.magnitude / distanciaEntreEjes) * Mathf.Tan(radianes);

            // Debug.Log("GiroCarro: " + giroCarro);

            Quaternion deltaRotation = Quaternion.Euler(0f, giroCarro * Mathf.Rad2Deg * Time.deltaTime, 0f);
            rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
        } 
    }
}
