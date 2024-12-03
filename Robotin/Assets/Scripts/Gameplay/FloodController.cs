using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodController : MonoBehaviour
{


    [SerializeField] private float speedPipe = 1.0f;
    void Update()
    {
        transform.position += Vector3.up * speedPipe * Time.deltaTime;



    }
}
