using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{
    TextMeshProUGUI label;
    bank Bank;

    // Start is called before the first frame update
    void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        Bank = FindObjectOfType<bank>();
        label.text = "Money: " + Bank.GetCurrentBalance;
    }

    // Update is called once per frame
    void Update()
    {
        label.text = "Money: " + Bank.GetCurrentBalance;
    }
}
