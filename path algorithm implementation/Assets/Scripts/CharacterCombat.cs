using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;

public class CharacterCombat : MonoBehaviour
{

    //Player Combat Stats and variables
    private float currentCharacterHP = 100f;
    private float baseCharacterHP;
    private int combatLevel;
    private const float baseAttackEXPNeeded = 100f;
    private float currentCombatEXP = 0f;
    private float currentCombatEXPNeeded = 0f;
    private int attackPower;
    private int attackPowerBonus;
    private int defencePower;
    private int defencePowerBonus;

    private int defencePowerGainOnNewLevel = 4;
    private int attackPowerGainOnNewLevel = 4;
    private int baseCharacterHPGainOnNewLevel = 10;

    public delegate void OnEnemyKilled();
    public static event OnEnemyKilled onEnemyKilled;

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


    public int AttackPowerBonus
    {
        set
        {
            attackPowerBonus = value;
            attackPower = attackPowerBonus + combatLevel * attackPowerGainOnNewLevel;
            print("HEEEEEEEEY!!!!!!!!!!!!!!!!!!!");
        }
        get
        {
            return attackPowerBonus;
        }
    }

    public float CharacterHP
    {
        set
        {
            float difference = currentCharacterHP - value;

            if(difference >= 0) //received damage
            {
                float baseDamage = difference; //damage before taken defence into account
                float defencePowerBonus = Random.Range(0, defencePower);
                float damage = baseDamage - defencePowerBonus; //the defencePower stat removes some of the damage taken

                if (damage < 0) damage = 0; //otherwise, if defencePower becomes too strong, you will gain health when getting damage.

                print("damage before defencePower: " + baseDamage);
                print("defencePowerBonus: " + defencePowerBonus);

                GameObject damageText = Instantiate(damageTextPrefab, damageTextPositionObject.transform);

                damageText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("-" + damage);
                currentCharacterHP -= damage;
                print("Character health: " + currentCharacterHP);
            }
            else //gained health
            {
                currentCharacterHP = value;
            }

            healthBar.GetComponent<Image>().fillAmount = currentCharacterHP / baseCharacterHP;

        }
        get
        {
            return currentCharacterHP;
        }
    }

    public int CombatLevel
    {
        set
        {
            combatLevel = value;
            currentCombatEXPNeeded = baseAttackEXPNeeded + combatLevel * .5f * baseAttackEXPNeeded;
            attackPower = attackPowerBonus + combatLevel * attackPowerGainOnNewLevel;
            defencePower = defencePowerBonus + combatLevel * defencePowerGainOnNewLevel;
            baseCharacterHP += baseCharacterHPGainOnNewLevel;
            CharacterHP = baseCharacterHP;

            levelUI.GetComponent<TextMeshProUGUI>().SetText(combatLevel.ToString());
        }
        get
        {
            return combatLevel;
        }
    }

    public int[] UpdateCombatBonuses(int attackBonus, int defenceBonus)
    {
        print("UPDATING POWEEEEEEEEER WITH: " + attackBonus);
        defencePower += defenceBonus;
        AttackPowerBonus += attackBonus;
        defencePowerBonus += defenceBonus;
        return new[] {attackPower, defencePower};
    }

    public float CurrentCombatEXP
    {
        set
        {
            currentCombatEXP = value;

            while (currentCombatEXP >= currentCombatEXPNeeded)
            {
                currentCombatEXP -= currentCombatEXPNeeded;
                CombatLevel++;
            }
            expBar.GetComponent<Image>().fillAmount = currentCombatEXP / currentCombatEXPNeeded;
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
        baseCharacterHP = currentCharacterHP;

        damageTextPositionObject = gameObject.transform.Find("DamageTextEmitPosition").gameObject;
       
        GameObject playerInfo = GameObject.Find("Canvas").transform.Find("PlayerInfo").gameObject;
        healthBar = playerInfo.transform.Find("HealthBar").gameObject;
        expBar = playerInfo.transform.Find("EXPBar").gameObject;
        levelUI = playerInfo.transform.Find("Level").gameObject;

        //important these comes after the ui initialization above.
        attackPowerBonus += 50;
        attackPower = 0;
        defencePowerBonus += 0;
        defencePower = 0;
        CombatLevel = 1; 
        CurrentCombatEXP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print("ATTACK POWEEEEEEER: " + attackPower);

        if(currentCharacterHP <= 0f && !isDead)
        {
            isDead = true;
            isInCombat = false;
            print("character is DEAD!");
        }

        if(targetEnemy != null && targetEnemy.EnemyHP <= 0)
        {
            if(onEnemyKilled != null)
            {
                onEnemyKilled();
            }
            targetEnemy = null;
            isInCombat = false;
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
