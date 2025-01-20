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

    public ParticleSystem windParticles;
    [SerializeField] private Vector2 minMaxParticleVelocity = new Vector2(5f, 0f);
    [SerializeField] private float emissionRate = 10f;
    [SerializeField] private float defaultEmission;

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

        ChangeParticleDirection();
    }

    private void StopWind()
    {
        OnWindStop?.Invoke();
        ResetParticleDirection();
    }

    private void ChangeParticleDirection()
    {
        var velocityModule = windParticles.velocityOverLifetime;

        if (isWindBlowingRight)
        {
            velocityModule.x = new ParticleSystem.MinMaxCurve(minMaxParticleVelocity.x, minMaxParticleVelocity.x);
        }
        else
        {
            velocityModule.x = new ParticleSystem.MinMaxCurve(-minMaxParticleVelocity.x, -minMaxParticleVelocity.x);
        }

        var emissionModule = windParticles.emission;
        emissionModule.rateOverTime = emissionRate;
    }

    private void ResetParticleDirection()
    {
        var velocityModule = windParticles.velocityOverLifetime;
        velocityModule.x = new ParticleSystem.MinMaxCurve(0f, 0f);

        var emissionModule = windParticles.emission;
        emissionModule.rateOverTime = defaultEmission;
    }
}