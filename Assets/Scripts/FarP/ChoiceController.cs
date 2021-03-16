using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceController : MonoBehaviour
{
    public ChatOption currentOption;
    public bool hasOptions;

    private ChatBoxManager chatManager;

    // Start is called before the first frame update
    void Start()
    {
        chatManager = GameObject.FindGameObjectWithTag("ChatManager").GetComponent<ChatBoxManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasOptions)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }    
    }

    public void ChoiceChoosen()
    {
        chatManager.ChoiceMade(currentOption, currentOption.npcAnwser,currentOption.playerAwnser);
    }

    public void SetValues(ChatOption _newOption)
    {
        currentOption = _newOption;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _newOption.feelingName;
        hasOptions = true;
    }

    public void ClearValues()
    {
        currentOption = null;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        hasOptions = false;
    }
}
