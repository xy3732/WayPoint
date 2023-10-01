using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirokoDrone : MonoBehaviour
{
    public float damage { get; set; }

    [field: SerializeField] private GameObject droneObject { get; set; }
    [field: SerializeField] private GameObject shadowObject { get; set; }
    private Vector3 shadowAnchor = new Vector3(0,-0.8f,0);

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        
    }
}

