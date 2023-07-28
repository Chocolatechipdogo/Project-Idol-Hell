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

    public bool heartShape;

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

            var currentAngle = angle * i;
            

            if (heartShape) 
            {
                // bottom center of hear should be the longest one out
                if (currentAngle == 90)   //  || i == 8 || i == 10
                {
                    mainModule.startSpeed = bulletSpeed + 1.25f;
                }
                //selecting the secounf quadrant
                else if (currentAngle > 180 && currentAngle < 270)
                {
                    // if its above 30 degree or less then the 60 degree eqivilant it will smooth to 2.5
                    if (currentAngle > 210 && currentAngle < 240)
                    {
                        mainModule.startSpeed = bulletSpeed + 2.5f;
                    }
                    // if there are lower angles that exist or higher angles make them transition smoothly from the transition 2 to the 1
                    else if (currentAngle < 210 || currentAngle > 240)
                    {
                        mainModule.startSpeed = bulletSpeed + 1.25f;
                    }
                    // any angle that is out side those will default to this aka fail safe (no issue)
                    else
                    {
                        mainModule.startSpeed = bulletSpeed + 10;
                    }
                }
                //the top center part of the heart that doesn't need to go that fast so make sure it go default speed
                else if (currentAngle == 270)
                {
                    mainModule.startSpeed = bulletSpeed;
                }
                // selecting first quadrant
                else if (currentAngle > 270 && currentAngle < 360)
                {
                    // if its above 30 degree or less then the 60 degree eqivilant it will smooth to 2.5 
                    if (currentAngle > 300  && currentAngle < 330)
                    {
                        mainModule.startSpeed = bulletSpeed + 2.5f;
                    }
                    //if there are lower angles that exist or higher angles make them transition smoothly from the transition 2 to the 1
                    else if (currentAngle < 300 || currentAngle > 330)
                    {
                        mainModule.startSpeed = bulletSpeed + 1.25f;
                    }
                    // any angle that is out side those will default to this aka fail safe
                    else
                    {
                        mainModule.startSpeed = bulletSpeed + 2;
                    }
                }
                // if there the exact middle area of the heart expand a bit to give the final curve before doing a rounded triangle
                else if (currentAngle == 0 || currentAngle == 180)
                {
                    mainModule.startSpeed = bulletSpeed + 1.5f;
                }
                //all the angles in quadrant 4 
                else if (currentAngle < 90)
                {
                  mainModule.startSpeed = bulletSpeed + 0.9f;
                }
                // all the angles in quadrant 3
                else if (currentAngle > 90 && currentAngle < 180) 
                {
                  mainModule.startSpeed = bulletSpeed + 0.9f;
                } 
               //else if (currentAngle > 90 && currentAngle < 150)
               //{
               //  mainModule.startSpeed = bulletSpeed + 0.9f;
               //}
               //any angles that don't get a value get default
                else
                {
                    mainModule.startSpeed = bulletSpeed + 10;
                }
            }
            else
            {
                mainModule.startSpeed = bulletSpeed;
            }


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