using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirokoDroneInRange : MonoBehaviour
{
    [field: SerializeField] public ShirokoDrone drone { get; set; }
    [field : SerializeField] private List<GameObject> enemys;

    [field:SerializeField] public bool isFireReady { get; set; }
    private float shortDistance = 999f;

    private void OnEnable()
    {
        isFireReady = false;
        enemys.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("enemy"))
        {
            enemys.Add(other.gameObject);

            if(!isFireReady) drone.setTarget(shortDistanceObject());
        }
    }

    public GameObject shortDistanceObject()
    {
        isFireReady = true;

        GameObject targetObject = null;

        foreach (var item in enemys)
        {
            float distance = Vector2.Distance(transform.position, item.transform.position);

            if(shortDistance > distance)
            {
                shortDistance = distance;
                targetObject = item.gameObject;
            }
        }
        return targetObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    { 
        if(other.CompareTag("enemy"))
        {
            int index = enemys.FindIndex(item => item.gameObject == other.gameObject);
            enemys.RemoveAt(index);

            if(!isFireReady) drone.setTarget(shortDistanceObject());
        }
    }
}
