using UnityEngine;

public class ConfirmMaskHotkey : MonoBehaviour
{
    public ConfirmMaskPrompt prompt;

    void Update()
    {
        if (prompt == null) return;

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (prompt.IsOpen()) prompt.ConfirmNo();
            else prompt.Open();
        }

        if (prompt.IsOpen())
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                prompt.ConfirmYes();

            if (Input.GetKeyDown(KeyCode.Escape))
                prompt.ConfirmNo();
        }
    }
}
