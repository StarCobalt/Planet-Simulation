using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerSimulation : MonoBehaviour
{

    //Script
    public planetManager PlanetManager;
    //Simulation
    public int _simulationLength = 400;

    //Planets
    public Vector3[,] planetPositionData;
    public List<planetManager.PlanetaryOrgan> planets_In_Simulation;
    int Number_Planets__Simulation;

    //Simulation Approvitations
    bool ManagerDataRecieved = false;
    public bool simulationYes = false;
    
    void Start()
    {
        _simulationLength = 1400;
    }

   
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)){
            simulationYes = !simulationYes;
            
        }
        if (ManagerDataRecieved && simulationYes){
            StartCoroutine(nameof(movePlanets));
            simulationYes = false;
            
        }
    }
    IEnumerator movePlanets(){
        
        simulationYes = false;
            for (int simulationTimeStep = 0; simulationTimeStep < _simulationLength ; simulationTimeStep++){
               for (int planet = 0; planet < Number_Planets__Simulation; planet++){
                  planets_In_Simulation[planet].mainOrgan.transform.position = planetPositionData[planet,simulationTimeStep];
               }
               //print(simulationTimeStep);
               yield return new WaitForSeconds(0.04f);
            }
            
        
    }
    //Simulation Manager

    public int SetSimulationLength(){
        return _simulationLength;
    }
   
    public void GetPlanetData(List<planetManager.PlanetaryOrgan> Organs , int Number_Planets_Simulation, Vector3[,] positionData){
        planets_In_Simulation = Organs;
        ManagerDataRecieved = true;
        Number_Planets__Simulation = Number_Planets_Simulation;
        planetPositionData = positionData;
    }
}
