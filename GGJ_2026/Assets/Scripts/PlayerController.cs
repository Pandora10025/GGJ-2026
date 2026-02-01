using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float xSpeed = 5f, ySpeed = 5f;

    [SerializeField] public static float balconyLevel = 3.5f;
    [SerializeField] public static float scaleLevel = 0.125f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region player movement
        //the mover
        //usual make-sure-runs-the-same-on-different-machines
        float frameX = xSpeed * Time.deltaTime;
        float frameY = ySpeed * Time.deltaTime;

        

        //i dont care about the diagonal speed thing
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            player.transform.position += new Vector3(0f, frameY, 0f);
            
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.position -= new Vector3(0f, frameY, 0f);

        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position += new Vector3(frameX, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position -= new Vector3(frameX, 0f, 0f);
        }

        //need to change scale to be closer/further away when not on the balcony or stairs

        player.transform.localScale = CalculateScale(player.transform.position.y);
        
        #endregion
    }

    /// <summary>
    /// Function that takes in the y-level of a Game Object, specifically the Player and NPCs
    /// </summary>
    /// <param name="yLevel"></param>
    /// <returns>Vector3 that determines scale of GameObject</returns>
    public static Vector3 CalculateScale(float yLevel)
    {
        Vector3 scale;
        if (yLevel > balconyLevel)
        {
            scale = new Vector3(1f, 2f, 1f);

        }
        else
        {
            float _s = 1 + (scaleLevel * Mathf.Abs(yLevel - balconyLevel));
            scale = new Vector3(_s, 2*_s, _s);
        }

        return scale;
    }
}


