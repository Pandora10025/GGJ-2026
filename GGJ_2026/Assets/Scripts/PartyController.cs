using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public int day = 2;
    public enum Species
    {
        Vampire,
        Werewolf,
        Fae,
        Siren
    };

    [SerializeField] GameObject npc;
    int[] numOfNPCs = { 20, 28, 35 };
    [SerializeField] double partyEnergy = 0;
    [SerializeField]
    [Tooltip("vamp, were, fae, siren")]
    int[] attendeeDistribution = { 10, 20, 30, 40 };//should always add up to 1
    String[] speciesNames = { "Vampire", "Werewolf", "Fae", "Siren" };
    List<Vector2Int>guestLocations = new List<Vector2Int>();
    Vector3 empty = new Vector3(-1, -1, -1);

    [SerializeField] public static int partyType;
    //stairs vs balcony height when sprites are in

    
    [SerializeField] float posXBound = 9.5f, negXBound = -9.0f;
    float[] yLevels = { 3.7f, -2f, -4.5f, -7f};
    Vector3[,] possiblePositions = new Vector3[4, 9];//2d array
    int[] spotsPerY = new int[4];
    int totalSpots = -1;
    public static List<GameObject> guestObjects = new List<GameObject>();

    //TODO: dump all arrays of game objects when switching scenes

    // Start is called before the first frame update
    void Start()
    {
        InitializePossibleGuestLocations();

        
    }

    void InitializePossibleGuestLocations()
    {
        //set depth here, and set anti swag here
        for(int i = 0; i < possiblePositions.GetLength(0); i++)
        {
            
            for(int j = 0; j < possiblePositions.GetLength(1); j++)
            {
                possiblePositions[i, j] = empty;
            }
        }

        float xDist = posXBound - negXBound;
        Debug.Log(xDist);
        
        switch (day)
        {
            case 0://7753
                spotsPerY[0] = 7;
                spotsPerY[1] = 7;
                spotsPerY[2] = 5;
                spotsPerY[3] = 3;
                break;
            case 1://8864
                spotsPerY[0] = 8;
                spotsPerY[1] = 8;
                spotsPerY[2] = 6;
                spotsPerY[3] = 4;
                break;
            case 2://9975
                spotsPerY[0] = 9;
                spotsPerY[1] = 9;
                spotsPerY[2] = 7;
                spotsPerY[3] = 5;
                break;
            default:
                Debug.Log("Something's wrong with the date!");
                break;
        }

        
        for (int y = 0; y < possiblePositions.GetLength(0); y++)
        {
            for(int x = 0; x < spotsPerY[y]; x++)
            {
                float currX = negXBound + ((x * (xDist / (spotsPerY[y] - 1))));
                float currY = yLevels[y];
                possiblePositions[y, x] = new Vector3(currX, currY);
                totalSpots++;
                
            }
        }
    }


    /// <summary>
    /// Ever wanted to randomize what guests come to your party? Well now you can!
    /// </summary>
    void RandomizeGuests()
    {
        UnityEngine.Random.InitState((int)Time.time);
        List<int> _speciesPopulator = new List<int>();
        List<int> _speciesLeft = new List<int>();
        for(int i = 0; i < 4; i++){_speciesPopulator.Add(i);}
        
        int rand;
        
        for (int i = 0; i < 4; i++) {
             rand = UnityEngine.Random.Range(0, _speciesPopulator.Count);
            _speciesLeft.Add(_speciesPopulator[rand]);
            _speciesPopulator.RemoveAt(rand);
        }
        /*for (int i = 0; i < 4; i++)
        {
            Debug.Log(speciesNames[_speciesLeft[i]]);
        }*/


        //for now, random
        partyType = _speciesLeft[0];
        int _guestsRemaining = numOfNPCs[day];

        //for eaach species but not always in the same order

        //pick from the species and then assign an amount of attendees
        int _firstSpecies = UnityEngine.Random.Range((int)(0.5 * numOfNPCs[day]), (int)(0.7 * numOfNPCs[day]));
        attendeeDistribution[partyType] = _firstSpecies;
        _guestsRemaining -= _firstSpecies;
        //Debug.Log(DebugRandom(_firstSpecies, partyType, _guestsRemaining));
        

        //populate remaining stuff
        for (int i = 1; i < 4; i++)
        {
            
            int _currSpeciesPercent = 0;
            if (i == 3)
            {
                _currSpeciesPercent = _guestsRemaining;
            }
            else
            {
                _currSpeciesPercent = UnityEngine.Random.Range((int)(0.1 * numOfNPCs[day]), (int)(_guestsRemaining * 0.75));
              
            }
            _guestsRemaining -= _currSpeciesPercent;
            //Debug.Log(DebugRandom(_currSpeciesPercent, _speciesLeft[i], _guestsRemaining));
            
            attendeeDistribution[_speciesLeft[i]] = _currSpeciesPercent;
        }
        


        //Debug.Log("Party type: " + speciesNames[partyType]);
        for (int i = 0; i < 4; i++)
        {
            //round to 2 sig figs
            attendeeDistribution[i] = attendeeDistribution[i];
            //Debug.Log(speciesNames[i] + " " + attendeeDistribution[i]);
        }
    }

    /// <summary>
    /// ever wanted to be angry at your code? i am.
    /// </summary>
    /// <param name="specPercent"></param>
    /// <param name="species"></param>
    /// <param name="specLeft"></param>
    /// <returns></returns>
    String DebugRandom(int specPercent, int species, int specLeft)
    {
        String s = "";
        s += "\nCurrent species: " + speciesNames[species] + " with " + specPercent + " guests. " + specLeft + " guests remaining.";

        return s;
    }

    /// <summary>
    /// Places all guests down in the spots from possiblePositions
    /// </summary>
    void GuestPlacement()
    {
        int[] _speciesFrequency = OrganizeSpecies();

        //2d array that holds species, no. of members per group, no. of groups
        //{species, perGroup, numGroups}
        //{species, remainder, 1}
        int[,] speciesGroups = new int[3, 8];
        string s = "";
        for(int i = 0; i < speciesGroups.GetLength(1); i+=2)
        {
            Debug.Log(i);
            int div = (attendeeDistribution[i/2] > 5 ? 3 : 2);
            speciesGroups[0, i] = _speciesFrequency[i/2];
            speciesGroups[1, i] = div;
            speciesGroups[2, i] = attendeeDistribution[i/2] / div;

            //remainder
            speciesGroups[0, i+1] = _speciesFrequency[i/2];
            speciesGroups[1, i+1] = attendeeDistribution[i/2] % div;
            speciesGroups[2, i+1] = 1;

            s += "[";
            s += speciesGroups[0, i] + ", ";
            s += speciesGroups[1, i] + ", ";
            s += speciesGroups[2, i] + "]\n";

            //remainder
            s += "[";
            s += speciesGroups[0, (i + 1)] + ", ";
            s += speciesGroups[1, i + 1] + ", ";
            s += speciesGroups[2, i + 1] + "]\n";

        }

        Debug.Log(s);
        


        /*List<int> spotsTaken = new List<int>();
        int guestsLeftToPlace = numOfNPCs[day];
        while(guestsLeftToPlace > 0)//per each day
        {
            int spot_to_check = UnityEngine.Random.Range(0, guestsLeftToPlace);
            //draw number

            //check empty
            while (spotsTaken.Contains(spot_to_check)){
                spot_to_check = UnityEngine.Random.Range(0, guestsLeftToPlace);
            }

            //check adjacency
            int _temp = -1;
            for (int y = 0; y < possiblePositions.GetLength(0); y++)
            {
                for (int x = 0; x < spotsPerY[y]; x++)//x, y
                {
                    _temp++;
                    if (_temp == spot_to_check)
                    {
                        if (guestLocations.Contains(new Vector2Int(x+1, y)) || guestLocations.Contains(new Vector2Int(x - 1, y)))
                        {//there's one adjacent
                            guestsLeftToPlace++;//fail condition, no guest placed
                        }
                    }
                    else
                    {//place guest!

                    }
                    

                }
            }
            guestsLeftToPlace--;
        }*/

        

    }

    /*Vector3 FindNextSpot()
    {
        Vector3 v3;



        return v3;
    }*/

    /// <summary>
    /// helper function for GuestPlacement that organizes the species in frequency order
    /// uses a selection sort
    /// </summary>
    /// <param name="_speciesFreq"></param>
    int[] OrganizeSpecies()
    {
        int[] _speciesFrequency = { 0, 1, 2, 3 };
        
        int n = attendeeDistribution.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int min_idx = i;
            for (int j = i + 1; j < n; j++)
            {
                if (attendeeDistribution[j] < attendeeDistribution[min_idx])
                {

                    min_idx = j;
                }
            }
            //swap
            int temp = attendeeDistribution[i];
            attendeeDistribution[i] = attendeeDistribution[min_idx];
            attendeeDistribution[min_idx] = temp;

            temp = _speciesFrequency[i];
            _speciesFrequency[i] = _speciesFrequency[min_idx];
            _speciesFrequency[min_idx] = temp;
        }
        
        
        for(int i = 0; i < _speciesFrequency.Length; i++)
        {
            Debug.Log(attendeeDistribution[i] + " " + _speciesFrequency[i] + " " + speciesNames[_speciesFrequency[i]]);
        }

        return _speciesFrequency;
        
    }

    /// <summary>
    /// properly initiates a cool and awesome npc!
    /// </summary>
    /// <param name="_species"></param>
    /// <param name="_position"></param>
    void createNPC(int _species, Vector3 _position)
    {
        GameObject temp = Instantiate(npc, _position, Quaternion.identity);
        temp.GetComponent<NPCBrain>().species = _species;
       //PlayerController.CalculateScale

        guestObjects.Add(temp);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeGuests();
            OrganizeSpecies();
            GuestPlacement();
        }
    }
}
