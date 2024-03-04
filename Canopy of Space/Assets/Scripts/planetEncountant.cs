using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetEncountant : MonoBehaviour
{

    
    public planetManager PlanetManager; 
    public planetPhysics PlanetPhysics;
    // Start is called before the first frame update
    void Start()
    {
      // PlanetPhysics.OnPlanetInbound += PlanetEncounter;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlanetEncounter(planetManager.secondaryTransform a, planetManager.secondaryTransform b,GameObject ap){
        
        Destroy(ap);
        print("Destroyed");
    }
}
