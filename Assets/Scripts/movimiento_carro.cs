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
    [SerializeField] GameObject ruedas;


    [Header("Movimiento")]
    [SerializeField] float velocidad = 2.0f;
    [SerializeField] float velocidadGiro = 10.0f;
    [SerializeField] InputActionReference Move;

    CharacterController cController;
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
    
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mVector = Move.action.ReadValue<Vector2>();

        float moviminetoVertical = mVector.y;
        float moviminetoHorizontal = mVector.x;

        if(Mathf.Abs(moviminetoHorizontal)> 0.01f)
        {

            foreach (Transform rueda in ruedas.GetComponentsInChildren<Transform>())
            {
                rueda.transform.Rotate(0,moviminetoHorizontal * Time.deltaTime, 0);
            }
        }

        if (Mathf.Abs(moviminetoVertical) > 0.01f)
        {
            Vector3 movimiento = moviminetoVertical * velocidad * transform.forward;

            cController.Move(movimiento * Time.deltaTime);
            
        }

    }

}
