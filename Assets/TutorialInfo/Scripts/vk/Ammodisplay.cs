using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class Ammodisplay : MonoBehaviour
{
    public static Ammodisplay Instance { get; set; }

    //UI
    public TextMeshProUGUI ammoDisplay;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }
}
