using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.Serialization;

public class PrototypeGameSystem : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private UICounter moneyCounter;
    [SerializeField] private UICounter fingerCounter;

    [Space]
    [SerializeField] private List<UIReelSpinButton> reelSpinButtons;

    [FormerlySerializedAs("reels")]
    [Space]
    [SerializeField] private List<UIReel> uiReels;

    [Space]
    [SerializeField] private UIBetField betField;

    [Space]
    [SerializeField] private Button tradeFingerButton;
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_Text playButtonText;

    [Space]
    [SerializeField] private TextBox textBox;

    private int moneyAmount;
    private int fingerAmount;
    private int costToPlay;
    
    private Vector2 screenCenter = new Vector2(960, 510);

    private List<Reel> reelInstances;
    private ReelIcons[,] reelsAsBoard;
    
    private static PrototypeGameSystem instance;

    public int MoneyAmount
    {
        get => moneyAmount;
        set
        {
            moneyAmount = value;
            moneyCounter.SetAmountText("$" + moneyAmount.ToString());
        }
    }

    public int FingerAmount
    {
        get => fingerAmount;
        set
        {
            fingerAmount = value;
            fingerCounter.SetAmountText(fingerAmount.ToString());
        }
    }

    public static PrototypeGameSystem Instance => instance;

    public void Awake()
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

    public void Start()
    {
        MoneyAmount = 0;
        FingerAmount = 5;
        costToPlay = 1;
        reelInstances = new List<Reel>();

        foreach (var reelSpinButton in reelSpinButtons)
        {
            reelSpinButton.Initialize();
        }
        betField.ResetText();
        
        CreateReel();
        CreateReel();
        AfterPlayerAction();
    }

    public void CreateReel()
    {
        Reel newReel = new Reel(6, 8, 12);
        reelInstances.Add(newReel);
    }
    
    public void AfterPlayerAction()
    {
        playButtonText.text = $"PLAY\n(${costToPlay})";
        if (moneyAmount < costToPlay)
        { // edge case, out of money
            playButton.interactable = false;

            if (fingerAmount == 0)
            {
                textBox.SetText("Yer outta money and fingers.\n\nGAME OVER!");
                textBox.MoveBox(screenCenter);
                textBox.ToggleVisibility(true);
            }
            else
            {
                textBox.SetText("Yer outta money.\n\nGive me a finger fer more!");
                textBox.MoveBox(screenCenter);
                textBox.ToggleVisibility(true);
                tradeFingerButton.interactable = true;
            }

            return;
        }

        tradeFingerButton.interactable = false;
        if (reelSpinButtons[2].IsLocked)
        {
            // edge case, game start -- need third reel
            textBox.SetText("Ya need at least three reels to play.\n\nBuy one now!");
            textBox.MoveBox(screenCenter + new Vector2(150, 300));
            textBox.ToggleVisibility(true);
            playButton.interactable = false;
            return;
        }

        // Main gameplay
        textBox.ToggleVisibility(false);
        playButton.interactable = true;
    }

    public void OnPlayButtonPressed()
    {
        // 1: Check player's money and subtract if enough
        if (moneyAmount < costToPlay)
        {
            return;    
        }

        MoneyAmount -= costToPlay;
        
        // 2: Spin the reel
        reelsAsBoard = new ReelIcons[5, reelInstances.Count];
        for (int i = 0; i < reelInstances.Count; i++)
        {
            int iconSteps = Random.Range(100, 150);
            reelInstances[i].SpinReel(iconSteps);
            
            // 3: Display results of reel
            List<ReelIcons> reelResults = reelInstances[i].GetIcons(5);
            uiReels[i].DisplayIcons(reelResults);

            for (int y = 0; y < reelResults.Count; y++)
            {
                reelsAsBoard[y,i] = reelResults[y];
            }
        }
        
        // 3: Check for winning combinations
        List<WinningCombinationSO> combinationsToCheck;

        switch (reelInstances.Count)
        {
            case 3:
                combinationsToCheck = SOReferences.Instance.Combinations.ThreeReelCombinations;
                break;
            case 4:
                combinationsToCheck = SOReferences.Instance.Combinations.FourReelCombinations;
                break;
            case 5:
                combinationsToCheck = SOReferences.Instance.Combinations.FiveReelCombinations;
                break;
            default:
                Debug.LogError($"{reelInstances.Count} not supported for checking");
                return;
        }

        int matches = CheckMatches(combinationsToCheck);
        Debug.Log(matches);
        
        // 4: Do something with the matches
        // - Count the icons that matched?
        // - Check icon attributes
        // - Add back to money
        AfterPlayerAction();
    }

    private int CheckMatches(List<WinningCombinationSO> combinationsToCheck)
    {
        int matches = 0;
        
        // iterate across board
        int boardHeight = reelsAsBoard.GetLength(0);
        int boardWidth = reelsAsBoard.GetLength(1);
        for (int boardRow = 0; boardRow < boardHeight; boardRow++)
        for (int boardCol = 0; boardCol < boardWidth; boardCol++)
        {
            // iterate through all combos
            foreach (WinningCombinationSO combo in combinationsToCheck)
            {
                // if there isn't enough space to check this combo, skip
                if (boardRow + combo.Height > boardHeight || boardCol + combo.Width > boardWidth)
                    continue;

                bool isValid = true;
                
                // iterate through all of pattern's squares
                for (int comboRow = 0; comboRow < combo.Height; comboRow++)
                {
                    for (int comboCol = 0; comboCol < combo.Width; comboCol++)
                    {
                        
                        // continue to next square if there is no icon set
                        // in the inspector, the axes are switched
                        if (!combo.Pattern[comboCol, comboRow])
                        {
                            Debug.Log("going next");
                            continue;
                        }

                        // if the board doesn't have an icon, exit loop early
                        // [y, x] = board's top left corner; add u and v to iterate with pattern
                        if (reelsAsBoard[boardRow + comboRow, boardCol + comboCol] == ReelIcons.None)
                        {
                            Debug.Log("invalid");
                            isValid = false;
                            break;
                        }
                    }

                    // leave pattern early if the pattern was broken
                    if (!isValid)
                        break;
                }

                // if the entire pattern was iterated through and remained valid, count
                if (isValid)
                {
                    Debug.Log($"VALID MATCH for {combo.name} at [{boardRow}, {boardCol}]");
                    matches++;
                }
            }
        }

        return matches;
    }
    
    public void OnTradeFingerButtonPressed()
    {
        if (!TrySubtractFingers(1))
            return;

        MoneyAmount += 10;
        AfterPlayerAction();
    }

    public bool TrySubtractMoney(int difference)
    {
        if (moneyAmount >= difference)
        {
            MoneyAmount -= difference;
            return true;
        }

        return false;
    }

    public bool TrySubtractFingers(int difference)
    {
        if (fingerAmount >= difference)
        {
            FingerAmount -= difference;
            return true;
        }

        return false;
    }
}