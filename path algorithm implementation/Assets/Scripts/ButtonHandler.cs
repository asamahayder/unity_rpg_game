using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject chestScreen;
    public void CloseChest()
    {
        Destroy(chestScreen.gameObject);
    }
}
