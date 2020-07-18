using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 3f;

    public float jumpForce = 200f;

    private bool _isGrounded;

    private bool _doubleJumpAvailable;

    public Vector2 direction = Vector2.down;
    public float distance = 2f;

    public LayerMask groundLayer;

    public Transform[] collisionPoints;

    private bool _jumpPressed;

    private bool _jumpedOnLastFrame;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
        }

        Fall();
    }

    private void FixedUpdate()
    {
        Move();

        Jump();

        UpdateIsGrounded();
    }

    private void Jump()
    {
        if (_jumpedOnLastFrame)
        {
            _jumpedOnLastFrame = false;
        }

        if (!_jumpPressed)
        {
            return;
        }

        if (_doubleJumpAvailable)
        {
            if (!_isGrounded)
            {
                _doubleJumpAvailable = false;
            }

            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);

            _jumpedOnLastFrame = true;
        }

        _jumpPressed = false;
    }

    private void Fall()
    {
        var downArrowPressed = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);

        if (downArrowPressed && _collidingWithPlatforms.Count > 0)
        {
            foreach (var collidingWithPlatform in _collidingWithPlatforms)
            {
                collidingWithPlatform.rotationalOffset = 180;
            }
        }
    }

    private void UpdateIsGrounded()
    {
        _isGrounded = false;

        foreach (var collisionPoint in collisionPoints)
        {
            var startPosition = collisionPoint.position;
            var raycastHit = Physics2D.Raycast(startPosition, direction, distance, groundLayer);
            Debug.DrawRay(startPosition, direction * distance, raycastHit ? Color.green : Color.red);

            if (raycastHit)
            {
                _isGrounded = true;
            }
        }
    }

    private void Move()
    {
        var h = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(h * speed, rb.velocity.y);
    }

    private List<PlatformEffector2D> _collidingWithPlatforms = new List<PlatformEffector2D>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionLayer = 1 << collision.gameObject.layer;

        var isCollidingWithGroundLayer = (groundLayer & collisionLayer) == collisionLayer;

        if (_isGrounded && isCollidingWithGroundLayer)
        {
            _doubleJumpAvailable = true;
        }

        if (collision.gameObject.CompareTag("FallThroughPlatform") && collision.enabled && _isGrounded)
        {
            var platformEffector2D = collision.gameObject.GetComponent<PlatformEffector2D>();

            _collidingWithPlatforms.Add(platformEffector2D);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FallThroughPlatform"))
        {
            var platformEffector2D = collision.gameObject.GetComponent<PlatformEffector2D>();

            if (_collidingWithPlatforms.Contains(platformEffector2D))
            {
                _collidingWithPlatforms.Remove(platformEffector2D);
            }

            if (!collision.enabled || !_isGrounded)
            {
                platformEffector2D.rotationalOffset = 0;
            }
        }
    }
}
