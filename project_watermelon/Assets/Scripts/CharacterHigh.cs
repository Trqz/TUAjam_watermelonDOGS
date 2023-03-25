using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHigh : MonoBehaviour
{
    public float highAmmount;
    public float highDecreaseRate = 2;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (highAmmount > 0)
        {
            RuntimeManager.StudioSystem.setParameterByName("High", highAmmount);
            highAmmount -= Time.deltaTime * highDecreaseRate;
        }
    }
}
