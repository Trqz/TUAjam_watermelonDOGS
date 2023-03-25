using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerInput : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 movementInput;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotateSpeed;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movementInput = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movementInput;
        //movementInput = transform.TransformDirection(movementInput);
        _characterController.Move(movementInput * movementSpeed * Time.deltaTime);

        transform.LookAt(new Vector3(movementInput.x, 0, movementInput.z));


    }
}
