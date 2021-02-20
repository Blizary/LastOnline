using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBoxManager : MonoBehaviour
{

    public GameObject generalTextContainer;//original text obj in the world used to render all text
    public GameObject textPrefab;
    public GameObject scrollUpTextButton;
    public GameObject scrollDownTextButton;
    public GameObject UITrash;

    private List<ChatConv> chats;
    private List<List<string>> savedChats;
    private List<int> chatNumb;
    private int currentChat;
    private bool scrolling;

    private int currentChatIndx;
    private FarPersonManager manager;


    private float innerTimer;
    private bool messageFinnish;

    // Start is called before the first frame update
    void Start()
    {
        chats = new List<ChatConv>();
        chatNumb = new List<int>();
        savedChats = new List<List<string>>();
        currentChat = 0;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<FarPersonManager>();
        scrolling = false;
        messageFinnish = false;
        ReadChatLists();
        StartChat();
    }

    // Update is called once per frame
    void Update()
    {
        ShowScrollButtons();

    }

    /// <summary>
    /// reads a new message in the chat
    /// </summary>
    /// <param name="_nextMessage"></param>
    void ReadNextMessage(int _chat, int _message)
    {
        if (chats[_chat].conversation.Count > _message)//check if there are any messages left in this conversation
        {

            for (int i = 0; i < chats[_chat].conversation[_message].chatText.Count; i++)//for each line in this message
            {
                savedChats[_chat].Add(chats[_chat].conversation[_message].chatText[i]); //add text to saved for that chat

                //check if the scroll up is active
                if(!scrolling)
                {
                    GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);//create a new line        
                    if (i == 0)//check if it is the 1st message so the name is added
                    {
                        newText.GetComponent<TextMeshProUGUI>().text = "[" + chats[_chat].conversation[_message].characterName + "] " + chats[_chat].conversation[_message].chatText[i];
                    }
                    else//dont add name
                    {
                        newText.GetComponent<TextMeshProUGUI>().text = chats[_chat].conversation[_message].chatText[i];
                    }
                    chatNumb[0] += 1;//current chat possion

                    CheckOverflow(0);//check if overflow
                }

               
            }



            StartCoroutine(WaitForNextSentence(chats[_chat].conversation[_message].timer, _chat, _message + 1));
        }

    }

    /// <summary>
    /// Reads and coppies all the data available in the manager so we can manipulate them in this script
    /// without damaging the scriptable objs
    /// </summary>
    void ReadChatLists()
    {
        for (int i = 0; i < manager.chatbox.Count; i++)
        {
            chats.Add(manager.chatbox[i]);
        }
    }


    void StartChat()
    {
        for (int i = 0; i < chats.Count; i++)
        {
            StartCoroutine(WaitForNextSentence(chats[i].conversation[0].timer, i, 0));
            List<string> newstrings = new List<string>();
            savedChats.Add(newstrings);
            chatNumb.Add(0);
        }
    }

    IEnumerator WaitForNextSentence(float _timer, int _chat, int _message)
    {
        //wait for timer
        yield return new WaitForSeconds(_timer);
        //read next message
        ReadNextMessage(_chat, _message);
    }

    void CheckOverflow(int _chat)
    {
        Debug.Log("Num of child: " + generalTextContainer.transform.childCount);
        if (generalTextContainer.transform.childCount > 8)
        {
            Debug.Log("destroyed 1 child");
            GameObject trashtext = generalTextContainer.transform.GetChild(0).gameObject;
            trashtext.transform.SetParent(UITrash.transform);
            Destroy(trashtext);

        }
    }

    void ShowScrollButtons()
    {
        if (chatNumb[currentChat] > 8)
        {
            scrollUpTextButton.SetActive(true);
        }
        else
        {
            scrollUpTextButton.SetActive(false);
        }


        if (chatNumb[currentChat] < savedChats[currentChat].Count-1)
        {
            scrollDownTextButton.SetActive(true);
        }
        else
        {
            scrollDownTextButton.SetActive(false);
        }
    }

    public void ScrollUpText()
    {
        scrolling = true;
        //remove i
        GameObject trashtext = generalTextContainer.transform.GetChild(generalTextContainer.transform.childCount-1).gameObject;
        trashtext.transform.SetParent(UITrash.transform);
        Destroy(trashtext);

        chatNumb[0] -= 1;
        //add i-6
        GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);
        newText.GetComponent<TextMeshProUGUI>().text = savedChats[currentChat][chatNumb[0] - 8];
        newText.transform.SetAsFirstSibling();


    }

    public void ScrollDownText()
    {

        //add i+1
        GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);
        newText.GetComponent<TextMeshProUGUI>().text = savedChats[currentChat][chatNumb[0] + 1];

        //remove i-6
        GameObject trashtext = generalTextContainer.transform.GetChild(0).gameObject;
        trashtext.transform.SetParent(UITrash.transform);
        Destroy(trashtext);

        chatNumb[0] += 1;

        //check if the player is on the last available message
        if(chatNumb[0] == savedChats[currentChat].Count-1)
        {
            scrolling = false;
        }
    }

}