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

        StartCoroutine(ChangeParticleDirectionSmoothly());
    }

    private void StopWind()
    {
        OnWindStop?.Invoke();
        StartCoroutine(ResetParticleDirectionSmoothly());
    }

    private IEnumerator ChangeParticleDirectionSmoothly()
    {
        var velocityModule = windParticles.velocityOverLifetime;
        var emissionModule = windParticles.emission;

        float startVelocity = velocityModule.x.constantMax;
        float targetVelocity = isWindBlowingRight ? -minMaxParticleVelocity.x : minMaxParticleVelocity.x;
        float startEmission = emissionModule.rateOverTime.constant;
        float targetEmission = emissionRate;

        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            float newVelocity = Mathf.Lerp(startVelocity, targetVelocity, elapsedTime / 1);
            velocityModule.x = new ParticleSystem.MinMaxCurve(newVelocity, newVelocity);

            float newEmission = Mathf.Lerp(startEmission, targetEmission, elapsedTime / 1);
            emissionModule.rateOverTime = newEmission;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        velocityModule.x = new ParticleSystem.MinMaxCurve(targetVelocity, targetVelocity);
        emissionModule.rateOverTime = targetEmission;
    }

    private IEnumerator ResetParticleDirectionSmoothly()
    {
        var velocityModule = windParticles.velocityOverLifetime;
        var emissionModule = windParticles.emission;

        float startVelocity = velocityModule.x.constantMax;
        float targetVelocity = 0f;
        float startEmission = emissionModule.rateOverTime.constant;
        float targetEmission = defaultEmission;

        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            float newVelocity = Mathf.Lerp(startVelocity, targetVelocity, elapsedTime / 1);
            velocityModule.x = new ParticleSystem.MinMaxCurve(newVelocity, newVelocity);

            float newEmission = Mathf.Lerp(startEmission, targetEmission, elapsedTime / 1);
            emissionModule.rateOverTime = newEmission;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        velocityModule.x = new ParticleSystem.MinMaxCurve(targetVelocity, targetVelocity);
        emissionModule.rateOverTime = targetEmission;
    }
}