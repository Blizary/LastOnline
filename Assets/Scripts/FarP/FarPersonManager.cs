using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarPersonManager : MonoBehaviour
{
    public List<ChatConv> chatbox;
    [SerializeField] private GameObject inventoryOBJ;
    [SerializeField] private string noTargetMessage;
    [SerializeField] private string friendlyTargetMessage;
    [SerializeField] private GameObject warningOBJ;
    [SerializeField] private GameObject targetFrame;
    [SerializeField] private GameObject targetName;
    [SerializeField] private GameObject targetIcon;
    [SerializeField] private GameObject optionsPanel;


    public bool inChat;
    public GameObject player;
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


    public void UpdateTarget(NPC _newTarget)
    {
        targetIcon.GetComponent<Image>().sprite = _newTarget.npcImage;
        targetName.GetComponent<TextMeshProUGUI>().text = _newTarget.npcName;
        targetFrame.SetActive(true);
        player.GetComponent<ThirdPersonMovement>().hasTarget = true;
    }

    public void NoTarget()
    {
        targetFrame.SetActive(false);
        player.GetComponent<ThirdPersonMovement>().hasTarget = false;
    }

    public void ChatStartButton()
    {
        inChat = true;
        optionsPanel.SetActive(true);

    }


    public void CloseChat()
    {
        inChat = false;
        optionsPanel.SetActive(false);
    }
}
