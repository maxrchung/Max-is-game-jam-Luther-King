using System.Collections.Generic;
using UnityEngine;

public struct Match
{
    public WinningCombinationSO pattern;
    public Vector2Int[] matchPositions;

    public Match
    (
        WinningCombinationSO patternData,
        Vector2Int[] positions
    )
    {
        pattern = patternData;
        matchPositions = positions;
    }
}
