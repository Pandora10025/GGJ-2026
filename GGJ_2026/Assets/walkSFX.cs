using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkSFX : MonoBehaviour
{
    public AudioClip walking;
    private PlayerController controller;
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(PlayFootSteps());
    }

    IEnumerator PlayFootSteps()
    {
        while (true)
        {
            if (controller.gameObject.GetComponent<Animator>().GetBool("isWalking") == true) 
            {
                AudioManager.instance.PlaySFX(walking);
            }
            yield return new WaitForSeconds(0.4f);
        }
    }

}
