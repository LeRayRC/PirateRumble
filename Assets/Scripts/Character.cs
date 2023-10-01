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
    }
    public int health_;
    public int totalHealth_;
    public int atk_;
    public int def_;
    public float speed_;
    public float critChance_;
    public float initiative_;
    public HakiLevels hakiLevels_;
    public ClassType classType_;
    public int unitType_;
    public bool playing_;
    public float turnTimer_;
    public Character(){}
    public Character(int health, int atk, int def, float speed, float critChance, ClassType classType){
        health_ = health;
        totalHealth_ = health;
        atk_ = atk;
        def_ = def;
        speed_ = speed;
        critChance_ = critChance;
        classType_ = classType;
        initiative_ = 0.0f;
        playing_ = false;
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
}
