﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FarPersonManager : MonoBehaviour
{
    public List<ChatConv> chatbox;
    [SerializeField] private GameObject inventoryOBJ;
    [SerializeField] private string noTargetMessage;
    [SerializeField] private string friendlyTargetMessage;
    [SerializeField] private GameObject warningOBJ;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Copies the chats into list to be used during the game
    /// </summary>
    void CopyChats()
    {

    }


    public void AbilityClicked()
    {
        if(player.GetComponent<ThirdPersonMovement>().hasTarget)
        {
            string oldText = warningOBJ.GetComponent<TextMeshProUGUI>().text;
            warningOBJ.GetComponent<TextMeshProUGUI>().text = friendlyTargetMessage;
            warningOBJ.GetComponent<TextMeshProUGUI>().text += "\n";
            warningOBJ.GetComponent<TextMeshProUGUI>().text += oldText;
            warningOBJ.GetComponent<WarningController>().StartTimer();
        }
        else
        {
            string oldText = warningOBJ.GetComponent<TextMeshProUGUI>().text;
            warningOBJ.GetComponent<TextMeshProUGUI>().text = noTargetMessage;
            warningOBJ.GetComponent<TextMeshProUGUI>().text += "\n" ;
            warningOBJ.GetComponent<TextMeshProUGUI>().text += oldText;
            warningOBJ.GetComponent<WarningController>().StartTimer();
        }
    }
}
