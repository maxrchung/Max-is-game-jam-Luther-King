using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombinationManagerSO", menuName = "Scriptable Objects/CombinationManagerSO")]
public class CombinationManagerSO : ScriptableObject
{
    [SerializeField] private List<WinningCombinationSO> threeReelCombinations;
    [SerializeField] private List<WinningCombinationSO> fourReelCombinations;
    [SerializeField] private List<WinningCombinationSO> fiveReelCombinations;
    
    public List<WinningCombinationSO> ThreeReelCombinations => threeReelCombinations;
    public List<WinningCombinationSO> FourReelCombinations => fourReelCombinations;
    public List<WinningCombinationSO> FiveReelCombinations => fiveReelCombinations;
}
