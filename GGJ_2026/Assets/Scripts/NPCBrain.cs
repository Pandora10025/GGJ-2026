using System;
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
    Vector3 startingScale;
    [SerializeField] Sprite[] allSprites = new Sprite[12];
    Boolean showDialogue;
    Canvas canvas;
    Vector3 canvScale;
    Vector3 canvPos;
    

    private void Awake()
    {
        canvas = this.GetComponentInChildren<Canvas>();
        
        startingScale = transform.localScale;
        canvScale = canvas.GetComponent<RectTransform>().localScale * startingScale.x;
        //canvPos = canvas.GetComponent<RectTransform>().localScale * startingScale.x;
        this.GetComponent<Transform>().localScale = PlayerController.CalculateScale(this.GetComponent<Transform>().position.y, startingScale);
        canvas.GetComponent<RectTransform>().localScale = canvScale;
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
        showDialogue = false;
    }
    void Update()
    {
        if (!showDialogue)
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
        //dialogue pops up while nearby
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            showDialogue = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            showDialogue = false;
        }
    }
}

