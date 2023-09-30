using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<CharacterController> pirateTeam_ = new List<CharacterController>();
    public List<CharacterController> marineTeam_ = new List<CharacterController>();
    public bool characterPlaying_;
    void Start()
    {
        characterPlaying_ = false;
        // pirateTeam_.Insert(0,new CharacterController());
        // marineTeam_.Insert(0,new CharacterController());
    }

    // Update is called once per frame
    void Update()
    {
        //Update Initiatives
        if(!characterPlaying_){
            for(int i=0;i<pirateTeam_.Count;i++){
                if(pirateTeam_[i].pirate_.initiative_ >= 100 && !characterPlaying_){
                    characterPlaying_=true;
                    pirateTeam_[i].pirate_.playing_ = true;
                    pirateTeam_[i].pirate_.initiative_ = 0.0f;
                    pirateTeam_[i].charState_ = CharacterController.CharacterState.SelectingEnemy;
                    Debug.Log("Team 1 -  " + pirateTeam_[i].name + " playing");
                    //Communicate that this player is the one playing
                }else{
                    pirateTeam_[i].pirate_.initiative_ += pirateTeam_[i].pirate_.speed_ * Time.deltaTime;
                }
            }
            for(int i=0;i<marineTeam_.Count;i++){
                if(marineTeam_[i].marine_.initiative_ >= 100 && !characterPlaying_){
                    characterPlaying_=true;
                    marineTeam_[i].marine_.initiative_ = 0.0f;
                    marineTeam_[i].marine_.playing_ = true;
                    marineTeam_[i].charState_ = CharacterController.CharacterState.SelectingEnemy;
                    Debug.Log("Team 2 " + marineTeam_[i].name +  " playing");
                    //Communicate that this player is the one playing
                }else{
                    marineTeam_[i].marine_.initiative_ += marineTeam_[i].marine_.speed_ * Time.deltaTime;
                }
            }
        }else{
            // Debug.Log("Finishing turn");
            // characterPlaying_=false;
        }
    }
}
