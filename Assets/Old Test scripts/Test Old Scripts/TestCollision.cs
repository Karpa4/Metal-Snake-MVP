using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;


public class TestCollision : MonoBehaviour
{
    public bool OnCollision = false;
    public float RotSpeed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantVariables.ObstacleTag))
        {
            OnCollision = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        float tempOld = transform.eulerAngles.z;
        float tempNew = tempOld;
        if (collision.contacts[0].point.x > transform.position.x)
        {
            tempNew = tempOld - RotSpeed * Time.deltaTime;
        }
        else
        {
            tempNew = tempOld + RotSpeed * Time.deltaTime;
        }
        Debug.Log(tempNew + "." + tempOld);
        transform.rotation = Quaternion.Euler(0f, 0f, tempNew);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantVariables.ObstacleTag))
        {
            OnCollision = false;
        }
    }
}
