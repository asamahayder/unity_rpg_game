using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ScriptableObjects.Items.Scripts;


public class QuestWoodCutting : Quest
{

    private int logsGathered;
    private int logsToGather;
    
    
    public QuestWoodCutting(ItemObject reward) : base(reward)
    {
        questTitel = "WoodCutting Training";
    }

    protected override void Start()
    {
        //questTitel = "WoodCutting Training";
        base.Start();
        questDescription = "Gather 5 logs";
        logsGathered = 0;
        logsToGather = 5;
        Character.onLogGathered += incrementLogCount;

        if (questUI != null)
        {
            questUITitel.GetComponent<TextMeshProUGUI>().text = questTitel;
        }
    }

    protected override void checkProgress()
    {
        base.checkProgress();

        if(questUI != null)
        {
            questUIProgress.GetComponent<TextMeshProUGUI>().text = makeProgressString();
        }

        if(logsGathered == logsToGather)
        {
            Debug.Log("Quest Finished!");
            endQuest();
        }
    }

    protected override string makeProgressString()
    {
        string progressString = "Logs gathered: " + logsGathered + "/" + logsToGather;
        return progressString;
    }

    private void incrementLogCount()
    {
        logsGathered++;
        if (!questFinished)
        {
            Debug.Log("Incrementing LogCount. Left to gather: " + (logsToGather - logsGathered));
        }
    }

    

}
