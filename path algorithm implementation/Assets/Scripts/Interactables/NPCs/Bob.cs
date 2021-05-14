using ScriptableObjects.Items.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bob : NPCbehavior
{
    protected override void Start()
    {
        name = "Bob";
        base.Start();
        quest = new QuestCombatTraining(reward);

    }
}
