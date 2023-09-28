using UnityEngine;
[System.Serializable]
public class Marine : Character
{
    // Start is called before the first frame update
    public int renown_;
    public bool isAdmiral_;

    // Start is called before the first frame update
    public Marine(int renown,bool isAdmiral, int atk, int def, float speed, float critChance, ClassType classType) : base(atk,def,speed,critChance,classType){
        renown_ = renown;
        isAdmiral_ =  isAdmiral;
        unitType_ = 1;
        if(isAdmiral_){
            SetHakiLevels(Random.Range(0,2),Random.Range(0,2),Random.Range(0,2));
        }
    }

    public  override void ShowData(){
        Debug.Log("Im a Marine " + ShowClass());
        base.ShowData();
        if(isAdmiral_){
            ShowHakiLevels();
        }
    }
}
