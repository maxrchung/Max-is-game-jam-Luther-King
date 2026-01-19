using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReel : MonoBehaviour
{
    [SerializeField] private Image bgImage;
    [Space] [SerializeField] private List<Image> iconImages;

    public void DisplayIcons(List<ReelIcons> icons)
    {
        if (icons.Count != 5)
        {
            Debug.LogError($"Icons size is not 5, it is {icons.Count}");
        }

        for (int i = 0; i < icons.Count; i++)
        {
            iconImages[i].sprite = SOReferences.Instance.Icons.Values[icons[i]].iconSprite;
        }
    }
    
    public void DisplayIcons(Sprite[] icons)
    {
        if (icons.Length != 5)
        {
            Debug.LogError($"Icons size is not 5, it is {icons.Length}");
        }

        for (int i = 0; i < icons.Length; i++)
        {
            iconImages[i].sprite = icons[i];
        }
    }
}
