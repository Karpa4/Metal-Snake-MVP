using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public List<Marker> MarkerList = new List<Marker>();

    private void FixedUpdate()
    {
        UpdateList();
    }

    public void UpdateList()
    {
        MarkerList.Add(new Marker(transform.position, transform.rotation));
    }

    public void ClearList()
    {
        MarkerList.Clear();
        MarkerList.Add(new Marker(transform.position, transform.rotation));
    }
}
