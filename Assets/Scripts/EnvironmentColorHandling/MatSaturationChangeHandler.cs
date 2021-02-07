using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatSaturationChangeHandler : SaturationHandler
{
    [SerializeField] private GameObject firework = null;
    [SerializeField] private Material[] mats = null;
    private HSV[] originalMatsHSV = null;
    private int currentMat = 0;

    private bool colored = true;

    
    private AudioSource fireworkAudioSource = null;
    private ParticleSystem fireworkParticleSystem = null;
    
    // Start is called before the first frame update
    void Start()
    {
        originalMatsHSV = new HSV [mats.Length];
        RemoveSaturation();
        fireworkAudioSource = firework.GetComponent<AudioSource>();
        fireworkParticleSystem = firework.GetComponent<ParticleSystem>();

        currentMat = 0;
    }

    void RemoveSaturation()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            float H, S, V; // H S V color values
            Color.RGBToHSV(mats[i].color, out H, out S, out V); // convert RGB to HSV
            originalMatsHSV[i] = new HSV(H, S, V);
            
            S = 0;
            mats[i].color = Color.HSVToRGB(H, S, V);
        }
    }

    public override void IncreaseSaturation()
    {
        for (int i = 0; i < 3; i++)
        {
            SoMuchSat();
        }
    }

    void SoMuchSat()
    {
        if (currentMat == mats.Length - 1)
        {
            enemySpawner.spawnLastEnemy = true;
        }

        if (currentMat >= mats.Length)
        {
            return;
        }
        fireworkAudioSource.Play();
        fireworkParticleSystem.Play();
        
        float h = originalMatsHSV[currentMat].H;
        float s = originalMatsHSV[currentMat].S;
        float v = originalMatsHSV[currentMat].V;
        
        mats[currentMat].color = Color.HSVToRGB(h, s, v);
        
        currentMat++;

    } 
        
    void OnDestroy()
    {
        if (colored)
        {
            colored = false;
            currentMat = 0;
            for (int i = 0; i < mats.Length; i++)
            {
                SoMuchSat();
            }

        }
    }

    private void OnApplicationQuit()
    {
        if (colored)
        {
            colored = false;
            currentMat = 0;
            for (int i = 0; i < mats.Length; i++)
            {
                SoMuchSat();
            }
        }
    }
}

public class HSV 
{
    public readonly float H;
    public readonly float S;
    public readonly float V;

    public HSV(float h, float s, float v)
    {
        this.H = h;
        this.S = s;
        this.V = v;
    }
}
