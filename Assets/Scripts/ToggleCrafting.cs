using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCrafting : MonoBehaviour
{
    public GameObject craftingUI;
    public void ToggleUI()
    {
        craftingUI.SetActive(!craftingUI.activeSelf);
    }
}
