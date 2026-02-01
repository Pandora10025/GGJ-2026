using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneLoader : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene("MaskLoadTest");
    }
}
