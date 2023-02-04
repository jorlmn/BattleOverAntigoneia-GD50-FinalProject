using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] Image scoutButton;

    [SerializeField] Image raiderButton;

    [SerializeField] Image freighterButton;

    [SerializeField] Image cruiserButton;

    [SerializeField] Image battleshipButton;

    private Color32 selectedColor = new Color32(175,255,66,255);
    private Color32 unselectedColor = new Color32(255,255,255,255);

    public void OnShipSelection(int index)
    {
        switch (index)
        {
            case 0:
                scoutButton.color = selectedColor;
                raiderButton.color = unselectedColor;
                freighterButton.color = unselectedColor;
                cruiserButton.color = unselectedColor;
                battleshipButton.color = unselectedColor;
                break;
            case 1:
                scoutButton.color = unselectedColor;
                raiderButton.color = selectedColor;
                freighterButton.color = unselectedColor;
                cruiserButton.color = unselectedColor;
                battleshipButton.color = unselectedColor;
                break;
            case 2:
                scoutButton.color = unselectedColor;
                raiderButton.color = unselectedColor;
                freighterButton.color = selectedColor;
                cruiserButton.color = unselectedColor;
                battleshipButton.color = unselectedColor;
                break;
            case 3:
                scoutButton.color = unselectedColor;
                raiderButton.color = unselectedColor;
                freighterButton.color = unselectedColor;
                cruiserButton.color = selectedColor;
                battleshipButton.color = unselectedColor;
                break;
            case 4:
                scoutButton.color = unselectedColor;
                raiderButton.color = unselectedColor;
                freighterButton.color = unselectedColor;
                cruiserButton.color = unselectedColor;
                battleshipButton.color = selectedColor;
                break;
        }
    }

    public void ShipSelection(int index)
    {
        MatchSettings.shipChosenIndex = index;
    }
}
