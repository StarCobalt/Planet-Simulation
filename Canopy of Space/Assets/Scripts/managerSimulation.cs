using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerSimulation : MonoBehaviour
{

    //Scripts
    public planetManager PlanetManager;
    public planetPhysics PlanetPhysics;
    //Simulation
    public int _simulationLength = 100;

    //Planets
    public Vector3[,] planetPositionData;
    public List<planetManager.PlanetaryOrgan> planets_In_Simulation;
    int Number_Planets__Simulation;

    //Simulation Approvitations
    bool PhysicsDataRecieved = false;
    bool ManagerDataRecieved = false;
    public bool simulationYes = false;
    void Start()
    {
        _simulationLength = 100;
        
    }

   
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)){
            simulationYes = !simulationYes;
            
        }
        StartCoroutine("movePlanets");
        
    }
    IEnumerator movePlanets(){
        if (PhysicsDataRecieved && ManagerDataRecieved && simulationYes){
            for (int simulationTimeStep = 0; simulationTimeStep < _simulationLength; simulationTimeStep++){
               for (int planet = 0; planet < Number_Planets__Simulation; planet++){
                  planets_In_Simulation[planet].mainOrgan.transform.position = planetPositionData[planet,simulationTimeStep];
               }
               print(simulationTimeStep);
               yield return null;
            }
        }   
        
    }
    //Simulation Manager

    public int SetSimulationLength(){
        return _simulationLength;
    }
    public void GetPositionData(Vector3[,] PlanetPositionData){
        planetPositionData = PlanetPositionData;
        PhysicsDataRecieved = true;

    }
    public void GetPlanetData(List<planetManager.PlanetaryOrgan> Organs , int Number_Planets_Simulation){
        planets_In_Simulation = Organs;
        ManagerDataRecieved = true;
        Number_Planets__Simulation = Number_Planets_Simulation;
    }
}
