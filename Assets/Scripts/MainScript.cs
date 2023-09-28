using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MainScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Pirate pirate_;
    public Marine marine_;
    public List<Character> characterList_;
    void Start()
    {
        pirate_ = new Pirate(400,true,20,30,15.0f,0.30f,ClassType.Elementalist);
        marine_ = new Marine(1200,false,20,30,15.0f,0.30f,ClassType.Figther);
        // char1 = new Character(20,30,40,0.30f,ClassType.Ranger);
        // char1 = new Character(30,40,50,0.30f,ClassType.Figther);
        // char1 = new Character(40,50,60,0.30f,ClassType.Slasher);
        // pirate_.ShowData();
        characterList_.Add(pirate_);
        characterList_.Add(marine_);
    }

    // Update is called once per frame
    void Update()
    {
        // pirate_.initiative += pirate_.speed_ * Time.deltaTime;
        // if(pirate_.initiative >= 100){
        //     Debug.Log("Pirate Turn");
        //     Debug.Break();
        // }else{
        //     Debug.Log("Pirate initiative " + pirate_.initiative);
        // }
    }
}
