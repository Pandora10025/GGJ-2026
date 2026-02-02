using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour
{
    public static Day instance;
    public static int day = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        DontDestroyOnLoad(this);
    }
}
