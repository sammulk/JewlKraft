using System.Collections.Generic;
using UnityEngine;

public class WorkstationInteraction : MonoBehaviour
{
    private List<Workstation> stationsInRange = new List<Workstation>();
    private Workstation nearbyWorkstation;

    void Update()
    {
        if (stationsInRange.Count == 0)
        {
            if (nearbyWorkstation != null)
                nearbyWorkstation.SetHighlight(false);
            nearbyWorkstation = null;
            return;
        }

        // Leia kõige lähedasem
        Workstation closest = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var station in stationsInRange)
        {
            float distance = Vector2.Distance(transform.position, station.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                closest = station;
            }
        }

        if (closest != nearbyWorkstation)
        {
            if (nearbyWorkstation != null)
                nearbyWorkstation.SetHighlight(false);

            nearbyWorkstation = closest;
            nearbyWorkstation.SetHighlight(true);
        }

        // Interaktsioon
        if (Input.GetKeyDown(KeyCode.E) && nearbyWorkstation != null)
            nearbyWorkstation.Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Workstation station = other.GetComponent<Workstation>();
        if (station != null && !stationsInRange.Contains(station))
            stationsInRange.Add(station);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Workstation station = other.GetComponent<Workstation>();
        if (station != null)
            stationsInRange.Remove(station);
    }
}


