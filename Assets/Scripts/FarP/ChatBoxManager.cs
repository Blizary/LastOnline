using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBoxManager : MonoBehaviour
{

    public GameObject generalTextContainer;//original text obj in the world used to render all text

    private List<List<ChatText>> chats;

    private int currentChatIndx;
    private FarPersonManager manager;

    private float innerTimer;
    private bool messageFinnish;

    // Start is called before the first frame update
    void Start()
    {
        chats = new List<List<ChatText>>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<FarPersonManager>();
        messageFinnish = false;
        ReadChatLists();
        StartChat();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// reads a new message in the chat
    /// </summary>
    /// <param name="_nextMessage"></param>
    void ReadNextMessage(int _chat, int _message)
    {
        generalTextContainer.GetComponent<TextMeshProUGUI>().text += " \n[" + chats[_chat][_message].characterName + "] " + chats[_chat][_message].chatText;
        StartCoroutine( WaitForNextSentence(chats[_chat][_message].timer, _chat, _message+1));
    }

    /// <summary>
    /// Reads and coppies all the data available in the manager so we can manipulate them in this script
    /// without damaging the scriptable objs
    /// </summary>
    void ReadChatLists()
    {
        for(int i = 0; i<manager.chatbox.Count;i++)
        {

            List<ChatText> newchat = new List<ChatText>();
            for(int j = 0; j<manager.chatbox[i].conversation.Count;j++)
            {
                newchat.Add(manager.chatbox[i].conversation[j]);
            }
            chats.Add(newchat);
        }
    }


    void StartChat()
    {
        for(int i=0;i<chats.Count;i++)
        {
            StartCoroutine(WaitForNextSentence(chats[i][0].timer, i, 0));
        }
    }

    IEnumerator WaitForNextSentence(float _timer,int _chat, int _message)
    {
        //wait for timer
        yield return new WaitForSeconds(_timer);
        //read next message
        ReadNextMessage(_chat, _message);
    }

}
