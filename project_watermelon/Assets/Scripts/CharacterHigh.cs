using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterHigh : MonoBehaviour
{
    public float highAmmount;
    public float highDecreaseRate = 2;
    public float cameraShakeIncrease = 10;
    [SerializeField]
    private CinemachineFreeLook highCamera;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (highAmmount > 0)
        {
            RuntimeManager.StudioSystem.setParameterByName("High", highAmmount);
            highCamera.Priority = 11;
            highAmmount -= Time.deltaTime * highDecreaseRate;
        }
        else
        {
            highCamera.Priority = 9;
        }
    }
}
