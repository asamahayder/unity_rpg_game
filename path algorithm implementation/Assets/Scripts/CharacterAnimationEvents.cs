using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    CharacterCombat characterCombat;
    Character character;

    // Start is called before the first frame update
    void Start()
    {
        characterCombat = GetComponentInParent<CharacterCombat>();
        character = GetComponentInParent<Character>();
    }


    //The following two functions are called with animation events.
    void onAttack()
    {
        characterCombat.attack();
    }

    void onCollectLogs()
    {
        character.collectLogs();
    }


}
