using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWatermelonHat : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.F;
    private MeshRenderer watermelonHat;
    [SerializeField]
    private float interactDistance;

    // Start is called before the first frame update
    void Start()
    {
        watermelonHat = GameObject.FindGameObjectWithTag("Hat").GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey) && Vector3.Distance(transform.position, watermelonHat.transform.position) < interactDistance)
        {
            watermelonHat.enabled = true;
            Destroy(this);
        }
    }
}
