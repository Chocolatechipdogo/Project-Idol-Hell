using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScriptFirstEnemy : MonoBehaviour
{

    
    
    
    
    
    
    public ParticleSystem pS;

    public GameObject particleHolder;


    // Start is called before the first frame update
    void Start()
    {

        Material particleMaterial = new Material(Shader.Find("Particles/Standard Unlit"));

        particleHolder.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
        particleHolder.transform.Rotate(-90, 0, 0);
        var mainModule = pS.main;

        mainModule.startColor = Color.green;
        mainModule.startSize = 0.5f;

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
        pS.Emit(emitParams, 10);
        pS.Play(); // Continue normal emissions
    }
}