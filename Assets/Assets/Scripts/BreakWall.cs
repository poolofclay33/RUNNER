using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {

    public Animator _anim;
    public GameObject _wallHole;
    public ParticleSystem _dust;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WallBreak")
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                _wallHole.SetActive(false);
                _dust.Play();
            }
        }
    }
}
