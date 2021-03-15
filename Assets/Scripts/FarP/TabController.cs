using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    private ChatBoxManager chatManager;
    public int tabnum;
    // Start is called before the first frame update
    void Start()
    {
        chatManager = GameObject.FindGameObjectWithTag("ChatManager").GetComponent<ChatBoxManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPress()
    {
        chatManager.TabsButton(tabnum);
    }
}
