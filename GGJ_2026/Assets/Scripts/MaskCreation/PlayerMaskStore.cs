using UnityEngine;

public class PlayerMaskStore : MonoBehaviour
{
    public static PlayerMaskStore Instance { get; private set; }

    // ---- Logical mask data (IDs + colors) ----
    public MaskState SavedMask { get; private set; } = new MaskState();
    public bool HasSavedMask { get; private set; } = false;

    // ---- Visual snapshot (static image) ----
    public Texture2D SavedMaskTexture { get; private set; }
    public bool HasSavedMaskTexture => SavedMaskTexture != null;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Save logical state only
    public void SaveMask(MaskState state)
    {
        SavedMask = DeepCopy(state);
        HasSavedMask = true;
    }

    // Save baked static image
    public void SaveMaskTexture(Texture2D texture)
    {
        SavedMaskTexture = texture;
    }

    // Optional convenience method: save both at once
    public void SaveMask(MaskState state, Texture2D texture)
    {
        SaveMask(state);
        SaveMaskTexture(texture);
    }

    static MaskState DeepCopy(MaskState src)
    {
        return new MaskState
        {
            shapeId = src.shapeId,
            baseColor = src.baseColor,

            patternId = src.patternId,
            patternColor = src.patternColor,

            trimId = src.trimId,
            trimColor = src.trimColor,

            gildedId = src.gildedId,
            laceId = src.laceId,
            feathersId = src.feathersId,
            fursId = src.fursId,
            accessoriesId = src.accessoriesId,
            flowersId = src.flowersId
        };
    }
}
