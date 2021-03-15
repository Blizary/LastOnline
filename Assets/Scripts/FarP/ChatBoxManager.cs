using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBoxManager : MonoBehaviour
{
    public GameObject tabsContainer;
    public GameObject tabsPrefab;
    public GameObject generalTextContainer;//original text obj in the world used to render all text
    public GameObject textPrefab;
    public GameObject scrollUpTextButton;
    public GameObject scrollDownTextButton;
    public GameObject UITrash;

    public int maxNumOfMessages;//maximun amount of shown messages at 1 time
    public float inactivityChatTimer;// amount of time after witch the chat resets to the latest messages
    private float innerInactivityChatTimer;

    public List<Tab> tabs;


    private int currentChat;
    private bool scrolling;
    private int currentChatIndx;
    private FarPersonManager manager;


    private float innerTimer;
    private bool messageFinnish;

    // Start is called before the first frame update
    void Start()
    {
        tabs = new List<Tab>();

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
       InactiveChatTimer();

    }

    /// <summary>
    /// reads a new message in the chat
    /// </summary>
    /// <param name="_nextMessage"></param>
    void ReadNextMessage(int _chat, int _message)
    {
        if (tabs[_chat].tabChat.Count > _message)//check if there are any messages left in this conversation
        {


            for (int i = 0; i < tabs[_chat].tabChat[_message].chatText.Count; i++)//for each line in this message
            {
                if (i == 0)//check if it is the 1st message so the name is added
                {
                    tabs[_chat].displayedText.Add("[" + tabs[_chat].tabChat[_message].characterName + "] " + tabs[_chat].tabChat[_message].chatText[i]);
                }
                else//dont add name
                {
                    tabs[_chat].displayedText.Add(tabs[_chat].tabChat[_message].chatText[i]);
                }


                //check if the scroll up is active
                if(!scrolling && currentChat ==_chat)
                {
                    GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);//create a new line        
                    if (i == 0)//check if it is the 1st message so the name is added
                    {
                        newText.GetComponent<TextMeshProUGUI>().text = "[" + tabs[_chat].tabChat[_message].characterName + "] " + tabs[_chat].tabChat[_message].chatText[i];
                    }
                    else//dont add name
                    {
                        newText.GetComponent<TextMeshProUGUI>().text = tabs[_chat].tabChat[_message].chatText[i];
                    }
                    tabs[_chat].currentText += 1;
                    //chatNumb[0] += 1;//current chat possion

                    CheckOverflow(0);//check if overflow
                }

               
            }



            StartCoroutine(WaitForNextSentence(tabs[_chat].tabChat[_message].timer, _chat, _message + 1));
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
            Tab newTab = new Tab();
            //get the name
            if(manager.chatbox[i].chatType==ChatType.chatPublic)
            {
                newTab.tabName = "General";
                GameObject newtabDisplay = Instantiate(tabsPrefab, tabsContainer.transform);//create a new tab    
            }
            else
            {
                newTab.tabName = manager.chatbox[i].conversation[0].characterName;
            }

            //get the conversation
            newTab.tabChat = manager.chatbox[i].conversation;

            //set type
            newTab.type = manager.chatbox[i].chatType;

            //start at 0 chat
            newTab.currentText = 0;
            //inicialize lists
            newTab.displayedText = new List<string>();
            newTab.tabNum = i;

            tabs.Add(newTab);


        }
    }


    void StartChat()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            StartCoroutine(WaitForNextSentence(tabs[i].tabChat[0].timer, i, 0));
        }
    }

    IEnumerator WaitForNextSentence(float _timer, int _chat, int _message)
    {
        //wait for timer
        yield return new WaitForSeconds(_timer);
        //check if it is a new tab
        if (tabs[_chat].type == ChatType.chatPrivate)
        {
            if(_message == 0)
            {
                GameObject newtabDisplay = Instantiate(tabsPrefab, tabsContainer.transform);//create a new tab  
                newtabDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tabs[_chat].tabName;
                newtabDisplay.GetComponent<TabController>().tabnum = tabs[_chat].tabNum;
            }
        }

        //read next message
        ReadNextMessage(_chat, _message);
    }

    void CheckOverflow(int _chat)
    {

        if (generalTextContainer.transform.childCount > 8)
        {
            GameObject trashtext = generalTextContainer.transform.GetChild(0).gameObject;
            trashtext.transform.SetParent(UITrash.transform);
            Destroy(trashtext);

        }
    }

    void ShowScrollButtons()
    {
        if (tabs[currentChat].currentText > maxNumOfMessages)
        {
            scrollUpTextButton.SetActive(true);
        }
        else
        {
            scrollUpTextButton.SetActive(false);
        }


        if (tabs[currentChat].currentText < tabs[currentChat].displayedText.Count - 1)
        {
            scrollDownTextButton.SetActive(true);
        }
        else
        {
            scrollDownTextButton.SetActive(false);
        }
    }


    void ActivatedChat()
    {
        innerInactivityChatTimer = inactivityChatTimer;//triggers the timer
        scrolling = true;
    }

    void ResetInactivityChatTimer()
    {
        innerInactivityChatTimer = 0;
        scrolling = false;
    }

    void InactiveChatTimer()
    {
        if (scrolling)
        {
            if (innerInactivityChatTimer > 0)
            {
                innerInactivityChatTimer -= Time.deltaTime;//ticking time
            }
            else// reset the chat
            {
                
                //clear all messages
              foreach(Transform text in generalTextContainer.transform)
                {
                    GameObject.Destroy(text.gameObject);

                }
              
                //add lastest messages
                for (int i = 0; i < maxNumOfMessages; i++)
                {
                    GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);
                    newText.GetComponent<TextMeshProUGUI>().text = tabs[currentChat].displayedText[tabs[currentChat].displayedText.Count - (maxNumOfMessages-i)];
                    tabs[currentChat].currentText = tabs[currentChat].displayedText.Count - 1;
                }
              
                scrolling = false;
            }
        }
        
    }



    public void ScrollUpText()
    {
        
        ActivatedChat();
        //remove i
        GameObject trashtext = generalTextContainer.transform.GetChild(generalTextContainer.transform.childCount-1).gameObject;
        trashtext.transform.SetParent(UITrash.transform);
        Destroy(trashtext);

        tabs[currentChat].currentText -= 1;
        //add i-6
        GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);
        newText.GetComponent<TextMeshProUGUI>().text = tabs[currentChat].displayedText[tabs[currentChat].currentText - maxNumOfMessages];
        newText.transform.SetAsFirstSibling();


    }

    public void ScrollDownText()
    {

        //add i+1
        GameObject newText = Instantiate(textPrefab, generalTextContainer.transform);
        newText.GetComponent<TextMeshProUGUI>().text = tabs[currentChat].displayedText[tabs[currentChat].currentText + 1];

        //remove top message
        GameObject trashtext = generalTextContainer.transform.GetChild(0).gameObject;
        trashtext.transform.SetParent(UITrash.transform);
        Destroy(trashtext);

        tabs[currentChat].currentText += 1;

        //check if the player is on the last available message
        if(tabs[currentChat].currentText == tabs[currentChat].displayedText.Count-1)
        {
            scrolling = false;
            ResetInactivityChatTimer();
        }
    }


    public void TabsButton(int _tabNum)
    {
        currentChat = _tabNum;
        Debug.Log(currentChat);
    }

}