using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] Image fighterButton;

    [SerializeField] Image cruiserButton;

    [SerializeField] Image battleshipButton;

    public void OnShipSelection(int index)
    {
        switch (index)
        {
            case 0:
                fighterButton.color = new Color32(175,255,66,255);
                cruiserButton.color = new Color32(255,255,255,255);
                battleshipButton.color = new Color32(255,255,255,255);
                break;
            case 1:
                fighterButton.color = new Color32(255,255,255,255);
                cruiserButton.color = new Color32(175,255,66,255);
                battleshipButton.color = new Color32(255,255,255,255);
                break;
            case 2:
                fighterButton.color = new Color32(255,255,255,255);
                cruiserButton.color = new Color32(255,255,255,255);
                battleshipButton.color = new Color32(175,255,66,255);
                break;
        }
    }
}
