using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindSystem : MonoBehaviour
{
    [SerializeField] private float windForceMagnitude = 75f;
    [SerializeField] private float windDuration = 10f;      
    [SerializeField] private float windInterval = 4f;     

    private Vector2 windForce; 
    private bool isWindBlowingRight = true; 

    public event Action<Vector2> OnWindStart;
    public event Action OnWindStop;

    private void Start()
    {
        StartCoroutine(WindRoutine());
    }

    private IEnumerator WindRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(windInterval);

            StartWind();

            yield return new WaitForSeconds(windDuration);

            StopWind();
        }
    }

    private void StartWind()
    {
        windForce = isWindBlowingRight ? Vector2.right * windForceMagnitude : Vector2.left * windForceMagnitude;

        isWindBlowingRight = !isWindBlowingRight;

        OnWindStart?.Invoke(windForce);

        Debug.Log($"Viento iniciado: Dirección {(windForce.x > 0 ? "Derecha" : "Izquierda")}");
    }

    private void StopWind()
    {
        OnWindStop?.Invoke();

        Debug.Log("Viento detenido.");
    }
}
