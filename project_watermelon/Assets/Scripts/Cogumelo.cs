using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cogumelo : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.F;
    public CharacterHigh characterHigh;
    [SerializeField] EventReference eatSFX;
    // Start is called before the first frame update
    void Start()
    {
        characterHigh = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHigh>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, characterHigh.transform.position));
        if (Input.GetKeyDown(interactKey) && Vector3.Distance(transform.position, characterHigh.transform.position) < 2) {
            characterHigh.highAmmount += 100;
            RuntimeManager.PlayOneShot(eatSFX);
            gameObject.SetActive(false);
        }
    }

  
}
