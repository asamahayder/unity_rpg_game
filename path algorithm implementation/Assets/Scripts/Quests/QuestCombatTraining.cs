using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ScriptableObjects.Items.Scripts;


public class QuestCombatTraining : Quest
{

    private int enemiesKilled;
    private int enemiesToKill;
    
    public QuestCombatTraining(ItemObject reward) : base(reward)
    {
        questTitel = "Combat Training";
    }

    protected override void Start()
    {
        base.Start();
        questDescription = "Kill 5 enemies";
        enemiesKilled = 0;
        enemiesToKill = 5;
        CharacterCombat.onEnemyKilled += incrementKillCount;

        if (questUI != null)
        {
            questUITitel.GetComponent<TextMeshProUGUI>().text = questTitel;
        }
    }

    protected override void checkProgress()
    {
        base.checkProgress();

        if (questFinished)
        {
            return;
        }

        if(questUI != null)
        {
            questUIProgress.GetComponent<TextMeshProUGUI>().text = makeProgressString();
        }

        if(enemiesKilled == enemiesToKill)
        {
            Debug.Log("Quest Finished!");
            endQuest();
        }
    }

    protected override string makeProgressString()
    {
        string progressString = "Enemies killed: " + enemiesKilled + "/" + enemiesToKill;
        return progressString;
    }

    private void incrementKillCount()
    {
        enemiesKilled++;
        if (!questFinished)
        {
            Debug.Log("Incrementing killcount. Left to kill: " + (enemiesToKill - enemiesKilled));
        }
    }

    

}
