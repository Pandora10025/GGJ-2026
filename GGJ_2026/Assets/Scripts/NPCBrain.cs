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
    public int groupNum = -1;
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
        //flip a coin
        if (UnityEngine.Random.Range(0, 1) == 1)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        //Debug.Log((species * 3) + (groupNum - 1));
        
        
        this.GetComponent<SpriteRenderer>().sprite = allSprites[UnityEngine.Random.Range(0, 2)];
        
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

    public void SetSpeciesAndGroupNum(int species, int groupNum)
    {

    }
}

