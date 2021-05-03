using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;

public class EnemyBehavior : Actor
{

    CharacterCombat characterCombat;
    private bool isInCombat = false;

    [SerializeField] private float enemyHP = 100f;
    private float startHP;
    [SerializeField] private Texture2D cursorImage;

    public GameObject damageTextPrefab;
    private GameObject damageTextPositionObject;

    public float attackRange = 3f;

    private GameObject healthBar;
    

    //TODO: the characterHP logic is copied inside this and CharacterCombat. Implement a better OOP way than this.
    public float EnemyHP
    {
        set
        {
            GameObject damageText = Instantiate(damageTextPrefab, damageTextPositionObject.transform);
            float difference = enemyHP - value;
            damageText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("-" + difference);
            enemyHP = value;
            healthBar.GetComponent<Image>().fillAmount = (enemyHP/startHP);
        }
        get
        {
            return enemyHP;
        }
    }

    public bool IsinCombat
    {
        set
        {
            isInCombat = value;
            animator.SetBool("isInCombat", value);
        }
        get
        {
            return isInCombat;
        }
    }


    protected override void onInteract()
    {
        base.onInteract();
        IsinCombat = true; //important that this is before startCoroutine
        if (characterCombat.TargetEnemy == null) characterCombat.TargetEnemy = this;
        else print("You are already fighting an enemy!");
      
    }

    protected override void onMouseOver()
    {
        base.onMouseOver();

    }

    protected override void Start()
    {
        damageTextPositionObject = gameObject.transform.Find("DamageTextEmitPosition").gameObject;
        outlineColor = Color.red; //important that this is before base.start
        base.Start();
        characterCombat = playerCharacter.GetComponent<CharacterCombat>();
        startHP = enemyHP;
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (isMouseOver) Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

    protected override void Update()
    {
        base.Update();
        if(enemyHP <= 0)
        {
            animator.SetBool("isDead", true);
            isInteracting = false;
        }


        if (!animator.GetBool("isDead") && isInCombat && Vector3.Distance(playerCharacter.transform.position, transform.position) > attackRange)
        {
            pathfinder.findPath(playerCharacter.transform.position, true);
            
        }


        Rect followArea = new Rect(pathfinder.x1, pathfinder.z1, 2 * pathfinder.roamingRadius, 2 * pathfinder.roamingRadius);
        Vector2 position2D = new Vector2(transform.position.x, transform.position.z);
        //if i am outside my roaming zone, return to center of roaming zone
        if (IsinCombat && !followArea.Contains(position2D) && !animator.GetBool("isDead"))
        {
            onEndInteraction();
            pathfinder.findPath(pathfinder.startPosition, false);
        }


    }


    //In an enemy's case, this is called when combat has been initiated, but then canceled when player runs away.
    protected override void onEndInteraction()
    {
        base.onEndInteraction();
        IsinCombat = false;
        characterCombat.TargetEnemy = null;
        
    }

    public void onDie()
    {
        Destroy(gameObject);
    }
    
    

    public void attack()
    {
        if (isInCombat && !(enemyHP <= 0) && !(characterCombat.CharacterHP <= 0f) && Vector3.Distance(playerCharacter.transform.position, transform.position) <= 3)
        {
            characterCombat.CharacterHP -= 10f;
            
        }
        else
        {
            IsinCombat = false;
            objectMover.clearPath();
            
        }
    }

    public bool isInsideRoamingZone()
    {
        return true;
    }

    protected override void setupInfoUI()
    {
        infoUI = gameObject.transform.Find("EnemyInfo").gameObject;
        healthBar = infoUI.transform.Find("HealthBarBackground").gameObject.transform.Find("HealthBar").gameObject;
    }

}
