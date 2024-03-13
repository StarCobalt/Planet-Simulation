
using UnityEngine;
using System.Collections.Generic;
using System;


public class planetPhysics : MonoBehaviour{
    
    //Variable/Fields

    int simulationLength; 
    public planetManager PlanetManager; 
    public managerSimulation ManagerSimulation;
     int Number_Planets_In_Simulation;
    public List<Transform> Transforms_In_Simulation;
    public List<planetManager.secondaryTransform> secondaryTransforms_In_Simulation;
    public float SunMass;
    public List<planetManager.PlanetaryOrgan> Planets_In_Simulation;
    public Vector3[,] planetPositionData;
    //Delegates/Events
    
    //public event Action<planetManager.secondaryTransform, planetManager.secondaryTransform, GameObject> OnPlanetInbound;
    
    








  //Functions
    float calculateGravity(float amass, float bmass, Vector3 aposition, Vector3 bposition, Vector3 differencebetweenAB, Vector3 ascale, Vector3 bscale){
       
       //Operatives
        float distanceBetweenCenters = differencebetweenAB.magnitude; //find distance
        float diameterBetweenCenters = (ascale.x + bscale.x)/2; //add radii
        float gravitationForce = -9000;

        if (distanceBetweenCenters > diameterBetweenCenters){
          //Gravity
          float distanceBetweenCentersSqr = differencebetweenAB.sqrMagnitude;
          float CollectiveMass = amass * bmass;
           gravitationForce = (PlanetManager.gravitationalConstant * CollectiveMass) / distanceBetweenCentersSqr;
          //Debug
          //print($"gravitationForce : \n{gravitationForce}");
        }
        

        
        return gravitationForce; 
    }

    public void Ask_For_SimulationData(List<Transform> Transforms_InSimulation, List<planetManager.PlanetaryOrgan> Planets_InSimulation,List<planetManager.secondaryTransform> secondaryTransforms_InSimulation,float sunMass, Vector3[,] planetposdata){
                
        Transforms_In_Simulation = Transforms_InSimulation;
	      secondaryTransforms_In_Simulation = secondaryTransforms_InSimulation;
        Planets_In_Simulation = Planets_InSimulation;
        SunMass = sunMass;
        
        Number_Planets_In_Simulation = Transforms_In_Simulation.Count;	   
        simulationLength = planetposdata.GetLength(1);
        planetPositionData = planetposdata;
    }
    void SetplanetPositionData(Vector3 position, int planet, int simulationTimeStep){
      planetPositionData[planet, simulationTimeStep] = position;
    }
    


  //Physics
  void Start(){
    
     for(int simulationTimeStep = 1; simulationTimeStep < simulationLength; simulationTimeStep++){
    
      
            for(int i = 0; i < Number_Planets_In_Simulation;i++){
           
              //Variables
           
              planetManager.secondaryTransform a = secondaryTransforms_In_Simulation[i];
              Transform ap = Transforms_In_Simulation[i];
              
              Vector3 CurrentVelocity = new(0,0,0);

              Vector3 A_prevPosition = planetPositionData[i,simulationTimeStep - 1];
              
              
              //Attraction Between All Bodies
            
              for (int j = 0; j < Number_Planets_In_Simulation;j++){
                
                  //Anti-Self-Attraction Condition
                  if (i == j){
                    //Sun Gravity
                    //print("Gravitating");
                    CurrentVelocity += -A_prevPosition.normalized * PlanetManager.gravitationalConstant * (SunMass * a.mass/A_prevPosition.sqrMagnitude);
                    continue;
                  }


                  //Variables/Fields
                
                  planetManager.secondaryTransform b = secondaryTransforms_In_Simulation[j];
                  Transform bp = Transforms_In_Simulation[j];
                 
                  Vector3 B_prevPosition = planetPositionData[j,simulationTimeStep - 1];
                  Vector3 DifferenceBetweenAB = B_prevPosition - A_prevPosition;
               
                

                float gravitationForce = calculateGravity(a.mass, b.mass, A_prevPosition, B_prevPosition, DifferenceBetweenAB,ap.localScale, bp.localScale);
                
                if (gravitationForce == -9000){//Something is In the Way
                    //Destroy(Planets_In_Simulation[i].mainOrgan);
                    //secondaryTransforms_In_Simulation.RemoveAt(i);
                    //Transforms_In_Simulation.RemoveAt(i);
                }else{ 
                    Vector3 direction = DifferenceBetweenAB.normalized;
                    Vector3 PlanetForce = direction * gravitationForce;
                    CurrentVelocity += PlanetForce;
                   
                }
              }
              
              //Motion
            
            
              Vector3 FinalAcceleration =  (CurrentVelocity + a.velocity) * PlanetManager.gravitationalConstant;
              secondaryTransforms_In_Simulation[i] = new planetManager.secondaryTransform(a.mass,a.velocity + CurrentVelocity,a.density,FinalAcceleration);
            
              //Round to The Nearest Decimal Point
              FinalAcceleration = new Vector3(Mathf.Round(FinalAcceleration.x*1000)/1000,Mathf.Round(FinalAcceleration.y*1000)/1000,Mathf.Round(FinalAcceleration.z*1000)/1000);
            
              
              
              
              
              if (true){
                SetplanetPositionData(A_prevPosition + FinalAcceleration, i, simulationTimeStep);
              }
          }
        }
        ManagerSimulation.GetPositionData(planetPositionData);
    }
}




 //Debug
           // Debug.DrawLine(Transforms_In_Simulation[i].transform.position,Transforms_In_Simulation[i].transform.position + motiveForce,Color.cyan);
            //($"CollectiveForce : \n{collectiveForce}");






//{

    //Prefabs
   //public GameObject testPrefab;


   //Variables
  // Renderer rend;

    //Materials
   // public Material[] materials =  new Material[6];
    
    
    
    //Class
   // public class PlanetaryOrgan{
      

      //Variables
      
     // public GameObject mainOrgan;
     // public secondaryTransform Object_secondary_Transform;
//      public Transform Object_Transform;
      
       // public Vector3 position;
       // public float scale;
        //public float density;
        //public Vector3 startingVelocity;
       // public float worldSize = 800;
       // public float scaleMinMax = 24100;
        //Constructor
       // public PlanetaryOrgan(GameObject prefab, string Name, Material material, Renderer rend){

            
           // position = new Vector3(Random.Range(-worldSize,worldSize),Random.Range(-worldSize,worldSize),Random.Range(-worldSize,worldSize));
           // startingVelocity = new(0,0,0);
           // scale = Random.Range(0,scaleMinMax);
           // density = Random.Range(0,2f);
            

            //Making of a New Planet
           // Object_secondary_Transform = new secondaryTransform(scale * density,startingVelocity,density);
            
           // mainOrgan = Instantiate(prefab);
          //  mainOrgan.transform.position = position;
          // mainOrgan.name = $"Cube{Name}";
            
           // Object_Transform = mainOrgan.transform;
            //Object_Transform.localScale = new Vector3(scale,scale,scale);
           
            //Rendering
           // rend = mainOrgan.GetComponent<Renderer>();
           // rend.enabled = true;
           // rend.sharedMaterial = material;

            
    // }
  // }
    //Variables
    
    //public planetPhysics planet_physics;
    
   // Transform[] Transforms_In_Simulation;
   // secondaryTransform[] secondaryTransforms_In_Simulation;
   // [SerializeField] int Amount_Planets_In_Simulation;
//  public static int Starting_Velocity_Size = 8;
    

      
    
    






    //The Touch
    //void Start(){

             


             //Amount_Planets_In_Simulation = Random.Range(4,12);
             //Transforms_In_Simulation = new Transform[Amount_Planets_In_Simulation];
             //secondaryTransforms_In_Simulation = new secondaryTransform[Amount_Planets_In_Simulation];
             

            //for (int i = 0; i < Amount_Planets_In_Simulation; i++){
              //Material material1 = materials[Random.Range(0,6)];
              //PlanetaryOrgan planet = new(testPrefab,$"{i}",material1,rend);
              
              
              //Transforms_In_Simulation[i] = planet.Object_Transform;
              //secondaryTransforms_In_Simulation[i] = planet.Object_secondary_Transform;
            // }
            
            //planet_physics.Ask_For_Transforms_In_Simulation(Transforms_In_Simulation);
           // planet_physics.Ask_For_secondaryTransforms_In_Simulation(secondaryTransforms_In_Simulation);
          
          
   // }      



   





    
  
//Struct
        

//public struct secondaryTransform{
        //public float mass;
        //public Vector3 velocity;
        //public float density;
        //public secondaryTransform(float m,Vector3 v, float d){
          //mass = m;
          //velocity = v;
          //density = d;
        //}
    //}
//}