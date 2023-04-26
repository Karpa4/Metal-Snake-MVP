using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public float DistanceBetween;
    public float Speed;
    public float TurnSpeed;
    public List<MarkerManager> markers;
    public GameObject prefab;
    public Rigidbody2D FirstRigid;
    private float countUp = 0;

    private void Start()
    {
        countUp = DistanceBetween;
        CreateBodyParts();
    }

    private void FixedUpdate()
    {
        SnakeMove();
    }

    public void SnakeMove()
    {
        FirstRigid.velocity = markers[0].transform.up * Speed * Time.fixedDeltaTime;
        if (Input.GetAxis("Horizontal") != 0)
        {
            markers[0].transform.Rotate(0, 0, -TurnSpeed * Time.fixedDeltaTime * Input.GetAxis("Horizontal"));
        }
    }

    public void CreateBodyParts()
    {
        MarkerManager mark = markers[markers.Count - 1];
        if (countUp == 0)
        {
            mark.ClearList();
        }
        countUp += Time.fixedDeltaTime;
        if (countUp >= DistanceBetween)
        {
            GameObject temp = Instantiate(prefab, mark.MarkerList[0].Position, mark.MarkerList[0].Rotation, transform);
        }
    }
}
