using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    [SerializeField] private float fadeWarnings;
    private float innerFadeWarnings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WarningTimer();
    }

    void WarningTimer()
    {
        if(innerFadeWarnings>0)
        {
            innerFadeWarnings -= Time.deltaTime;
        }
        else
        {
            //queu fade in the future
            GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void StartTimer()
    {
        innerFadeWarnings = fadeWarnings;
    }


}
