using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StateMachineBroke : MonoBehaviour
{
    public GameObject machInfo;
    public Text title;
    public Text description;
    public Text[] stateTexts;
    public GameObject gameInfo;
    public Machine curMachine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curMachine == null)
        {
            machInfo.SetActive(false);
            gameInfo.SetActive(true);
        }
        else
        {
            if (curMachine.jammed)
            {
                title.text = curMachine.machineName + " [JAMMED]";
                title.color = new Color(.85f, 0, 0, 1);
            }
            else
            {
                title.text = curMachine.machineName;
                title.color = Color.white;
            }
            description.text = curMachine.description;
            foreach (Text stext in stateTexts)
            {
                stext.text = "";
            }
            for (int i = 0; i < curMachine.possibleStates.Length; i++)
            {
                if (curMachine.possibleStates[i] == curMachine.state)
                {
                    stateTexts[i].text = "["+curMachine.possibleStates[i]+"]";
                    stateTexts[i].color = Color.yellow;
                }
                else
                {
                    stateTexts[i].text = curMachine.possibleStates[i];
                    stateTexts[i].color = Color.white;
                }
            }
            machInfo.SetActive(true);
            gameInfo.SetActive(false);
        }
    }
}
