


using System.Collections.Generic;
using UnityEngine;

public class planetManager : MonoBehaviour
{

    
  
   //Variables
   public planetPhysics planet_physics;
   public managerSimulation ManagerSimulation;
   public float SUN_MASS;
   public float gravitationalConstant;
   int simulationLength;
   public Vector3[,] planetPositionData;
   
  //Functions
  string recognizeType(float density, float Mass, Vector3 size){
    return "";
  }
  

    //Materials
    public Material[] materials =  new Material[8];
    
    public GameObject testPrefab;
    
   
    //Class
    public class Sun{
      public GameObject mainOrgan;
      public float mass;
      public float diameter = 4000;
      public float diameterModifier =1;
      Vector3 position = Vector3.zero;
      public secondaryTransform Sun_secondary_transform;

      public Sun(GameObject prefab, float SunMass){
        mainOrgan = Instantiate(prefab);
        mainOrgan.transform.position = position;
        mainOrgan.transform.localScale = new(diameter * diameterModifier,diameter* diameterModifier,diameter* diameterModifier);
        mainOrgan.name = "Sun";

        mass = SunMass;
        Sun_secondary_transform = new(mass,Vector3.zero,diameter/mass,Vector3.zero);
      }

    }
    public class PlanetaryOrgan{
      

      //Variables
      
      public GameObject mainOrgan;
      public secondaryTransform Object_secondary_Transform;
      public Transform Object_Transform;
      
      //Transforms
       
        //Angle
        public Vector2 theta = new(Random.Range(0,Mathf.PI*2),Random.Range(0,Mathf.PI*2));
        
        //position
        public Vector3 position;
        public Vector2 worldSizeMinMax = new(500,700);
        //Size
        public float diameter;
        public Vector2 diameterMinMax = new(30,4000);//km
        public float sizeChances = Mathf.Pow(Random.Range(1,10),2)/Mathf.Pow(10,2);//NEEDS CHANGING
        public readonly float diameterModifier = 1;
        //Mass
        public float density;
        public float densityModifier = Random.Range(0.3f,4);
        //Starting Velocity
        public Vector3 startingVelocity;
        public float mean_orbital_Speed;
        //Name
        public string planetName;
        
        //Constructor
        public PlanetaryOrgan(GameObject prefab, string Name, Material planetMaterial, float Sun_mass, float gravitationalCons){
            
            
            //position
            float distance = Random.Range(worldSizeMinMax.x,worldSizeMinMax.y);
            position = new(distance * Mathf.Cos(theta.x),distance * Mathf.Sin(theta.y),0);
            //Size
            diameter = sizeChances * Random.Range(diameterMinMax.x,diameterMinMax.y) * diameterModifier; //km
            //Mass
            density =  Random.Range(0.5f*diameter / diameterMinMax.y,1) * densityModifier;
            //Velocity
            mean_orbital_Speed = Mathf.Sqrt(Sun_mass*(density * diameter) / distance );
            startingVelocity = Quaternion.AngleAxis(90,Vector3.forward) * position.normalized * mean_orbital_Speed * (Random.Range(0,2)*2-1);
            //Debug
            //print(mean_orbital_Speed);
              
            
            
           
            
            
            //Name Choosing
            planetName = "Unidentified Space Object";
            switch(diameter/diameterMinMax.y){
              case < 1/1430: 
                planetName = "Asteroid";
                break;
              case  < 1/44f:
                planetName = "Moon";
                break;
              case <= 1/11f:
                planetName = "Planet"; 
                break;
              case <= 4/11f:
                planetName = "SuperPlanet"; 
                break;
              case <= 1:
                planetName = "Gas Giant";
                break;
              default :
                break;
            }

            
            //Making of a New Planet
            
            mainOrgan = Instantiate(prefab);
            mainOrgan.transform.position = position;
            mainOrgan.transform.localScale = new Vector3(diameter,diameter,diameter);
            mainOrgan.name = $"{planetName}({Name})";
            
            Object_Transform = mainOrgan.transform;
            Object_secondary_Transform = new secondaryTransform(diameter * density,startingVelocity,density,new(0,0,0));
           
            //Rendering
            Renderer planetRenderer = mainOrgan.GetComponent<Renderer>();
            planetRenderer.enabled = true;
            planetRenderer.sharedMaterial = planetMaterial;

            
            
            
      }
    }
    //Variables
    
   
    public TrailRenderer trailb;
    public List<Transform> Transforms_In_Simulation;
    public List<secondaryTransform> secondaryTransforms_In_Simulation;
    public List<PlanetaryOrgan> Planets_In_Simulation;
    [SerializeField] int Amount_Planets_In_Simulation;
    
    


    
    






    //The Touch
    void Start(){
             SUN_MASS = 8800;
             Sun sun = new(testPrefab,SUN_MASS);
             Amount_Planets_In_Simulation = Random.Range(9,45);
             Transforms_In_Simulation = new List<Transform>();
             secondaryTransforms_In_Simulation = new List<secondaryTransform>();
             Planets_In_Simulation = new List<PlanetaryOrgan>();

             //Simulation Manager
             simulationLength = ManagerSimulation.SetSimulationLength();
             //print(simulationLength);
             planetPositionData = new Vector3[Amount_Planets_In_Simulation,simulationLength];
             
             for (int i = 0; i < Amount_Planets_In_Simulation; i++){
              //Debug
              //print(i);
              Material material1 = materials[Random.Range(0,8)];
              PlanetaryOrgan planet = new(testPrefab,$"{i}",material1,sun.mass,gravitationalConstant);
                            
              //trail
              //TrailRenderer trail = Instantiate(trailb);
              //trail.transform.parent = planet.Object_Transform;
              //trail.transform.position = planet.Object_Transform.position;
              
              planetPositionData[i,0] = planet.position;
   
              Transforms_In_Simulation.Add(planet.Object_Transform);
              secondaryTransforms_In_Simulation.Add(planet.Object_secondary_Transform);
              Planets_In_Simulation.Add(planet);
             }
             planet_physics.Ask_For_SimulationData(Transforms_In_Simulation,Planets_In_Simulation,secondaryTransforms_In_Simulation,SUN_MASS,planetPositionData);
             ManagerSimulation.GetPlanetData(Planets_In_Simulation, Amount_Planets_In_Simulation);
          
          
    }      



   





    
  
//Struct
        
[System.Serializable]
public struct secondaryTransform{
        public float mass;
         public Vector3 velocity;
       public float density;
      public Vector3 acceleration;

        public secondaryTransform(float m,Vector3 v, float d, Vector3 a){
          mass = m;
          velocity = v;
          density = d;
          acceleration = a;
        }
    }
}


//Physics Script
//{//Variable/Fields
    //public float gravitation = 1f; 
   // public planetManager PlanetManager; 
   // [SerializeField] int Number_Planets_In_Simulation;
   // [SerializeField] Transform[] Transforms_In_Simulation;
   // [SerializeField] planetManager.secondaryTransform[] secondaryTransforms_In_Simulation;
    
    

    








  //Functions
    //float calculateForces(planetManager.secondaryTransform a, planetManager.secondaryTransform b, Transform ap, Transform bp){
      
     // float gravitationForce = 0f;
      
      //if ((ap.position - bp.position).magnitude > 0){
       // float distanceBetweenCentersSqr = (bp.position - ap.position).sqrMagnitude;
      //  float CollectiveMass = a.mass * b.mass;
      //  gravitationForce = gravitation * CollectiveMass / distanceBetweenCentersSqr;
        

        
        //Debug
        //print($"gravitationForce : \n{gravitationForce}");
      //}
      //return gravitationForce; 
    //}

    //public void Ask_For_Transforms_In_Simulation(Transform[] Transforms_InSimulation){
     //   Transforms_In_Simulation = Transforms_InSimulation;
     //   Number_Planets_In_Simulation = Transforms_In_Simulation.Length;
    //}

    //public void Ask_For_secondaryTransforms_In_Simulation(planetManager.secondaryTransform[] secondaryTransforms_InSimulation){
    //  secondaryTransforms_In_Simulation = secondaryTransforms_InSimulation;
   // }
   

  //Physics
   // void Update()

   // {
      //  for (int i = 0; i < Number_Planets_In_Simulation;i++){
           
           //Variables
           
          //  float collectiveForce = 0; 
          //  Vector3 collectiveDirection = new(0,0,0);
            
          // //Attraction Between All Bodies
          //  for (int j = 0; j < Number_Planets_In_Simulation;j++){
                
              //Anti-Self-Attraction Condition
               // if (i == j){continue;}


                //Variables/Fields
              // planetManager.secondaryTransform a = secondaryTransforms_In_Simulation[i];
              // planetManager.secondaryTransform b = secondaryTransforms_In_Simulation[j];
              //Transform ap = Transforms_In_Simulation[i];
              //Transform bp = Transforms_In_Simulation[j];

                //Operatives
              //  Vector3 direction = bp.position - ap.position;
              //  collectiveForce += calculateForces(a,b,ap,bp);
              //  collectiveDirection += direction;
                
               

           // }
            //Motion
            
           // collectiveDirection = collectiveDirection.normalized;
          //  Vector3 motiveForce = collectiveDirection * collectiveForce;
            
            //Debug
           // Debug.DrawLine(Transforms_In_Simulation[i].transform.position,Transforms_In_Simulation[i].transform.position + motiveForce,Color.cyan);
            //print($"CollectiveForce : \n{collectiveForce}");
            
            
            
            //motiveForce = new Vector3(Mathf.Round(motiveForce.x*10000)/10000,Mathf.Round(motiveForce.y*10000)/10000,Mathf.Round(motiveForce.z*10000)/10000);
            


           // if (true){

             // for (int angleX = -180; angleX < 180;angleX++){
               //  for (int angleY = -180; angleY < 180; angleY++){
              //      for (int angleZ = -180; angleZ < 180; angleZ++){
                    
                    
                 // }
              //  }
             // }
           //   Transforms_In_Simulation[i].position += motiveForce;
           // }
             
            
       // }


       
   // }

//}
