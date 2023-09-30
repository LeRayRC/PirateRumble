using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public enum CharacterState{
        SelectingEnemy,
        MovingToEnemy,
        Attacking,
        Retrieving,
    };
    public bool isPirate_;
    public Pirate pirate_;
    public Marine marine_;
    public GameController gc_; 
    public float turnTimer_;
    Transform trEnemy_;
    Transform tr_;
    Vector3 originalPos_;
    public CharacterState charState_;
    void Start()
    {
        tr_ = GetComponent<Transform>();
        originalPos_ = tr_.position;
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
            gc_.pirateTeam_.Add(this);
        }else{
            gc_.marineTeam_.Add(this);
        }
        turnTimer_ = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(marine_.playing_ || pirate_.playing_){
            //Acting
            //Decide which enemy to attack
            switch(charState_){
                case CharacterState.SelectingEnemy:
                    if(isPirate_){
                        int selectedEnemy_ = Random.Range(0,gc_.marineTeam_.Count);
                        trEnemy_ = gc_.marineTeam_[selectedEnemy_].GetComponent<Transform>();
                        Debug.Log("Selected marine " + selectedEnemy_ + " at pos: " + trEnemy_.position.x + " , " + trEnemy_.position.y + " , " + trEnemy_.position.z);
                    }else{
                        int selectedEnemy_ = Random.Range(0,gc_.pirateTeam_.Count);
                        trEnemy_ = gc_.pirateTeam_[selectedEnemy_].GetComponent<Transform>();
                        Debug.Log("Selected pirate " + selectedEnemy_ + " at pos: " + trEnemy_.position.x + " , " + trEnemy_.position.y + " , " + trEnemy_.position.z);
                    }
                    charState_ = CharacterState.MovingToEnemy;
                    break;
                case CharacterState.MovingToEnemy:
                    if(Vector3.Distance(tr_.position,trEnemy_.position) > 1.5f){
                        tr_.position = Vector3.Lerp(tr_.position,trEnemy_.position,Time.deltaTime);
                    }else{
                        charState_ = CharacterState.Retrieving;
                    }
                    break;
                case CharacterState.Attacking:
                    break;
                case CharacterState.Retrieving:
                    if(Vector3.Distance(tr_.position,originalPos_) > 0.5f){
                        tr_.position = Vector3.Lerp(tr_.position,originalPos_,Time.deltaTime);
                    }else{
                        tr_.position = originalPos_;
                        turnTimer_ = 10.0f;
                        if(isPirate_){
                            pirate_.playing_ = false;
                        }else{
                            marine_.playing_ = false;
                        }
                        gc_.characterPlaying_ = false;
                    }
                    // turnTimer_ -= Time.deltaTime;
                    // if(turnTimer_ <= 0.0f){
                        
                    // }
                    break;
            }

            
            // Debug.Log("My Turn!! I am" + gameObject.name);
            
        }
    }
}
