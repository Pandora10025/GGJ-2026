using UnityEngine;

public class PlacedItemInstance : MonoBehaviour
{
    public string itemId;

    public SpriteRenderer SpriteRenderer =>
        GetComponentInChildren<SpriteRenderer>();
}
