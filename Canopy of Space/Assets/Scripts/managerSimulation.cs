using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerSimulation : MonoBehaviour
{

    //Scripts
    public planetManager PlanetManager;
    public planetPhysics PlanetPhysics;
    //Simulation
    int simulationLength = 595;

    //Planets
    public Vector3[,] planetPositionData;
    public List<planetManager.PlanetaryOrgan> planets_In_Simulation;
    int Number_Planets__Simulation;

    //Simulation Approvitations
    bool PhysicsDataRecieved = false;
    bool ManagerDataRecieved = false;
    bool simulationYes = true;
    void Start()
    {
        simulationLength = 595;
        
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
            for (int simulationTimeStep = 0; simulationTimeStep < simulationLength; simulationTimeStep++){
                
                
                
                
                for (int planet = 0; planet < Number_Planets__Simulation; planet++){
                    try{
                    planets_In_Simulation[planet].mainOrgan.transform.position = planetPositionData[planet,simulationTimeStep];
                    }
                    catch(System.Exception e){
                        print(e);
                        print($"Final Final Index Calc - planet{planet},{planetPositionData.GetLength(0)},{Number_Planets__Simulation}\n simuationTimeStep {simulationTimeStep},{planetPositionData.GetLength(1)},{simulationLength} ");
                    }
                
                
                }
            }
        }   
        yield return new WaitForSeconds(1.21f);
    }
    //Simulation Manager

    public void SetSimulationLength(int SimulationLength){
        SimulationLength = simulationLength;
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
