using UnityEngine;

public class VehicleVisibility : MonoBehaviour
{
    private GameObject[] vehicles;

    private void Awake()
    {
        vehicles = GameObject.FindGameObjectsWithTag("Carriage");
    }

    private void OnEnable()
    {
        HideVehicles();
    }

    private void OnDisable()
    {        
        ShowVehicles();
    }

    private void HideVehicles()
    {
        foreach (var vehicle in vehicles)
        {
            if (vehicle != null)
            {
                vehicle.SetActive(false);
            }
        }
    }

    private void ShowVehicles()
    {
        foreach (var vehicle in vehicles)
        {
            if (vehicle != null)
            {
                vehicle.SetActive(true);
            }
        }
    }
}
