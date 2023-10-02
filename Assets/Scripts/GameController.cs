using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<CharacterController> pirateTeam_ = new List<CharacterController>();
    public List<CharacterController> marineTeam_ = new List<CharacterController>();
    public bool characterPlaying_;
    SpriteSelector spriteSelector_;
    void Start()
    {
        characterPlaying_ = false;
        spriteSelector_ = GetComponent<SpriteSelector>();
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
                    // Debug.Log("Team 1 -  " + pirateTeam_[i].name + " playing");
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
                    // Debug.Log("Team 2 " + marineTeam_[i].name +  " playing");
                    //Communicate that this player is the one playing
                }else{
                    marineTeam_[i].marine_.initiative_ += marineTeam_[i].marine_.speed_ * Time.deltaTime;
                }
            }
        }else{
            //Check for dead characters
            for(int i = marineTeam_.Count - 1; i >= 0; i--){
                if(marineTeam_[i].marine_.health_ <= 0){
                    marineTeam_[i].GetComponent<SpriteRenderer>().sprite = UpdateHurtSprite(marineTeam_[i].isPirate_,marineTeam_[i].marine_.classType_);
                    marineTeam_.RemoveAt(i);
                    //Update sprite to damaged sprite
                }
            }

            for(int i = pirateTeam_.Count - 1; i >= 0; i--){
                if(pirateTeam_[i].pirate_.health_ <= 0){
                    pirateTeam_[i].GetComponent<SpriteRenderer>().sprite = UpdateHurtSprite(pirateTeam_[i].isPirate_,pirateTeam_[i].pirate_.classType_);
                    pirateTeam_.RemoveAt(i);
                    //Update sprite to damaged sprite
                }
            }
        }
    }

    Sprite UpdateHurtSprite(bool isPirate, ClassType classType){
        Sprite spriteToReturn_ = spriteSelector_.pirateRangerHurt;
        switch(classType){
            case ClassType.Ranger:
                if(isPirate){
                    spriteToReturn_ =spriteSelector_.pirateRangerHurt;
                }else{
                    spriteToReturn_ = spriteSelector_.marineRangerHurt;
                }
                break;
            case ClassType.Figther:
                if(isPirate){
                    spriteToReturn_ = spriteSelector_.pirateFigtherHurt;
                }else{
                    spriteToReturn_ = spriteSelector_.marineFigtherHurt;
                }
                break;
            case ClassType.Elementalist:
                if(isPirate){
                    spriteToReturn_ =spriteSelector_.pirateElementalistHurt;
                }else{
                    spriteToReturn_ = spriteSelector_.marineElementalistHurt;
                }
                break;
            case ClassType.Slasher:
                if(isPirate){
                    spriteToReturn_ =spriteSelector_.pirateSlasherHurt;
                }else{
                    spriteToReturn_ = spriteSelector_.marineSlasherHurt;
                }
                break;
        }
        return spriteToReturn_;
    }
}
