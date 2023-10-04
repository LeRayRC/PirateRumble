using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public enum CharacterState{
        Idle,
        SelectingEnemy,
        MovingToEnemy,
        Attacking,
        Retrieving,
    };
    public CharacterState charState_;
    public Character character_;
    public float turnTimer_;
    Transform tr_;
    Vector3 originalPos_;
    // public bool isPirate_;
    // public Pirate pirate_;
    // public Marine marine_;
    public GameController gc_; 
    Classes classStats_;
    //Enemy Interaction
    Transform trEnemy_;
    public int selectedEnemy_;

    //UI
    public Image actionBar_;
    public Image healthBar_;
    public float actionBarOffset_;
    public float healthBarOffset_;
    public Camera cam_;
    SpriteRenderer ownSprite_;
    void Start()
    {
        ClassType classSelected_;
        classStats_ = gc_.GetComponent<Classes>();
        tr_ = GetComponent<Transform>();
        ownSprite_ = GetComponent<SpriteRenderer>();
        originalPos_ = tr_.position;

        if(character_.unitType_ == UnitType.Pirate){
            gc_.pirateTeam_.Add(this);
        }else{
            gc_.marineTeam_.Add(this);
        }

        do{
            classSelected_ =  (ClassType)Random.Range(1,5);
        }while(CheckClassInUse(character_.unitType_,classSelected_));

        switch(classSelected_){
            case ClassType.Ranger:
                if(character_.unitType_ == UnitType.Pirate){
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().pirateRanger;
                    character_ = new Pirate();
                    character_.Init(classStats_.rangerStats_);
                    // pirate_ = new Pirate(Random.Range(1000,5000),false,70,20,30,1,0.5f,ClassType.Ranger);
                }else{
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().marineRanger;
                    character_ = new Marine();
                    character_.Init(classStats_.rangerStats_);
                }
                break;
            case ClassType.Figther:
                if(character_.unitType_ == UnitType.Pirate){
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().pirateFigther;
                    character_ = new Pirate();
                    character_.Init(classStats_.figtherStats_);
                }else{
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().marineFigther;
                    character_ = new Marine();
                    character_.Init(classStats_.figtherStats_);
                }
                break;
            case ClassType.Slasher:
                if(character_.unitType_ == UnitType.Pirate){
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().pirateSlasher;
                    character_ = new Pirate();
                    character_.Init(classStats_.slasherStats_);
                }else{
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().marineSlasher;
                    character_ = new Marine();
                    character_.Init(classStats_.slasherStats_);
                }
                break;
            case ClassType.Elementalist:
                if(character_.unitType_ == UnitType.Pirate){
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().pirateElementalist;
                    character_ = new Pirate();
                    character_.Init(classStats_.elementalistStats_);
                    // pirate_ = new Pirate(Random.Range(1000,5000),false,50,30,20,1,0.15f,ClassType.Elementalist);
                }else{
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().marineElementalist;
                    character_ = new Marine();
                    character_.Init(classStats_.elementalistStats_);
                }
                break;
        }
        turnTimer_ = 10.0f;

        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(cam_.WorldToScreenPoint(tr_.position));
        // Debug.Log(Screen.width + ", " + Screen.height);
        UpdateActionBar();
        UpdateHealthBar();
        if(character_.playing_){
            //Acting
            //Decide which enemy to attack
            switch(charState_){
                case CharacterState.SelectingEnemy:
                    if(character_.unitType_ == UnitType.Pirate){
                        selectedEnemy_ = Random.Range(0,gc_.marineTeam_.Count);
                        trEnemy_ = gc_.marineTeam_[selectedEnemy_].GetComponent<Transform>();
                        Debug.Log("Selected marine " + selectedEnemy_ + " at pos: " + trEnemy_.position.x + " , " + trEnemy_.position.y + " , " + trEnemy_.position.z);
                    }else{
                        selectedEnemy_ = Random.Range(0,gc_.pirateTeam_.Count);
                        trEnemy_ = gc_.pirateTeam_[selectedEnemy_].GetComponent<Transform>();
                        Debug.Log("Selected pirate " + selectedEnemy_ + " at pos: " + trEnemy_.position.x + " , " + trEnemy_.position.y + " , " + trEnemy_.position.z);
                    }
                    charState_ = CharacterState.MovingToEnemy;
                    
                    break;
                case CharacterState.MovingToEnemy:
                    if(Vector3.Distance(tr_.position,trEnemy_.position) > 3.0f){
                        tr_.position = Vector3.Lerp(tr_.position,trEnemy_.position,Time.deltaTime);
                    }else{
                        charState_ = CharacterState.Attacking;
                    }
                    healthBar_.enabled = false;
                    break;
                case CharacterState.Attacking:
                    //Calculate damage reduction from target
                    switch(character_.unitType_){
                        case UnitType.Pirate:
                            gc_.marineTeam_[selectedEnemy_].character_.TakeDamage(character_.Attack(gc_.marineTeam_[selectedEnemy_].character_));
                            break;
                        case UnitType.Marine:
                            gc_.pirateTeam_[selectedEnemy_].character_.TakeDamage(character_.Attack(gc_.pirateTeam_[selectedEnemy_].character_));
                            break;
                    }
                    // float damageReduction_;
                    // float damageDealt_;
                    // if(character_.unitType_ == UnitType.Pirate){
                    //     damageReduction_ = ( 1.0f - (100.0f / ( 100.0f + gc_.marineTeam_[selectedEnemy_].marine_.def_)));
                    //     if(Random.Range(0,10) * 0.1f <= character_.stats_.critChance_){
                    //         //Crit hit
                    //         damageDealt_ = pirate_.atk_ *1.5f*(1.0f - damageReduction_);
                    //     }else{
                    //         damageDealt_ = pirate_.atk_ * (1.0f - damageReduction_);
                    //     }
                    //     Debug.Log("  Delt " + damageDealt_ + " to " + gc_.marineTeam_[selectedEnemy_].gameObject.name);
                    //     gc_.marineTeam_[selectedEnemy_].marine_.health_ -= (int)damageDealt_;
                    // }else{
                    //     damageReduction_ = ( 1.0f - (100.0f / ( 100.0f + gc_.pirateTeam_[selectedEnemy_].pirate_.def_)));
                    //     if(Random.Range(0,10) * 0.1f <= marine_.critChance_){
                    //         //Crit hit
                    //         damageDealt_ = marine_.atk_ *1.5f*(1.0f - damageReduction_);
                    //     }else{
                    //         damageDealt_ = marine_.atk_ * (1.0f - damageReduction_);
                    //     }
                    //     Debug.Log(selectedEnemy_);
                    //     Debug.Log("  Delt " + damageDealt_ + " to " + gc_.pirateTeam_[selectedEnemy_].gameObject.name);
                    //     gc_.pirateTeam_[selectedEnemy_].pirate_.health_ -= (int)damageDealt_;
                    // }
                    charState_ = CharacterState.Retrieving;
                    break;
                case CharacterState.Retrieving:
                    if(Vector3.Distance(tr_.position,originalPos_) > 0.5f){
                        tr_.position = Vector3.Lerp(tr_.position,originalPos_,Time.deltaTime);
                    }else{
                        tr_.position = originalPos_;
                        turnTimer_ = 10.0f;
                        character_.playing_ = false;
                        gc_.characterPlaying_ = false;
                        healthBar_.enabled = true;
                    }
                    break;
            }

            
            // Debug.Log("My Turn!! I am" + gameObject.name);
            
        }
    }

    void UpdateActionBar(){
        actionBar_.fillAmount = character_.initiative_ * 0.01f;
        Vector3 playerPositionOnScreen = cam_.WorldToScreenPoint(tr_.position);
        RectTransform rect = actionBar_.GetComponent<RectTransform>();
        if(character_.unitType_ == UnitType.Pirate){
            rect.anchoredPosition = new Vector3(playerPositionOnScreen.x - actionBarOffset_, playerPositionOnScreen.y, playerPositionOnScreen.z);
        }else{
            rect.anchoredPosition = new Vector3(playerPositionOnScreen.x + actionBarOffset_, playerPositionOnScreen.y, playerPositionOnScreen.z);
        }
    }

    void UpdateHealthBar(){
        if(character_.unitType_ == UnitType.Pirate){
            healthBar_.fillAmount = (1.0f * character_.stats_.health_) / character_.stats_.totalHealth_;
        }else{
            healthBar_.fillAmount = (1.0f * character_.stats_.health_) / character_.stats_.totalHealth_;
        }
        Vector3 playerPositionOnScreen = cam_.WorldToScreenPoint(tr_.position);
        RectTransform rect = healthBar_.GetComponent<RectTransform>();
        if(character_.unitType_ == UnitType.Pirate){
            rect.anchoredPosition = new Vector3(playerPositionOnScreen.x - healthBarOffset_, playerPositionOnScreen.y, playerPositionOnScreen.z);
        }else{
            rect.anchoredPosition = new Vector3(playerPositionOnScreen.x + healthBarOffset_, playerPositionOnScreen.y, playerPositionOnScreen.z);
        }
    }

    public bool CheckClassInUse(UnitType unitType, ClassType classType){
        bool inUse = false;
        if(unitType == UnitType.Pirate){
            for(int i=0;i<gc_.pirateTeam_.Count && !inUse;i++){
                if(gc_.pirateTeam_[i].character_.stats_.classType_ == classType){
                    inUse = true;
                }
            }
        }else{
            for(int i=0;i<gc_.marineTeam_.Count && !inUse;i++){
                if(gc_.marineTeam_[i].character_.stats_.classType_ == classType){
                    inUse = true;
                }
            }
        }
        return inUse;
    }
}
