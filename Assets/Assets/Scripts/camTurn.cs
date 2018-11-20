using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTurn : MonoBehaviour {

    public Camera _cam;
    public Animator _camAnim;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");

        if (other.tag == "CamBlock")
        {
            Debug.Log("CAM");
            //_camAnim.Play("Swap");
            //_cam.transform.rotation *= Quaternion.Euler(0, 180, 0);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            _camAnim.Play("Swap");
        }
    }
}