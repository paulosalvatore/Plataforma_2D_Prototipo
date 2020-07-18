using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick_Test : MonoBehaviour
{
    private int _direction = 1;

    private int _speedDirection = 1;

    public float speed = 5f;

    public float speedMinX;
    public float speedMaxX;

    public float speedChangeVelocity;

    public float minX;
    public float maxX;

    private Rigidbody2D _rb;

    public float constantSpeedY = -1f;

    public float jumpForce = 300f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        speed += Time.deltaTime * _speedDirection * speedChangeVelocity;

        if (_speedDirection == -1 && speed < speedMinX
            || _speedDirection == 1 && speed > speedMaxX)
        {
            _speedDirection *= -1;
        }

        // transform.Translate(Vector2.right * (speed * _direction * Time.deltaTime));
        _rb.velocity = new Vector2(
            speed * _direction,
            _rb.velocity.y + constantSpeedY
        );

        if (_direction == -1 && transform.position.x < minX
            || _direction == 1 && transform.position.x > maxX)
        {
            _direction *= -1;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _rb.velocity = Vector3.zero;
            _rb.AddForce(Vector2.up * jumpForce);
        }
    }
}
