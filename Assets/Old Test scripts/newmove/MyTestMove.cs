using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTestMove : MonoBehaviour
{
    [SerializeField] float distanceBetween = .2f;
    [SerializeField] float speed = 280;
    [SerializeField] float turnSpeed = 180;
    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    public float value;
    List<GameObject> snakeBody = new List<GameObject>();
    float countUp = 0;
    public static MyTestMove instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        CreateBodyParts();
    }

    void FixedUpdate()
    {
        if (bodyParts.Count > 0)
        {
            CreateBodyParts();
        }
        SnakeMovement();
    }

    public void ChangeDirection(float impulse)
    {
        snakeBody[0].GetComponent<Rigidbody2D>().angularVelocity = 0;
        snakeBody[0].transform.Rotate(new Vector3(0, 0, impulse * value));
    }

    void SnakeMovement()
    {
        snakeBody[0].GetComponent<Rigidbody2D>().velocity = snakeBody[0].transform.up * speed * Time.deltaTime;
        if (Input.GetAxis("Horizontal") != 0)
            snakeBody[0].transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal")));
        if (snakeBody.Count > 1)
        {
            for (int i = 1; i < snakeBody.Count; i++)
            {
                MarkerManager markM = snakeBody[i - 1].GetComponent<MarkerManager>();
                snakeBody[i].transform.position = markM.MarkerList[0].Position;

                snakeBody[i].transform.rotation = markM.MarkerList[0].Rotation;

                markM.MarkerList.RemoveAt(0);
            }
        }
    }

    void CreateBodyParts()
    {
        if (snakeBody.Count == 0)
        {
            GameObject temp1 = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
            temp1.AddComponent<SnakeHead>().Construct(this);
            if (!temp1.GetComponent<MarkerManager>())
                temp1.AddComponent<MarkerManager>();
            if (!temp1.GetComponent<Rigidbody2D>())
            {
                temp1.AddComponent<Rigidbody2D>();
                temp1.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            snakeBody.Add(temp1);
            bodyParts.RemoveAt(0);
        }

        MarkerManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkerManager>();
        if (countUp == 0)
        {
            markM.ClearList();
        }
        countUp += Time.deltaTime;

        if (countUp >= distanceBetween)
        {
            GameObject temp = Instantiate(bodyParts[0], markM.MarkerList[0].Position, markM.MarkerList[0].Rotation, transform);
            if (!temp.GetComponent<MarkerManager>())
                temp.AddComponent<MarkerManager>();

            if (!temp.GetComponent<Rigidbody2D>())
            {
                temp.AddComponent<Rigidbody2D>();
                temp.GetComponent<Rigidbody2D>().gravityScale = 0;
            }

            snakeBody.Add(temp);
            bodyParts.RemoveAt(0);
            temp.GetComponent<MarkerManager>().ClearList();
            countUp = 0;
        }
    }
}
