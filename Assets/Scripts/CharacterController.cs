using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    enum CharacterSatate{
        MovingToEnemy,
        Attacking,
        Retrieving,
    };
    public bool isPirate_;
    public Pirate pirate_;
    public Marine marine_;
    public GameController gc_; 
    public float turnTimer_;
    void Start()
    {
        switch((ClassType)Random.Range(0,4)){
            case ClassType.Ranger:
                if(isPirate_){
                    pirate_ = new Pirate(Random.Range(1000,5000),false,15,5,10,0.6f,ClassType.Ranger);
                }else{
                    marine_ = new Marine(Random.Range(1000,5000),false,15,5,10,0.6f,ClassType.Ranger);
                }
                break;
            case ClassType.Figther:
                if(isPirate_){
                    pirate_ = new Pirate(Random.Range(1000,5000),false,10,15,5,0.2f,ClassType.Figther);
                }else{
                    marine_ = new Marine(Random.Range(1000,5000),false,10,15,5,0.2f,ClassType.Figther);
                }
                break;
            case ClassType.Slasher:
                if(isPirate_){
                    pirate_ = new Pirate(Random.Range(1000,5000),false,15,5,10,0.4f,ClassType.Slasher);
                }else{
                    marine_ = new Marine(Random.Range(1000,5000),false,15,5,10,0.4f,ClassType.Slasher);
                }
                break;
            case ClassType.Elementalist:
                if(isPirate_){
                    pirate_ = new Pirate(Random.Range(1000,5000),false,10,2,7,0.15f,ClassType.Elementalist);
                }else{
                    marine_ = new Marine(Random.Range(1000,5000),false,10,2,7,0.15f,ClassType.Elementalist);
                }
                break;
        }
        if(isPirate_){
            gc_.team1List.Add(pirate_);
        }else{
            gc_.team2List.Add(marine_);
        }
        turnTimer_ = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(marine_.playing_ || pirate_.playing_){
            //Acting
            Debug.Log("My Turn!! I am" + gameObject.name);
            turnTimer_ -= Time.deltaTime;
            if(turnTimer_ <= 0.0f){
                gc_.characterPlaying_ = false;
                turnTimer_ = 10.0f;
                if(isPirate_){
                    pirate_.playing_ = false;
                }else{
                    marine_.playing_ = false;
                }
            }
        }
    }
}
