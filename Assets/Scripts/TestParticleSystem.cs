using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticleSystem : MonoBehaviour
{

    public int numberOfCollums;
    public float bulletSpeed;
    public Sprite texture;
    public Color color;
    public float lifetime;
    public float fireRate;
    public float bulletSize;
    private float angle;
    public Material material;


    public ParticleSystem system;

    void awake() 
    {
        Summon();
    }

    void Summon()
    {
        angle = 360f / numberOfCollums;

        for (int i = 0; i < numberOfCollums; i++)
        {

            // A simple particle material with no texture.
            Material particleMaterial = new Material(Shader.Find("Particles/Standard Unlit"));

            // Create a green Particle System.
            var go = new GameObject("Particle System");
            go.transform.Rotate(angle * 1, 90, 0); // Rotate so the system emits upwards.
            go.transform.parent = this.transform;
            go.transform.position = this.transform.position;
            system = go.AddComponent<ParticleSystem>();
            go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            var mainModule = system.main;
            mainModule.startColor = Color.green;
            mainModule.startSize = 0.5f;

            var emission = system.emission;
            emission.enabled = false;

            var form = system.shape;
            form.enabled = true;
        }

        // Every 2 secs we will emit.
        InvokeRepeating("DoEmit", 2.0f, 2.0f);
    }

    void DoEmit()
    {
        // Any parameters we assign in emitParams will override the current system's when we call Emit.
        // Here we will override the start color and size.
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.startColor = Color.red;
        emitParams.startSize = 0.2f;
        system.Emit(emitParams, 10);
        system.Play(); // Continue normal emissions
    }
}
