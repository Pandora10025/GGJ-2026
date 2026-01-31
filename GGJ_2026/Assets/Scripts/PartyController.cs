using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public enum Species
    {
        Vampire,
        Werewolf,
        Fae,
        Siren
    };

    [SerializeField] GameObject npc;
    [SerializeField] int numOfNPCs = 10;
    [SerializeField] float partyEnergy = 0f;
    [SerializeField]
    [Tooltip("vamp, were, fae, siren")]
    float[] attendeeDistribution = { 0.1f, 0.2f, 0.3f, 0.4f };//should always add up to 1
    String[] speciesNames = { "Vampire", "Werewolf", "Fae", "Siren" };

    [SerializeField] public static int partyType;
    //stairs vs balcony height when sprites are in

    [SerializeField] float xBound = 20, yBound = 20;

    // Start is called before the first frame update
    void Start()
    {
        List<int> _speciesLeft = new List<int>();
        for (int i = 0; i < 4; i++) _speciesLeft.Add(i);

        //for now, random
        partyType = UnityEngine.Random.Range(0, _speciesLeft.Count);
        float _guestsRemaining = 1f;

        //for eaach species but not always in the same order

        //pick from the species and then assign an amount of attendees
        float _firstSpecies = UnityEngine.Random.Range(0.5f, 0.7f);
        attendeeDistribution[partyType] = _firstSpecies;
        _guestsRemaining -= _firstSpecies;
        _speciesLeft.Remove(partyType);

        //populate remaining stuff
        for (int i = 0; i < 3; i++)
        {
            int _currSpecies = UnityEngine.Random.Range(0, _speciesLeft.Count);
            float _currSpeciesPercent = 0f;
            if (i == 2)
            {
                _currSpeciesPercent = _guestsRemaining;
            }
            else
            {
                _currSpeciesPercent = UnityEngine.Random.Range(0.05f, _guestsRemaining * 3 / 4);
                _guestsRemaining -= _currSpeciesPercent;
            }
            _speciesLeft.Remove(_currSpecies);
            attendeeDistribution[_currSpecies] = _currSpeciesPercent;
        }

        
        
        Debug.Log("Party type: " + speciesNames[partyType]);
        for (int i = 0; i < 4; i++) {
            //round to 2 sig figs
            attendeeDistribution[i] = (Mathf.Round(attendeeDistribution[i] * 100));
            Debug.Log(speciesNames[i] + " " + attendeeDistribution[i]);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
