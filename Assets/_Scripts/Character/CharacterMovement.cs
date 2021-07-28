using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    [SerializeField] private Animator anim;
    private Vector3 charMovement;

    [SerializeField] private float walkSpeed = 7;
    [SerializeField] private float runSpeed = 10;
    [SerializeField] private float jumpForce = 1;

    private Vector2 screenBounds;
    private float _width, _height;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        _width = _collider.bounds.size.x / 2;
        _height = _collider.bounds.size.y / 2;
    }

    void LateUpdate()
    {
        charMovement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("run", true);
            transform.position += charMovement * runSpeed * Time.deltaTime;
        }
        else
        {
            anim.SetBool("run", false);
            transform.position += charMovement * walkSpeed * Time.deltaTime;
        }

        //Turn character sprites around depending on his direction
        if (!Mathf.Approximately(0, charMovement.x))
            transform.rotation = charMovement.x > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            anim.SetTrigger("jump");
        }

        anim.SetBool("walk", Mathf.Abs(charMovement.x) > 0);

    }

    public void Jump()
    {
        _rigidBody.AddForce(new Vector3(0, jumpForce), ForceMode2D.Impulse);
    }

    //Is touching ground
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0, Vector2.down,
            _collider.bounds.size.y / 2, 1 << 7);

        return hit.collider != null;
    }
}
