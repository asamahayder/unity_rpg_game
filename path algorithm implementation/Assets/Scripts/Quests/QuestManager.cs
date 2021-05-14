using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    private List<Quest> questList;
    public GameObject scrollView;
    public GameObject questUIPrefab;

    // Start is called before the first frame update
    void Start()
    {
        questList = new List<Quest>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Quest quest in questList)
        {
            quest.update();
            if (quest.getQuestFinished())
            {
                Destroy(quest.getQuestUI());
            }

        }
    }

    public void addToQuestList(Quest quest)
    {

        print("name of quest: " + quest.getQuestTitel());

        foreach (Quest q in questList)
        {
            if (quest.getQuestTitel().Equals(q.getQuestTitel()))
            {
                return; //Skip if quest has already been added.
            }
        }

        print("Adding quest!");
        questList.Add(quest);
        GameObject questUI = Instantiate(questUIPrefab);
        questUI.transform.parent = scrollView.transform;
        quest.setQuestUI(questUI); //important that this line is before quest.start()
        quest.start();
        
    }
}
