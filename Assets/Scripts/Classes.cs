using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public enum ClassType{
    None,
    Ranger,
    Slasher,
    Figther,
    Elementalist,
}

public enum UnitType{
    Pirate = 1,
    Marine
}
[System.Serializable]
public class Classes : MonoBehaviour{
    public Character.Stats rangerStats_;
    public Character.Stats slasherStats_;
    public Character.Stats elementalistStats_;
    public Character.Stats figtherStats_;

}
