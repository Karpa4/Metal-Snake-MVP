using Services.Pause;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxShootRadius;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotationError;
    [SerializeField] private float modifier;
    [SerializeField] private PauseManager pauseManager;
    private Vector2 aimDirection;
    private float angleToTarget;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            RefContainer.Instance.MainPauseManager.PauseOn();

        if (Input.GetKeyDown(KeyCode.D))
            RefContainer.Instance.MainPauseManager.PauseOff();

        if (target != null)
        {
            aimDirection = target.position - transform.position;
            if (aimDirection.magnitude < maxShootRadius)
            {
                angleToTarget = Vector2.Angle(transform.up, aimDirection);
                if (angleToTarget < maxRotationError)
                {
                    Debug.Log("RotateToTarget");

                    // shoot


                }
                else
                {
                    RotateWeaponToTarget();
                }
            }
        }
    }

    private void RotateWeaponToTarget()
    {
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - modifier;
        Quaternion aimRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, aimRotation, Time.deltaTime * rotationSpeed);
    }

    public void SetNewTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
