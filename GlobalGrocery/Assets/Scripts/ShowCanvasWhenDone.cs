using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvasWhenDone : MonoBehaviour
{
    public GameObject infoPanel;

    private void Start()
    {
        if (!infoPanel.activeSelf)
        {
            bool checkUSA = PlayerHistory.USA != null && PlayerHistory.USA.Count > 0;
            bool checkCHINA = PlayerHistory.CHINA != null && PlayerHistory.CHINA.Count > 0;
            bool checkMEXICO = PlayerHistory.MEXICO != null && PlayerHistory.MEXICO.Count > 0;

            if (checkUSA && checkCHINA && checkMEXICO)
            {
                infoPanel.SetActive(true);
            }
        }
    }
}
