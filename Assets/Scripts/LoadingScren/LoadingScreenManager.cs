using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] private float loadingTimer;
    private float innerLoadingTimer;
    [SerializeField] private List<GameObject> constallations;
    [SerializeField] private List<string> tips;
    [SerializeField] private GameObject loadingBar;
    [SerializeField] private GameObject tipUI;
    [SerializeField] private GameObject fadePanel;
    private string currentTip;



    // Start is called before the first frame update
    void Start()
    {
        innerLoadingTimer = loadingTimer;
        //pick one tip from the list
        int rand = Random.Range(0, tips.Count);
        currentTip = tips[rand];
        tipUI.GetComponent<TextMeshProUGUI>().text = currentTip;
        loadingBar.GetComponent<Image>().fillAmount = 1 - (innerLoadingTimer / loadingTimer);

    }

    // Update is called once per frame
    void Update()
    {
        LoadingTimerTick();

    }

    void LoadingTimerTick()
    {
        if (innerLoadingTimer >= 0)
        {
            innerLoadingTimer -= Time.deltaTime;
            loadingBar.GetComponent<Image>().fillAmount = 1 - (innerLoadingTimer / loadingTimer);
            CheckBarProgress();
        }
        else
        {
            fadePanel.GetComponent<Animator>().SetTrigger("Fade");
            //loadNextScene
        }
    }


    void CheckBarProgress()
    {
        if (constallations[1].activeInHierarchy == false)
        {
            if (loadingBar.GetComponent<Image>().fillAmount >= 0.25)
            {
                constallations[1].SetActive(true);
            }
        }

        if (constallations[2].activeInHierarchy == false)
        {
            if (loadingBar.GetComponent<Image>().fillAmount >= 0.5)
            {
                constallations[2].SetActive(true);
            }
        }

        if (constallations[3].activeInHierarchy == false)
        {
            if (loadingBar.GetComponent<Image>().fillAmount >= 0.75)
            {
                constallations[3].SetActive(true);
            }
        }



    }
}


