using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    enum Species
    {
        Vampire,
        Werewolf,
        Fae,
        Siren
    };

    [SerializeField] GameObject npc;
    [SerializeField] int numOfNPCs = 10;
    [SerializeField] float partyEnergy = 0f;
    [SerializeField] float[] attendeeDistribution = { 0.1f, 0.2f, 0.3f, 0.4f };//should always add up to 1
    //stairs vs balcony height when sprites are in

    [SerializeField] float xBound = 20, yBound = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
