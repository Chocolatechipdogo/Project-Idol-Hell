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

    

    void Awake()
    {
        Summon();
    }

    void Summon()
    {
        angle = 360f / numberOfCollums;


        for (int i = 0; i < numberOfCollums; i++)
        {
            Material particleMaterial = material;

            var go = new GameObject("Particle System");
            go.transform.Rotate(angle * i, 90, 0); // Rotate so the system emits upwards.
            go.transform.parent = this.transform;
            go.transform.position = this.transform.position;
            pS = go.AddComponent<ParticleSystem>();
            go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;


            var mainModule = pS.main;

            mainModule.startColor = Color.green;
            mainModule.startSize = 0.5f;
            mainModule.startSpeed = bulletSpeed;

            var emission = pS.emission;
            emission.enabled = false;

            var form = pS.shape;
            form.enabled = true;
            form.shapeType = ParticleSystemShapeType.Sprite;
            form.sprite = null;

            var text = pS.textureSheetAnimation;
            text.enabled = true;
            text.mode = ParticleSystemAnimationMode.Sprites;
            text.AddSprite(texture);

            var collision = pS.collision;
            collision.enabled = true;
            collision.type = ParticleSystemCollisionType.World;
            collision.mode = ParticleSystemCollisionMode.Collision2D;
            collision.sendCollisionMessages = true;

        }

        // Every 2 secs we will emit.
        InvokeRepeating("DoEmit", 0f, fireRate);
    }

    void DoEmit()
    {

        foreach (Transform child in transform) 
        {
            pS = child.GetComponent<ParticleSystem>();
            // Any parameters we assign in emitParams will override the current system's when we call Emit.
            // Here we will override the start color and size
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = color;
            emitParams.startSize = bulletSize;
            emitParams.startLifetime = lifetime;
            pS.Emit(emitParams, 10);
        }
    }

    //void OnParticleCollision(GameObject other)
    //{
    //    Debug.Log("Hit");
    //}
}