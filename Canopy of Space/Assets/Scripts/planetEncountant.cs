using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;







//To-Do List
//Stop Simulation at any time P In Progress in Progress
//Collision Destruction (in progress)
//Stars
//Different Types of Planets/Stars/Comets/Asteroids/Meteors in progress
//Low Density Planets identified as gas giants/planets in progress
//Classification Identification for all Celestial Bodies in progress in Progress
//Planets Rotating
//Atmospheres
//Pausing
//Ligthing
//

//Game Idea
//Camera ViewPoint from All Angles facing the sun or other planets from the perspective of a planet
//Your an astronaught/ astronomer trying to map the solar system
//CARTOGRAPHY


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
