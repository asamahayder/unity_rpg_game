using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects.Items.Scripts;

public abstract class Quest
{
    protected string questTitel;
    protected string questDescription;
    protected string questProgress;
    protected bool questFinished;
    protected GameObject questUI;
    protected GameObject questUITitel;
    protected GameObject questUIProgress;
    protected ItemObject reward;

    protected Quest(ItemObject reward)
    {
        this.reward = reward;
    }

    protected virtual void Start()
    {
        questFinished = false;
        //questTitel = "";
        questDescription = "";
        questProgress = "";
    }

    protected virtual void Update()
    {
        if(!questFinished) checkProgress();
    }

    public void update() //workaround to make the protected update method public
    {
        Update();
    }

    public void start() //workaround to make the protected update method public
    {
        Start();
    }

    public void setQuestUI(GameObject uiObject)
    {
        questUI = uiObject;
        questUITitel = uiObject.transform.Find("QuestName").gameObject;
        questUIProgress = uiObject.transform.Find("QuestProgress").gameObject;
    }

    protected virtual void endQuest()
    {
        questFinished = true;
        GameObject.Find("Character").GetComponent<Character>().playerInventoryObject.AddItemToInventorySlot(reward,500);
        
    }

    protected virtual void checkProgress()
    {

    }

    protected virtual string makeProgressString()
    {
        return "";
    }


    //GETTERS

    public bool getQuestFinished()
    {
        return questFinished;
    }

    public GameObject getQuestUI()
    {
        return questUI;
    }

    public string getQuestTitel()
    {
        return questTitel;
    }

    public ItemObject getReward()
    {
        return reward;
    }
}
