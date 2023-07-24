using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScriptFirstEnemy : MonoBehaviour
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
    
    
    
    
    public ParticleSystem pS;

    public GameObject particleHolder;


    void Awake()
    {
        Summon();
    }

    void Summon()
    {
        angle = 360f / numberOfCollums;


        for (int i = 0; i < numberOfCollums; i++)
        {
            Material particleMaterial = new Material(Shader.Find("Particles/Standard Unlit"));

            particleHolder.transform.Rotate(angle * i, 90, 0);
            pS = particleHolder.GetComponent<ParticleSystem>();
            particleHolder.GetComponent<ParticleSystemRenderer>().material = particleMaterial;

            particleHolder.transform.parent = this.transform;
            particleHolder.transform.position = this.transform.position;
            var mainModule = pS.main;

            mainModule.startColor = Color.green;
            mainModule.startSize = 0.5f;

            var emission = pS.emission;
            emission.enabled = false;

            var forma = pS.shape;
            forma.enabled = true;
            forma.shapeType = ParticleSystemShapeType.Sprite;
            forma.sprite = null;

        }

        // Every 2 secs we will emit.
        InvokeRepeating("DoEmit", 2.0f, 2.0f);
    }

    void DoEmit()
    {

        foreach (Transform child in transform) 
        {
            pS = child.GetComponent<ParticleSystem>();
            // Any parameters we assign in emitParams will override the current system's when we call Emit.
            // Here we will override the start color and size
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = Color.red;
            emitParams.startSize = 0.2f;
            pS.Emit(emitParams, 10);
        }
    }
}