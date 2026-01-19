using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ReelIconValuesSO", menuName = "Project SOs/ReelIconValuesSO")]
public class ReelIconValuesSO : SerializedScriptableObject
{
    [SerializeField] private List<ReelIcons> iconRanks;
    [SerializeField] private Dictionary<ReelIcons, IconValues> reelValues;

    public List<ReelIcons> IconRanks => iconRanks;
    public Dictionary<ReelIcons, IconValues> ReelValues => reelValues;
}
