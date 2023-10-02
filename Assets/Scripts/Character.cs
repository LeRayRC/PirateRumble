// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Character
{
    public struct HakiLevels{
        public int perception_;
        public int armor_;
        public int conqueror_;

        public HakiLevels(int percep, int armor, int conqueror)
        {
            perception_ = percep;
            armor_ = armor;
            conqueror_ = conqueror;
        }
    }

    public struct Stats{
        public float health_;
        public float totalHealth_;
        public float atk_;
        public float def_;
        public float speed_;
        public float critChance_;
        public float critDamageBonus_;
        public float initiative_;
        public HakiLevels hakiLevels_;
    }

    public Stats stats_;
    public ClassType classType_;
    public int unitType_;
    
    public bool playing_;
    public float turnTimer_;
    public Character(){
        stats_.health_          = 0.0f;
        stats_.totalHealth_     = 0.0f;
        stats_.atk_             = 0.0f;
        stats_.def_             = 0.0f;
        stats_.critChance_      = 0.0f;
        stats_.critDamageBonus_ = 0.0f;
        stats_.hakiLevels_      = new HakiLevels(0, 0, 0);
    }
    
    /*public Character(float health, float atk, float def, float speed, float critChance, ClassType classType){
        health_ = health;
        totalHealth_ = health;
        atk_ = atk;
        def_ = def;
        speed_ = speed;
        critChance_ = critChance;
        classType_ = classType;
        initiative_ = 0.0f;
        playing_ = false;


    }*/

    public void Init(Stats stats)
    {
        
    }

    public virtual void ShowData(){
        Debug.Log("Atk: " + atk_ + " , Def: " + def_ + " ,Speed: " + speed_);
    }

    public string ShowClass(){
        string classString_;
        switch(classType_){
            case ClassType.Ranger:
                classString_ =  "Ranger";
                break;
            case ClassType.Slasher:
                classString_ =  "Slasher";
                break;
            case ClassType.Elementalist:
                classString_ =  "Elementalist";
                break;
            case ClassType.Figther:
                classString_ =  "Fighter";
                break;
            default:
                classString_ = "";
                break;
        }
        return classString_;
    }

    public void SetHakiLevels(int perception, int armor, int conqueror){
        hakiLevels_ = new HakiLevels{
            perception_ = perception,
            armor_ = armor,
            conqueror_ = conqueror,
        };
    }

    public void ShowHakiLevels(){
        Debug.Log("Haki Levels  : " + hakiLevels_.perception_ + "," + hakiLevels_.armor_ + "," + hakiLevels_.conqueror_);
    }

    public float Attack(Character other_){
        float dmg=0.0f;
        float dmg_multiplier = 1.0f;
        //Check interclass attack bonus
        switch (classType_)
        {
            case ClassType.Ranger:
                if(other_.classType_ == ClassType.Slasher){
                    dmg_multiplier = 2.0f;
                }else if(other_.classType_ == ClassType.Figther){
                    dmg_multiplier = 0.5f;
                }
                break;
            case ClassType.Slasher:
                if (other_.classType_ == ClassType.Figther)
                {
                    dmg_multiplier = 2.0f;
                }
                else if (other_.classType_ == ClassType.Ranger)
                {
                    dmg_multiplier = 0.5f;
                }
                break;
            case ClassType.Figther:
                if (other_.classType_ == ClassType.Ranger)
                {
                    dmg_multiplier = 2.0f;
                }
                else if (other_.classType_ == ClassType.Slasher)
                {
                    dmg_multiplier = 0.5f;
                }
                break;
            default:
                dmg_multiplier = 1.0f;
                break;
        }
        //Check Crit Damage
        if(Random.Range(0.0f,1.0f) <= critChance_)
        {
            dmg_multiplier *= (critDamageBonus_ * 0.01f);
        }
        dmg = atk_ * dmg_multiplier;
        return dmg;
    }

    public void TakeDamage(float dmg)
    {
        //damageReduction_ = ( 1.0f - (100.0f / ( 100.0f + gc_.pirateTeam_[selectedEnemy_].pirate_.def_)));
        float damageReduction = (1.0f - (100.0f / (100.0f + def_)));
        health_ -= dmg * damageReduction;
    }
}