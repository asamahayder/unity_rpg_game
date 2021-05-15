public class Bob : NPCbehavior
{
    protected override void Start()
    {
        name = "Bob";
        base.Start();
        quest = new QuestCombatTraining(reward);
    }
}
