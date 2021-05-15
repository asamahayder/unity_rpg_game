public class Peter : NPCbehavior
{
    protected override void Start()
    {
        name = "Peter";
        base.Start();
        quest = new QuestWoodCutting(reward);
    }
}
