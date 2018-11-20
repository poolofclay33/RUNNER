using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class MovementTest : MonoBehaviour {

    public float Speed = 10f;
    public float JumpHeight = 4f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;
    public float sprint = 5f;
    public float dash = 5f;

    private Rigidbody _body;
    public Vector3 _inputs = Vector3.zero;
    public bool _isGrounded;
    private Transform _groundChecker;

    public Animator _anim;

    private Animator _camAnim;

    float direction = 1f;

    public GameObject _normalCam;
    public GameObject _secondCam;
    public GameObject _raveCam;

    public ParticleSystem _sparks;
    public ParticleSystem _poof;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = this.transform;
        _anim.GetComponent<Animator>();
        _isGrounded = true;
        GetComponent<Mover>().enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CamBlock")
        {
            StartCoroutine("Inverse");
        }

        if (other.tag == "RaveCaveBlock")
        {
            StartCoroutine("RaveCave");
        }

        if (other.tag == "RailTrigger")
        {
            GetComponent<Mover>().enabled = true;
            _sparks.Play();
            _anim.Play("Grind");
        }

        if(other.tag == "Battery")
        {
            _poof.Play();
        }
    }


    private bool hi = true;
    private bool bye = true;

    IEnumerator RaveCave()
    {
        if (bye == true)
        {
            _secondCam.SetActive(false);
            _raveCam.SetActive(true);
            yield return new WaitForSeconds(1f);
            transform.Rotate(0, 90, 0);
            bye = false;
        }
        else if (bye == false)
        {
            _secondCam.SetActive(true);
            _raveCam.SetActive(false);
            yield return new WaitForSeconds(.2f);
            bye = true;
        }
    }

    /*
    IEnumerator Inverse()
    {
        if(hi == true)
        {
            _normalCam.SetActive(false);
            _secondCam.SetActive(true);
            yield return new WaitForSeconds(.2f);
            direction = -1f;
            hi = false;
        }

        else if(hi == false)
        {
            _normalCam.SetActive(true);
            _secondCam.SetActive(false);
            yield return new WaitForSeconds(.2f);
            direction = 1f;
            hi = true;
        }
    }
    */

    public float distance = 5f;

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        _anim.SetBool("Grounded", _isGrounded);

        _inputs = Vector3.zero;

        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");

        if (_inputs != Vector3.zero)
        {
            Vector3 _dir = _normalCam.transform.forward;

            _dir = new Vector3(_dir.x, 0f, _dir.z).normalized;

            transform.forward = _inputs;

            _anim.SetFloat("Speed", _inputs.sqrMagnitude);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -1f * Physics.gravity.y), ForceMode.VelocityChange);

            _anim.SetBool("Grounded", _isGrounded);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
            _body.AddForce(dashVelocity * dash, ForceMode.VelocityChange);
            _anim.Play("Charge");
        }
        
        if(Input.GetKeyDown(KeyCode.F))
        {
            _anim.SetBool("Sliding", true);
            _anim.Play("Slide");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            _anim.Play("MegaJump");
        }

        /*
        if(Input.GetKey(KeyCode.LeftShift))
        {
            _inputs.x *= sprint;
            _anim.SetBool("Sprint", true);
            _inputs.z *= sprint;
        } else {
            _anim.SetBool("Sprint", false);
        }
        */
    }

    void FixedUpdate()
    {
       _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}