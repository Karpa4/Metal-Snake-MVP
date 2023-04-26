using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.Player
{
    public class TrailersController : MonoBehaviour
    {
        [SerializeField] private float connectorPointOffset;
        [SerializeField] private List<Transform> trailers;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float fullSpeed;
        [SerializeField] private float maxRotSpeed;
        [SerializeField] private Health health;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Transform connectedTrailers;
        [SerializeField] private TextMeshProUGUI trailersNumber;
        private float currentMaxSpeed;
        private Vector2 connectorPoint;
        private float previousRotation = 0;
        private float deltaRotation = 0;
        private float currentRotation = 0;



        private List<TestCollision> tests = new List<TestCollision>();



        private void Awake()
        {
            TrailersChange(trailers.Count);
            //health.Death += DestroyLastTrailer;
            playerMovement.ChangeSpeedAndRotationSpeed(currentMaxSpeed, rotationSpeed);


            foreach (var t in trailers)
            {
                tests.Add(t.GetComponent<TestCollision>());
            }
        }

        private void Update()
        {
            previousRotation = transform.eulerAngles.z;
            connectorPoint = transform.position - (transform.up * 0.95f);
            MoveTrailersNew();
        }

        public void AddNewTrailer(Transform newTrailer)
        {
            newTrailer.SetParent(connectedTrailers);
            trailers.Add(newTrailer);
            int newTrailersCount = trailers.Count + 1;
            TrailersChange(newTrailersCount);
        }

        private void MoveTrailers()
        {
            foreach (var Cargo in trailers)
            {
                Cargo.position = connectorPoint;
                currentRotation = Cargo.eulerAngles.z;
                deltaRotation = previousRotation - currentRotation;
                if (Mathf.Abs(deltaRotation) > 1)
                {
                    if (deltaRotation > 180)
                    {
                        currentRotation += 360;
                        deltaRotation = previousRotation - currentRotation;
                    }
                    else if (deltaRotation < -180)
                    {
                        currentRotation -= 360;
                        deltaRotation = previousRotation - currentRotation;
                    }
                    if (previousRotation > currentRotation)
                    {
                        currentRotation += rotationSpeed * Time.deltaTime * (deltaRotation * 0.04f + 0.2f);
                    }
                    else
                    {
                        currentRotation += rotationSpeed * Time.deltaTime * (deltaRotation * 0.04f - 0.2f);
                    }
                }
                Cargo.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
                previousRotation = currentRotation;
                connectorPoint = Cargo.position - (Cargo.up * connectorPointOffset);
            }
        }



        private void MoveTrailersNew()
        {
            int index = 0;
            foreach (var Cargo in trailers)
            {
                Cargo.position = connectorPoint;
                currentRotation = Cargo.eulerAngles.z;
                deltaRotation = previousRotation - currentRotation;

                if (tests[index].OnCollision)
                {

                }
                else if (Mathf.Abs(deltaRotation) > 1)
                {
                    if (deltaRotation > 180)
                    {
                        currentRotation += 360;
                        deltaRotation = previousRotation - currentRotation;
                    }
                    else if (deltaRotation < -180)
                    {
                        currentRotation -= 360;
                        deltaRotation = previousRotation - currentRotation;
                    }
                    if (previousRotation > currentRotation)
                    {
                        currentRotation += rotationSpeed * Time.deltaTime * (deltaRotation * 0.04f + 0.2f);
                    }
                    else
                    {
                        currentRotation += rotationSpeed * Time.deltaTime * (deltaRotation * 0.04f - 0.2f);
                    }
                }
                Cargo.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
                previousRotation = currentRotation;
                connectorPoint = Cargo.position - (Cargo.up * connectorPointOffset);
                index++;
            }
        }





        private void DestroyLastTrailer()
        {
            int newTrailersCount = trailers.Count - 1;
            Transform lastTrailer = trailers[newTrailersCount];
            trailers.RemoveAt(newTrailersCount);
            Destroy(lastTrailer);
            TrailersChange(newTrailersCount);
        }

        private void TrailersChange(int newTrailersCount)
        {
            if (newTrailersCount > 0)
            {
                trailersNumber.color = Color.white;
            }
            else
            {
                trailersNumber.color = Color.red;
            }
            trailersNumber.text = newTrailersCount.ToString();

            if (newTrailersCount > 2)
            {
                currentMaxSpeed = fullSpeed * Mathf.Pow(0.85f, newTrailersCount - 2);
                rotationSpeed = maxRotSpeed * Mathf.Pow(0.85f, newTrailersCount - 2);
            }
            else
            {
                currentMaxSpeed = fullSpeed;
                rotationSpeed = maxRotSpeed;
            }
            playerMovement.ChangeSpeedAndRotationSpeed(currentMaxSpeed, rotationSpeed);
        }
    }
}
