using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputRemapping : MonoBehaviour
{
    public TextMeshProUGUI currentlySelectedKeyText { get; set; }
    public bool selectingKeyText { get; set; }
    void Update()
    {
        DetectKeyCode();
    }

    public void DetectKeyCode()
    {
        if (selectingKeyText)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                currentlySelectedKeyText.text = KeyCode.LeftControl.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentlySelectedKeyText.text = KeyCode.LeftShift.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                currentlySelectedKeyText.text = KeyCode.CapsLock.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                currentlySelectedKeyText.text = KeyCode.Tab.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                currentlySelectedKeyText.text = KeyCode.LeftAlt.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                currentlySelectedKeyText.text = KeyCode.Space.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.AltGr))
            {
                currentlySelectedKeyText.text = KeyCode.AltGr.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.RightControl))
            {
                currentlySelectedKeyText.text = KeyCode.RightControl.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.RightShift))
            {
                currentlySelectedKeyText.text = KeyCode.RightShift.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentlySelectedKeyText.text = KeyCode.UpArrow.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentlySelectedKeyText.text = KeyCode.DownArrow.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentlySelectedKeyText.text = KeyCode.LeftArrow.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentlySelectedKeyText.text = KeyCode.RightArrow.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Minus))
            {
                currentlySelectedKeyText.text = KeyCode.Minus.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.KeypadEquals))
            {
                currentlySelectedKeyText.text = KeyCode.Minus.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                currentlySelectedKeyText.text = KeyCode.A.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                currentlySelectedKeyText.text = KeyCode.B.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                currentlySelectedKeyText.text = KeyCode.C.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                currentlySelectedKeyText.text = KeyCode.D.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                currentlySelectedKeyText.text = KeyCode.E.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                currentlySelectedKeyText.text = KeyCode.F.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                currentlySelectedKeyText.text = KeyCode.G.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                currentlySelectedKeyText.text = KeyCode.H.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                currentlySelectedKeyText.text = KeyCode.I.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                currentlySelectedKeyText.text = KeyCode.J.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                currentlySelectedKeyText.text = KeyCode.K.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                currentlySelectedKeyText.text = KeyCode.L.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                currentlySelectedKeyText.text = KeyCode.M.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                currentlySelectedKeyText.text = KeyCode.N.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                currentlySelectedKeyText.text = KeyCode.O.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                currentlySelectedKeyText.text = KeyCode.P.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                currentlySelectedKeyText.text = KeyCode.Q.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                currentlySelectedKeyText.text = KeyCode.R.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currentlySelectedKeyText.text = KeyCode.S.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                currentlySelectedKeyText.text = KeyCode.T.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                currentlySelectedKeyText.text = KeyCode.U.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                currentlySelectedKeyText.text = KeyCode.V.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                currentlySelectedKeyText.text = KeyCode.W.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                currentlySelectedKeyText.text = KeyCode.X.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                currentlySelectedKeyText.text = KeyCode.Y.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                currentlySelectedKeyText.text = KeyCode.Alpha0.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentlySelectedKeyText.text = KeyCode.Alpha1.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentlySelectedKeyText.text = KeyCode.Alpha2.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentlySelectedKeyText.text = KeyCode.Alpha3.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentlySelectedKeyText.text = KeyCode.Alpha4.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentlySelectedKeyText.text = KeyCode.Alpha5.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                currentlySelectedKeyText.text = KeyCode.Alpha6.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                currentlySelectedKeyText.text = KeyCode.Alpha7.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                currentlySelectedKeyText.text = KeyCode.Alpha8.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                currentlySelectedKeyText.text = KeyCode.Alpha9.ToString();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                selectingKeyText = false;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                selectingKeyText = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                selectingKeyText = false;
            }
        }
    }
}