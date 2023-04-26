using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float minAcceleration;
        [SerializeField] private float percentAcceleration;
        [SerializeField] private float maxRotationError;
        [SerializeField] private float speedAfterCollision;
        [SerializeField] private float collisionDeltaDirection;
        private float newDirection = 0;
        private float currentDirection = 0;
        private float maxSpeed;
        private float rotationSpeed;
        private float deltaSpeed;
        private float directionDelta;

        void Update()
        {
            if (Mathf.Abs(newDirection - currentDirection) > maxRotationError)
            {
                TruckRotation();
            }
            MoveTruck();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag(ConstantVariables.ObstacleTag))
            {
                moveSpeed = speedAfterCollision;


                // takedamage


                directionDelta = 0 - transform.eulerAngles.z;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.CompareTag(ConstantVariables.ObstacleTag))
            {
                //currentDirection = 0 - transform.eulerAngles.z;
                moveSpeed = speedAfterCollision;
                if (directionDelta > (0 - transform.eulerAngles.z))
                {
                    newDirection = 0 - collisionDeltaDirection - transform.eulerAngles.z;
                }
                else if (directionDelta < (0 - transform.eulerAngles.z))
                {
                    newDirection = collisionDeltaDirection - transform.eulerAngles.z;
                }
                directionDelta = 0 - transform.eulerAngles.z;
            }
        }

        public void ChangeDirection(float newDirection)
        {
            this.newDirection = newDirection;
        }

        public void ChangeSpeedAndRotationSpeed(float newSpeed, float newRotationSpeed)
        {
            maxSpeed = newSpeed;
            rotationSpeed = newRotationSpeed;
        }

        private void MoveTruck()
        {
            if (moveSpeed < maxSpeed)
            {
                deltaSpeed = maxSpeed - moveSpeed;
                moveSpeed += (deltaSpeed * percentAcceleration + minAcceleration) * Time.deltaTime;
            }
            else if (moveSpeed > maxSpeed * 1.02)
            {
                moveSpeed -= (maxSpeed * percentAcceleration + minAcceleration) * Time.deltaTime;
            }
            transform.Translate(moveSpeed * Time.deltaTime * Vector2.up);
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
    }
}
