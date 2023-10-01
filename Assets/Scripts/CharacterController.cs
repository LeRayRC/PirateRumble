using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    public Image actionBar_;
    public Image healthBar_;
    public float actionBarOffset_;
    public float healthBarOffset_;
    public Camera cam_;
    SpriteRenderer ownSprite_;
    public int selectedEnemy_ ;
    void Start()
    {
        ClassType classSelected_;
        tr_ = GetComponent<Transform>();
        ownSprite_ = GetComponent<SpriteRenderer>();
        originalPos_ = tr_.position;

        if(isPirate_){
            gc_.pirateTeam_.Add(this);
        }else{
            gc_.marineTeam_.Add(this);
        }

        do{
            classSelected_ =  (ClassType)Random.Range(1,5);
        }while(CheckClassInUse(isPirate_,classSelected_));

        switch(classSelected_){
            case ClassType.Ranger:
                if(isPirate_){
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().pirateRanger;
                    pirate_ = new Pirate(Random.Range(1000,5000),false,70,20,30,17,0.5f,ClassType.Ranger);
                    // pirate_ = new Pirate(Random.Range(1000,5000),false,70,20,30,1,0.5f,ClassType.Ranger);
                }else{
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().marineRanger;
                    marine_ = new Marine(Random.Range(1000,5000),false,70,20,30,17,0.5f,ClassType.Ranger);
                }
                break;
            case ClassType.Figther:
                if(isPirate_){
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().pirateFigther;
                    pirate_ = new Pirate(Random.Range(1000,5000),false,150,8,80,12,0.8f,ClassType.Figther);
                    // pirate_ = new Pirate(Random.Range(1000,5000),false,150,8,80,1,0.8f,ClassType.Figther);
                }else{
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().marineFigther;
                    marine_ = new Marine(Random.Range(1000,5000),false,150,8,80,12,0.8f,ClassType.Figther);
                }
                break;
            case ClassType.Slasher:
                if(isPirate_){
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().pirateSlasher;
                    pirate_ = new Pirate(Random.Range(1000,5000),false,100,15,50,25,0.3f,ClassType.Slasher);
                    // pirate_ = new Pirate(Random.Range(1000,5000),false,100,15,50,1,0.3f,ClassType.Slasher);
                }else{
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().marineSlasher;
                    marine_ = new Marine(Random.Range(1000,5000),false,100,15,50,25,0.3f,ClassType.Slasher);
                }
                break;
            case ClassType.Elementalist:
                if(isPirate_){
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().pirateElementalist;
                    pirate_ = new Pirate(Random.Range(1000,5000),false,50,30,20,8,0.15f,ClassType.Elementalist);
                    // pirate_ = new Pirate(Random.Range(1000,5000),false,50,30,20,1,0.15f,ClassType.Elementalist);
                }else{
                    ownSprite_.sprite = gc_.GetComponent<SpriteSelector>().marineElementalist;
                    marine_ = new Marine(Random.Range(1000,5000),false,50,30,20,8,0.15f,ClassType.Elementalist);
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
        if(marine_.playing_ || pirate_.playing_){
            //Acting
            //Decide which enemy to attack
            switch(charState_){
                case CharacterState.SelectingEnemy:
                    if(isPirate_){
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
                    float damageReduction_;
                    float damageDealt_;
                    if(isPirate_){
                        damageReduction_ = ( 1.0f - (100.0f / ( 100.0f + gc_.marineTeam_[selectedEnemy_].marine_.def_)));
                        if(Random.Range(0,10) * 0.1f <= pirate_.critChance_){
                            //Crit hit
                            damageDealt_ = pirate_.atk_ *1.5f*(1.0f - damageReduction_);
                        }else{
                            damageDealt_ = pirate_.atk_ * (1.0f - damageReduction_);
                        }
                        Debug.Log("  Delt " + damageDealt_ + " to " + gc_.marineTeam_[selectedEnemy_].gameObject.name);
                        gc_.marineTeam_[selectedEnemy_].marine_.health_ -= (int)damageDealt_;
                    }else{
                        damageReduction_ = ( 1.0f - (100.0f / ( 100.0f + gc_.pirateTeam_[selectedEnemy_].pirate_.def_)));
                        if(Random.Range(0,10) * 0.1f <= marine_.critChance_){
                            //Crit hit
                            damageDealt_ = marine_.atk_ *1.5f*(1.0f - damageReduction_);
                        }else{
                            damageDealt_ = marine_.atk_ * (1.0f - damageReduction_);
                        }
                        Debug.Log(selectedEnemy_);
                        Debug.Log("  Delt " + damageDealt_ + " to " + gc_.pirateTeam_[selectedEnemy_].gameObject.name);
                        gc_.pirateTeam_[selectedEnemy_].pirate_.health_ -= (int)damageDealt_;
                    }
                    charState_ = CharacterState.Retrieving;
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
                        healthBar_.enabled = true;
                    }
                    break;
            }

            
            // Debug.Log("My Turn!! I am" + gameObject.name);
            
        }
    }

    void UpdateActionBar(){
        if(isPirate_){
            actionBar_.fillAmount = pirate_.initiative_ * 0.01f;
        }else{
            actionBar_.fillAmount = marine_.initiative_ * 0.01f;
        }
        Vector3 playerPositionOnScreen = cam_.WorldToScreenPoint(tr_.position);
        RectTransform rect = actionBar_.GetComponent<RectTransform>();
        if(isPirate_){
            rect.anchoredPosition = new Vector3(playerPositionOnScreen.x - actionBarOffset_, playerPositionOnScreen.y, playerPositionOnScreen.z);
        }else{
            rect.anchoredPosition = new Vector3(playerPositionOnScreen.x + actionBarOffset_, playerPositionOnScreen.y, playerPositionOnScreen.z);
        }
    }

    void UpdateHealthBar(){
        if(isPirate_){
            healthBar_.fillAmount = (1.0f * pirate_.health_) / pirate_.totalHealth_;
        }else{
            healthBar_.fillAmount = (1.0f * marine_.health_) / marine_.totalHealth_;
        }
        Vector3 playerPositionOnScreen = cam_.WorldToScreenPoint(tr_.position);
        RectTransform rect = healthBar_.GetComponent<RectTransform>();
        if(isPirate_){
            rect.anchoredPosition = new Vector3(playerPositionOnScreen.x - healthBarOffset_, playerPositionOnScreen.y, playerPositionOnScreen.z);
        }else{
            rect.anchoredPosition = new Vector3(playerPositionOnScreen.x + healthBarOffset_, playerPositionOnScreen.y, playerPositionOnScreen.z);
        }
    }

    public bool CheckClassInUse(bool isPirate, ClassType classType){
        bool inUse = false;
        if(isPirate){
            for(int i=0;i<gc_.pirateTeam_.Count && !inUse;i++){
                if(gc_.pirateTeam_[i].pirate_.classType_ == classType){
                    inUse = true;
                }
            }
        }else{
            for(int i=0;i<gc_.marineTeam_.Count && !inUse;i++){
                if(gc_.marineTeam_[i].marine_.classType_ == classType){
                    inUse = true;
                }
            }
        }
        return inUse;
    }
}
