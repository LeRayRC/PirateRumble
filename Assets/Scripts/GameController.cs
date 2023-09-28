using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Character> team1List = new List<Character>();
    public List<Character> team2List = new List<Character>();
    public bool characterPlaying_;
    void Start()
    {
        characterPlaying_ = false;
        team1List.Insert(0,new Character());
        team2List.Insert(0,new Character());
    }

    // Update is called once per frame
    void Update()
    {
        //Update Initiatives
        if(!characterPlaying_){
            for(int i=1;i<team1List.Count && !characterPlaying_;i++){
                if(team1List[i].initiative_ >= 100 && !characterPlaying_){
                    characterPlaying_=true;
                    team1List[i].playing_ = true;
                    team1List[i].initiative_ = 0.0f;
                    Debug.Log("Team 1 - Character "+ i + " playing");
                    //Communicate that this player is the one playing
                }else{
                    team1List[i].initiative_ += team1List[i].speed_ * Time.deltaTime;
                }
            }
            for(int i=1;i<team2List.Count && !characterPlaying_;i++){
                if(team2List[i].initiative_ >= 100 && !characterPlaying_){
                    characterPlaying_=true;
                    team2List[i].initiative_ = 0.0f;
                    team2List[i].playing_ = true;
                    Debug.Log("Team 2 - Character " + i + " playing");
                    //Communicate that this player is the one playing
                }else{
                    team2List[i].initiative_ += team2List[i].speed_ * Time.deltaTime;
                }
            }
        }else{
            Debug.Log("Finishing turn");
            // characterPlaying_=false;
        }
    }
}
