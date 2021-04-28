using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{


    CharacterCombat characterCombat;

    // Start is called before the first frame update
    void Start()
    {
        characterCombat = GetComponentInParent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onAttack()
    {
        characterCombat.attack();
    }
}
