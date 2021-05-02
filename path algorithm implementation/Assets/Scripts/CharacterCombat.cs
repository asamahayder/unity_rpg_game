using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCombat : MonoBehaviour
{

    private float characterHP = 100f;

    public GameObject damageTextPrefab;
    private GameObject damageTextPositionObject;
    private Animator animator;
    public bool isDead = false;

    private EnemyBehavior targetEnemy;
    
    public EnemyBehavior TargetEnemy
    {
        set
        {
            targetEnemy = value;
            if (value != null)
            {
                isInCombat = true;
            }
            else
            {
                isInCombat = false;
            }
        }
        get
        {
            return targetEnemy;
        }
    }

    public bool isInCombat
    {
        set
        {
            animator.SetBool("isInCombat", value);
        }
        get
        {
            return animator.GetBool("isInCombat");
        }
    }

    public float CharacterHP
    {
        set
        {
            GameObject damageText = Instantiate(damageTextPrefab, damageTextPositionObject.transform);
            float difference = characterHP - value;
            damageText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("-" + difference);
            characterHP = value;
            print("Character health: " + characterHP);
        }
        get
        {
            return characterHP;
        }
    }

    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        damageTextPositionObject = gameObject.transform.Find("DamageTextEmitPosition").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(characterHP <= 0f && !isDead)
        {
            isDead = true;
            isInCombat = false;
            print("character is DEAD!");
        }

        if(targetEnemy != null && targetEnemy.EnemyHP <= 0)
        {
            targetEnemy = null;
            isInCombat = false;
        }
        
    }



    //this method is called from the CharacterAnimationEvents class. Is called at a certain point in the attack animation.
    public void attack()
    {
        if (targetEnemy != null)
        {
            if(!(targetEnemy.EnemyHP <= 0)) targetEnemy.EnemyHP -= 15;
            else
            {
                
            }

        }
    }
}
