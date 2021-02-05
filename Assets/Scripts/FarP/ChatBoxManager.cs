using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBoxManager : MonoBehaviour
{

    public GameObject textExample;//original text obj in the world used to render all text

    private int currentChatIndx;
    private FarPersonManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<FarPersonManager>();
        //for test only
        for (int i = 0; i < manager.chatbox[0].conversation.Count; i++)
        {
            ReadNextMessage(manager.chatbox[0].conversation[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// reads a new message in the chat
    /// </summary>
    /// <param name="_nextMessage"></param>
    void ReadNextMessage(ChatText _nextMessage)
    {
        GameObject newText = Instantiate(textExample, transform);
        newText.GetComponent<TextMeshProUGUI>().text = "[" + _nextMessage.characterName + "] " + _nextMessage.chatText;
       
    }

    /// <summary>
    /// Allows to show previous messages that no longer show in the text
    /// </summary>
    void BackTrackMessage()
    {

    }

    /// <summary>
    /// Moves the chat forward showing new messages
    /// </summary>
    void ForwardMessage()
    {

    }
}
