
using UnityEngine;
using System.Collections.Generic;
using System;


public class planetPhysics : MonoBehaviour{
    
    //Variable/Fields

    public float timeVar; 
    public planetManager PlanetManager; 
    planetEncountant planet_ecountant;
    public int Number_Planets_In_Simulation;
    public List<Transform> Transforms_In_Simulation;
    public List<planetManager.secondaryTransform> secondaryTransforms_In_Simulation;
    public planetManager.secondaryTransform Sun_secondaryTransform;
    public List<planetManager.PlanetaryOrgan> Planets_In_Simulation;
    
    //Delegates/Events
    
    //public event Action<planetManager.secondaryTransform, planetManager.secondaryTransform, GameObject> OnPlanetInbound;
    
    








  //Functions
    float calculateGravity(planetManager.secondaryTransform a, planetManager.secondaryTransform b, Transform ap, Transform bp){
       
       //Operatives
        float distanceBetweenCenters = (bp.position - ap.position).magnitude; //find distance
        float diameterBetweenCenters = (ap.localScale.x + bp.localScale.x)/2; //add radii
        float gravitationForce = -9000;

        if (distanceBetweenCenters > diameterBetweenCenters){
          //Gravity
          float distanceBetweenCentersSqr = (bp.position - ap.position).sqrMagnitude;
          float CollectiveMass = a.mass * b.mass;
           gravitationForce = (PlanetManager.gravitationalConstant * CollectiveMass) / distanceBetweenCentersSqr;
          //Debug
          //print($"gravitationForce : \n{gravitationForce}");
        }
        

        
        return gravitationForce; 
    }

    public void Ask_For_Transforms_In_Simulation(List<Transform> Transforms_InSimulation, List<planetManager.PlanetaryOrgan> Planets_InSimulation){
        Transforms_In_Simulation = Transforms_InSimulation;
        Number_Planets_In_Simulation = Transforms_In_Simulation.Count ;
        Planets_In_Simulation = Planets_InSimulation;
    }

    public void Ask_For_secondaryTransforms_In_Simulation(List<planetManager.secondaryTransform> secondaryTransforms_InSimulation,planetManager.secondaryTransform Sun_st){
      secondaryTransforms_In_Simulation = secondaryTransforms_InSimulation;
      Sun_secondaryTransform = Sun_st;
    }


  //Physics
    void FixedUpdate()
    {
      bool GameOn = true;

        if (Input.GetKeyDown(KeyCode.Space)){
          GameOn = !GameOn;
          //Debug
          //print($"Game On :{GameOn}, It is now {Time.fixedTime}");
        }
      
        if (!GameOn){
          //Just In case I want to put some information for pausing simulations
        }
        else if(GameOn){
          
            for(int i = 0; i < Number_Planets_In_Simulation;i++){
           
              //Variables
           
              planetManager.secondaryTransform a = secondaryTransforms_In_Simulation[i];
              Transform ap = Transforms_In_Simulation[i];
            
              Vector3 CurrentVelocity = new(0,0,0);
              //Attraction Between All Bodies
            
              for (int j = 0; j < Number_Planets_In_Simulation;j++){
                
                  //Anti-Self-Attraction Condition
                  if (i == j){
                    //Sun Gravity
                    //print("Gravitating");
                    CurrentVelocity += -ap.position.normalized * PlanetManager.gravitationalConstant * (Sun_secondaryTransform.mass * a.mass/ap.position.sqrMagnitude);
                    continue;
                  }


                  //Variables/Fields
                
                  planetManager.secondaryTransform b = secondaryTransforms_In_Simulation[j];
                  Transform bp = Transforms_In_Simulation[j];
               
               
               
                

                float gravitationForce = calculateGravity(a,b,ap,bp);
                
                if (gravitationForce == -9000){//Something is In the Way
                    //Destroy(Planets_In_Simulation[i].mainOrgan);
                    //secondaryTransforms_In_Simulation.RemoveAt(i);
                    //Transforms_In_Simulation.RemoveAt(i);
                }else{ 
                    Vector3 direction = (bp.position - ap.position).normalized;
                    Vector3 PlanetForce = direction * gravitationForce;
                    CurrentVelocity += PlanetForce;
                   // print($"PlanetForce {PlanetForce}, CurrentForce from Planet : {CurrentVelocity}, gravitation {gravitationForce}");
                }
              }
              
              //Motion
            
            
              Vector3 FinalAcceleration =  (CurrentVelocity + a.velocity) * PlanetManager.gravitationalConstant;
              secondaryTransforms_In_Simulation[i] = new planetManager.secondaryTransform(a.mass,a.velocity + CurrentVelocity,a.density,FinalAcceleration);
            
              //Round to The Nearest Decimal Point
              FinalAcceleration = new Vector3(Mathf.Round(FinalAcceleration.x*1000)/1000,Mathf.Round(FinalAcceleration.y*1000)/1000,Mathf.Round(FinalAcceleration.z*1000)/1000);
            
              Debug.DrawLine(ap.position,ap.position + a.velocity * 500, Color.red);
              Debug.DrawLine(ap.position,ap.position +CurrentVelocity * 500);
              
              
              
              if (true){
                ap.position += FinalAcceleration;
              }
          }
      }
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