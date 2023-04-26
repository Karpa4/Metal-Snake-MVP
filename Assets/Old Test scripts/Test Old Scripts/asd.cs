using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asd : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 dir;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private float newDirection = 0;
    private float currentDirection = 0;

    private void Update()
    {
        if(Mathf.Abs(newDirection - currentDirection) > 3)
        {
            TruckRotation();
        }
        rb.velocity = transform.up * speed * Time.deltaTime;
    }

    public void ChangeDirection(float s)
    {
        newDirection = s;
    }

    private void TruckRotation()
    {
        if (newDirection - 180 > currentDirection)
        {
            currentDirection += 360;
        }
        else if (newDirection + 180 < currentDirection)
        {
            currentDirection -= 360;
        }

        if (newDirection > currentDirection)
        {
            currentDirection += rotationSpeed * Time.deltaTime;
        }
        else
        {
            currentDirection -= rotationSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, currentDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.contacts[0].normalImpulse);
        Debug.Log(collision.contacts[0].tangentImpulse);
    }
}
