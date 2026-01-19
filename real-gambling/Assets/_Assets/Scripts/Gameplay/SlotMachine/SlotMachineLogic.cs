using System.Collections.Generic;
using UnityEngine;

public class SlotMachineLogic : MonoBehaviour
{
    private List<Reel> activeReels;
    private List<ReelIcons> spinResult;
    
    private void Start()
    {
        spinResult = new List<ReelIcons>();
        activeReels = new List<Reel>();

        Reel reelOne = new Reel(6, 8, 12);
        Reel reelTwo = new Reel(6, 8, 12);
        Reel reelThree = new Reel(6, 8, 12);

        activeReels = new List<Reel>()
        {
            reelOne, reelTwo, reelThree
        };
    }

    public void Spin()
    {
        spinResult.Clear();

        // 1: Spin each reel until it lands on an icon
        foreach (Reel reel in activeReels)
        {
            int reelIconSize = reel.IconsOnReel.Count;
            int resultIndex = Random.Range(0, reelIconSize);
            
            while (reel.IconsOnReel[resultIndex] == ReelIcons.None)
            {
                resultIndex++;
                resultIndex %= reelIconSize;
            }
            
            spinResult.Add(reel.IconsOnReel[resultIndex]);
        }

        PrintResult();
        
        // 2: Check if winning combination
    }

    public void PrintResult()
    {
        string result = "Result: | ";
        foreach (ReelIcons icon in spinResult)
        {
            result += icon + " | ";
        }
        Debug.Log(result);
    }
}
