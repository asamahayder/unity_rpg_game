using UnityEngine;
using ScriptableObjects.Items.Scripts;

public class TreeBehavior : Interactable
{
    Animator characterAnimator;
    [SerializeField] public ItemObject logs;

    protected override void Start()
    {
        base.Start();
        characterAnimator = GameObject.Find("Character").GetComponentInChildren<Animator>();
    }

    protected override void onInteract()
    {
        characterAnimator.SetBool("isWoodCutting", true);
    }

    protected override void onMouseOver()
    {
        
    }
}
