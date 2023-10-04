using UnityEngine;
[System.Serializable]
public class Pirate : Character
{
    public int bounty_;
    public bool isEmperor_;

    // Start is called before the first frame update
    public Pirate(){
        unitType_ = UnitType.Pirate;
    }

    public override void Init(Stats stats){
        base.Init(stats);
        unitType_ = UnitType.Pirate;
    }

    public override void ShowData(){
        Debug.Log("Im a Pirate " + ShowClass());
        base.ShowData();
        if(isEmperor_){
            ShowHakiLevels();
        }
    }
}
