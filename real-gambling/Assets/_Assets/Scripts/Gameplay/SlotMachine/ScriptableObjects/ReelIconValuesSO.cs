using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ReelIconValuesSO", menuName = "Project SOs/ReelIconValuesSO")]
public class ReelIconValuesSO : SerializedScriptableObject
{
    [SerializeField] private List<ReelIcons> ranks;
    [SerializeField] private Dictionary<ReelIcons, IconValues> values;

    public List<ReelIcons> Ranks => ranks;
    public Dictionary<ReelIcons, IconValues> Values => values;
}
