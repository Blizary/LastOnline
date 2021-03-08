using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginScreenManager : MonoBehaviour
{

    [SerializeField] private string username;
    [SerializeField] private GameObject usernameOBJ;
    [SerializeField] private float usernameTypingSpeed;
    private float innerUsernameTypingSpeed;
    private bool userDone;
    private int currentUserLetter;

    [SerializeField] private int passwordSize;
    [SerializeField] private GameObject passwordOBJ;
    [SerializeField] private float passwordTypingSpeed;
    private float innerPasswordTypingSpeed;
    private bool typingDone;

    [SerializeField] private GameObject fadePanel;

    // Start is called before the first frame update
    void Start()
    {
        currentUserLetter = 0;
        userDone = false;
        typingDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!typingDone)
        {
            Typing();
        }
    }

    void Typing()
    {
        if(!userDone)
        {
            //type out user
            if(innerUsernameTypingSpeed>0)
            {
                innerUsernameTypingSpeed -= Time.deltaTime;
            }
            else
            {
                if(currentUserLetter< username.Length)
                {
                    usernameOBJ.GetComponent<TextMeshProUGUI>().text += username[currentUserLetter];
                    currentUserLetter += 1;
                    innerUsernameTypingSpeed = usernameTypingSpeed;
                }
                else
                {
                    userDone = true;
                    currentUserLetter = 0;
                }     
            }
        }
        else
        {
            if(innerPasswordTypingSpeed>0)
            {
                innerPasswordTypingSpeed -= Time.deltaTime;
            }
            else
            {
                if(currentUserLetter<passwordSize)
                {
                    passwordOBJ.GetComponent<TextMeshProUGUI>().text += "*";
                    currentUserLetter += 1;
                    innerPasswordTypingSpeed = passwordTypingSpeed;
                }
                else
                {
                    typingDone = true;
                    //fadePanel.GetComponent<Animator>().SetTrigger("Fade");
                    //show play button?
                }
            }
        }
    }


}
