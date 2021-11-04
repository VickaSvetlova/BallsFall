using System;
using UnityEngine;

public class ParticleFx : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Init(Color color)
    {
        var main = _particleSystem.main;
        main.startColor = color;
        Destroy(gameObject, main.startLifetimeMultiplier);
    }
}