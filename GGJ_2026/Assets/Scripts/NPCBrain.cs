using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        PartyController.partyEnergy = 0;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Talk();
                showDialogue = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            showDialogue = false;
        }
    }

    public void ShowDialogue(bool _showDialogue)
    {
        showDialogue = _showDialogue;
    }

    public void Talk()
    {
        int like = -1;
        float rand = UnityEngine.Random.Range(0.0f, 10.0f);
        if(rand > 2.5)
        {
            like = UnityEngine.Random.Range(4, 2);
        }
        else
        {
            like = UnityEngine.Random.Range(0, 1);
        }
        string s = "";
        switch (like)
        {
            case 4:
                s = "Divine.Such refined elegance.";
                break;
            case 3:
                s = "How delightfully dramatic.";
                break;
            case 2:
                s = "Mm.Passible.";
                break;
            case 1:
                s = "A bit gauche.";
                break;
            case 0:
                s = "I wouldn't be caught dead wearing that…";
                break;
            default:
                s = "Hmph.";
                break;
        }
        PartyController.partyEnergy += like;
        canvas.GetComponentInChildren<TextMeshProUGUI>().text = s;
        Debug.Log(PartyController.partyEnergy);
    }

    public void SetSpeciesAndGroupNum(int species, int groupNum)
    {
        //set these
    }
}

