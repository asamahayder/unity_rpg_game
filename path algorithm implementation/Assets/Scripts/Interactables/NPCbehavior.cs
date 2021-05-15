using ScriptableObjects.Items.Scripts;

public class NPCbehavior : Actor
{

    public ItemObject Reward;
    protected ItemObject reward; //workaround for protection

    DialogueTrigger dialogueTrigger;
    QuestManager questManager;
    
    protected Quest quest;

    protected override void Start()
    {
        base.Start();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        questManager = playerCharacter.GetComponent<QuestManager>();
        reward = Reward;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void onInteract()
    {
        base.onInteract();
        dialogueTrigger.triggerDialogue();
    }

    protected override void onMouseOver()
    {
        base.onMouseOver();
    }

    protected override void onEndInteraction()
    {
        base.onEndInteraction();
        questManager.addToQuestList(quest);
    }

    public void endInteraction() //used as a workaround to the fact that onEndInteraction is protected.
    {
        onEndInteraction();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

}
