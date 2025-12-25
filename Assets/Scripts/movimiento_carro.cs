using UnityEngine;
using UnityEngine.InputSystem;

public class movimiento_carro : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Camera MainCamera;

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
        float currentSpeed = velocidad;

        Vector2 mVector = Move.action.ReadValue<Vector2>();

        float moviminetoVertical = mVector.y;
        float moviminetoHorizontal = mVector.x;

        transform.Rotate(0, mVector.x * velocidadGiro * Time.deltaTime, 0);

        if(Mathf.Abs(moviminetoVertical) > 0.01f)
        {
            Vector3 movimiento = transform.forward * moviminetoVertical * velocidad;
            cController.Move(movimiento * Time.deltaTime);
            
        }

    }

}
