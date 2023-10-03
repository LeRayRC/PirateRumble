using UnityEngine;
[System.Serializable]
public class Marine : Character
{
    // Start is called before the first frame update
    public int renown_;
    public bool isAdmiral_;

    // Start is called before the first frame update
    public Marine(){
        unitType_ = UnitType.Marine;
    }

    public override void Init(Stats stats)
    {
        base.Init(stats);
        unitType_ = UnitType.Marine;
    }

    public override void ShowData(){
        Debug.Log("Im a Marine " + ShowClass());
        base.ShowData();
        if(isAdmiral_){
            ShowHakiLevels();
        }
    }
}
