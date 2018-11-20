using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMovementScript : MonoBehaviour {

    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;

    public bool _isGrounded;
    private Transform _groundChecker;

    public Animator _anim;

    public float moveSpeed;

    public float jumpForce;

    public Transform pivot;

    public GameObject playerModel;

    public float rotateSpeed;

    //public CharacterController controller;
    private Rigidbody _rb;

    private Vector3 moveDirection;
    public float gravityScale;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _groundChecker = this.transform;
        _anim.GetComponent<Animator>();
        _isGrounded = true;
        GetComponent<Mover>().enabled = false;
    }

    private void Update()
    {
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -1f * Physics.gravity.y), ForceMode.VelocityChange);

            _anim.SetBool("Grounded", _isGrounded);
        }

        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        _anim.SetBool("Grounded", _isGrounded);


        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        _rb.AddForce(moveDirection * Time.deltaTime * moveSpeed);

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        _anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) * Mathf.Abs(Input.GetAxis("Horizontal"))));
    }
}
