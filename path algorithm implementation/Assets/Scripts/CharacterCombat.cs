using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;

public class CharacterCombat : MonoBehaviour
{

    //Player Combat Stats
    private float characterHP = 100f;
    private float startCharacterHP;
    private int combatLevel = 0;
    private const float baseAttackEXPNeeded = 100f;
    private float currentCombatEXP = 0f;
    private float currentCombatEXPNeeded = 0f;
    private int attackPower = 0;
    private int attackPowerBonus = 0;
    private int defencePower = 0;
    private int defencePowerBonus = 0;

    
    //Other variables
    public GameObject damageTextPrefab;
    private GameObject damageTextPositionObject;
    private Animator animator;
    public bool isDead = false;

    private GameObject healthBar;
    private GameObject expBar;
    private GameObject levelUI;

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
            float difference = characterHP - value;

            if(difference > 0) //received damage
            {
                float baseDamage = difference; //damage before taken defence into account
                float damage = baseDamage - Random.Range(0, defencePower); //the defencePower stat removes some of the damage taken

                GameObject damageText = Instantiate(damageTextPrefab, damageTextPositionObject.transform);

                damageText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("-" + damage);
                characterHP -= damage;
                print("Character health: " + characterHP);
            }
            else //gained health
            {

            }

            healthBar.GetComponent<Image>().fillAmount = characterHP / startCharacterHP;

        }
        get
        {
            return characterHP;
        }
    }

    public int CombatLevel
    {
        set
        {
            combatLevel = value;
            currentCombatEXPNeeded = baseAttackEXPNeeded + combatLevel * .5f * baseAttackEXPNeeded;
            attackPower = attackPowerBonus + combatLevel * 10;
            defencePower = defencePowerBonus + combatLevel * 10;

            //levelUI.GetComponent<TextMeshPro>().SetText(combatLevel.ToString());


        }
        get
        {
            return combatLevel;
        }
    }

    public float CurrentCombatEXP
    {
        set
        {
            currentCombatEXP = value;


            //update exp bar here

            if (currentCombatEXP >= currentCombatEXPNeeded)
            {
                combatLevel++;
            }
        }
        get
        {
            return currentCombatEXP;
        }
    }

    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startCharacterHP = characterHP;

        damageTextPositionObject = gameObject.transform.Find("DamageTextEmitPosition").gameObject;
       
        GameObject playerInfo = GameObject.Find("Canvas").transform.Find("PlayerInfo").gameObject;
        healthBar = playerInfo.transform.Find("HealthBar").gameObject;
        expBar = playerInfo.transform.Find("EXPBar").gameObject;
        levelUI = playerInfo.transform.Find("Level").gameObject;

        CombatLevel = 1; //important this comes after the ui initialization above.

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
            CurrentCombatEXP += 20;
        }
        
    }



    //this method is called from the CharacterAnimationEvents class. Is called at a certain point in the attack animation.
    public void attack()
    {
        if (targetEnemy != null)
        {
            if(!(targetEnemy.EnemyHP <= 0)) targetEnemy.EnemyHP -= Random.Range(0,attackPower);
            else
            {
                
            }

        }
    }
}
