using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private MyTestMove move;
    public void Construct(MyTestMove move)
    {
        this.move = move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantVariables.ObstacleTag))
        {
            Debug.Log(collision.contacts[0].normalImpulse);
            Debug.Log(collision.contacts[0].tangentImpulse);
            move.ChangeDirection(collision.contacts[0].tangentImpulse);
        }
    }
}
