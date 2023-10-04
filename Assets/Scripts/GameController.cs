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
                if(pirateTeam_[i].character_.initiative_ >= 100 && !characterPlaying_){
                    characterPlaying_=true;
                    pirateTeam_[i].character_.playing_ = true;
                    pirateTeam_[i].character_.initiative_ = 0.0f;
                    pirateTeam_[i].charState_ = CharacterController.CharacterState.SelectingEnemy;
                    // Debug.Log("Team 1 -  " + pirateTeam_[i].name + " playing");
                    //Communicate that this player is the one playing
                }else{
                    pirateTeam_[i].character_.initiative_ += pirateTeam_[i].character_.stats_.speed_ * Time.deltaTime;
                }
            }
            for(int i=0;i<marineTeam_.Count;i++){
                if(marineTeam_[i].character_.initiative_ >= 100 && !characterPlaying_){
                    characterPlaying_=true;
                    marineTeam_[i].character_.initiative_ = 0.0f;
                    marineTeam_[i].character_.playing_ = true;
                    marineTeam_[i].charState_ = CharacterController.CharacterState.SelectingEnemy;
                    // Debug.Log("Team 2 " + marineTeam_[i].name +  " playing");
                    //Communicate that this player is the one playing
                }else{
                    marineTeam_[i].character_.initiative_ += marineTeam_[i].character_.stats_.speed_ * Time.deltaTime;
                }
            }
        }else{
            //Check for dead characters
            for(int i = marineTeam_.Count - 1; i >= 0; i--){
                if(marineTeam_[i].character_.stats_.health_ <= 0){
                    marineTeam_[i].GetComponent<SpriteRenderer>().sprite = UpdateHurtSprite(marineTeam_[i].character_.unitType_,marineTeam_[i].character_.stats_.classType_);
                    marineTeam_.RemoveAt(i);
                    //Update sprite to damaged sprite
                }
            }

            for(int i = pirateTeam_.Count - 1; i >= 0; i--){
                if(pirateTeam_[i].character_.stats_.health_ <= 0){
                    pirateTeam_[i].GetComponent<SpriteRenderer>().sprite = UpdateHurtSprite(pirateTeam_[i].character_.unitType_,pirateTeam_[i].character_.stats_.classType_);
                    pirateTeam_.RemoveAt(i);
                    //Update sprite to damaged sprite
                }
            }
        }
    }

    Sprite UpdateHurtSprite(UnitType unitType, ClassType classType){
        Sprite spriteToReturn_ = spriteSelector_.pirateRangerHurt;
        switch(classType){
            case ClassType.Ranger:
                if(unitType == UnitType.Pirate){
                    spriteToReturn_ =spriteSelector_.pirateRangerHurt;
                }else{
                    spriteToReturn_ = spriteSelector_.marineRangerHurt;
                }
                break;
            case ClassType.Figther:
                if(unitType == UnitType.Pirate){
                    spriteToReturn_ = spriteSelector_.pirateFigtherHurt;
                }else{
                    spriteToReturn_ = spriteSelector_.marineFigtherHurt;
                }
                break;
            case ClassType.Elementalist:
                if(unitType == UnitType.Pirate){
                    spriteToReturn_ =spriteSelector_.pirateElementalistHurt;
                }else{
                    spriteToReturn_ = spriteSelector_.marineElementalistHurt;
                }
                break;
            case ClassType.Slasher:
                if(unitType == UnitType.Pirate){
                    spriteToReturn_ =spriteSelector_.pirateSlasherHurt;
                }else{
                    spriteToReturn_ = spriteSelector_.marineSlasherHurt;
                }
                break;
        }
        return spriteToReturn_;
    }
}
