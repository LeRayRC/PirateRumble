using UnityEngine;
[System.Serializable]
public class Pirate : Character
{
    public int bounty_;
    public bool isEmperor_;

    // Start is called before the first frame update
    public Pirate(int bounty,bool isEmperor,int health, int atk, int def, float speed, float critChance, ClassType classType) : base(health,atk,def,speed,critChance,classType){
        bounty_ = bounty;
        isEmperor_ =  isEmperor;
        unitType_ = 0;
        if(isEmperor_){
            SetHakiLevels(Random.Range(0,2),Random.Range(0,2),Random.Range(0,2));
        }
    }

    public  override void ShowData(){
        Debug.Log("Im a Pirate " + ShowClass());
        base.ShowData();
        if(isEmperor_){
            ShowHakiLevels();
        }
    }
}
