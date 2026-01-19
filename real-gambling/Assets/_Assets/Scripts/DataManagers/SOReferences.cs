using Sirenix.OdinInspector;
using UnityEngine;

public class SOReferences : MonoBehaviour
{
    [SerializeField, AssetsOnly] private ReelIconValuesSO reelIconValuesSO;
    [SerializeField, AssetsOnly] private CombinationManagerSO combinationManagerSO;

    public ReelIconValuesSO Icons => reelIconValuesSO;
    public CombinationManagerSO Combinations => combinationManagerSO;

    private static SOReferences instance;
    public static SOReferences Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
