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
    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (highAmmount > 0)
        {
            RuntimeManager.StudioSystem.setParameterByName("High", highAmmount);
            highCamera.Priority = 11;
            highAmmount -= Time.deltaTime * highDecreaseRate;
            _animator.SetBool("isDrugged", true);
        }
        else
        {
            highCamera.Priority = 9;
            _animator.SetBool("isDrugged", false);
        }
    }
}
