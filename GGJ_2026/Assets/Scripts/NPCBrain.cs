using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBrain : MonoBehaviour
{
    public enum Species
    {
        Vampire,
        Werewolf,
        Fae,
        Siren
    };

    GameObject npc;
    public int species = -1;
    Sprite spr;
    

    private void Awake()
    {
        this.GetComponent<Transform>().localScale = PlayerController.CalculateScale(this.GetComponent<Transform>().position.y);
        switch (species)
        {
            case (int) Species.Vampire:
                this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                break;
            case (int) Species.Werewolf:
                this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
                break;
            case (int)Species.Fae:
                this.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                break;
            case (int) Species.Siren:
                this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
                break;
            default:
                break;
        }
    }
    void Update()
    {
        //dialogue pops up while nearby
    }

    
}

