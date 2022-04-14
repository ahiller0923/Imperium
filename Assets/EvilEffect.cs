using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvilEffect : MonoBehaviour
{
    public TMP_FontAsset creepyFont;
    public TMP_FontAsset normalFont;
    public void SetEffect(bool isEvil = false)
    {
        if(isEvil)
        {
            GetComponentInChildren<Image>().color = new Color32(111, 7, 0, 255);
            foreach (TextMeshProUGUI text in transform.GetChild(1).GetComponentsInChildren<TextMeshProUGUI>())
            {
                //text.color = new Color32(255, 255, 255, 255);
                if(text.gameObject.name != "NPC_Name")
                {
                    text.font = creepyFont;
                }
            }
        }

        else
        {
            GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
            foreach (TextMeshProUGUI text in transform.GetChild(1).GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = new Color32(0, 0, 0, 255);
                text.font = normalFont;
            }
        }
    }
}
