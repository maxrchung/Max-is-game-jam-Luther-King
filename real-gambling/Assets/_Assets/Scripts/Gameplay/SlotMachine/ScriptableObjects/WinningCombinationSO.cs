using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "WinningCombinationSO", menuName = "Project SOs/WinningCombinationSO")]
public class WinningCombinationSO : SerializedScriptableObject
{
    [SerializeField] private bool[,] pattern;

    public bool[,] Pattern => pattern;

    public int Height => pattern.GetLength(1);
    public int Width => pattern.GetLength(0);
}