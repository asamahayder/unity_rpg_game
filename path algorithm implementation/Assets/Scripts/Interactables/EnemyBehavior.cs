using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehavior : Actor
{

    CharacterCombat characterCombat;
    private bool isInCombat = false;

    [SerializeField] private float health = 100f;
    [SerializeField] private Texture2D cursorImage;

    public GameObject damageTextPrefab;
    private GameObject damageTextPositionObject;
    private float characterHP = 100f;

   


    //TODO: the characterHP logic is copied inside this and CharacterCombat. Implement a better OOP way than this.
    public float CharacterHP
    {
        set
        {
            GameObject damageText = Instantiate(damageTextPrefab, damageTextPositionObject.transform);
            float difference = characterHP - value;
            damageText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("-" + difference);
            characterHP = value;
            print("Enemy Helath: " + CharacterHP);
        }
        get
        {
            return characterHP;
        }
    }

    protected override void onInteract()
    {
        base.onInteract();
        isInCombat = true; //important that this is before startCoroutine
        characterCombat.isInCombat = true;
        characterCombat.TargetEnemy = this;
        StartCoroutine(activateCombat());
      
    }

    protected override void onMouseOver()
    {
        base.onMouseOver();
        //Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);

    }

    protected override void Start()
    {
        damageTextPositionObject = gameObject.transform.Find("DamageTextEmitPosition").gameObject;
        outlineColor = Color.red; //important that this is before base.start
        base.Start();
        print("HEEEEEEEEEEELOOOOOOOOOOOOO i am running!");
        characterCombat = GameObject.Find("Character").GetComponent<CharacterCombat>();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (isMouseOver) Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {
        if(characterHP <= 0)
        {
            onDie();
        }
    }

    void onDie()
    {
        StopCoroutine(activateCombat());
        Destroy(gameObject);
    }


    IEnumerator activateCombat()
    {
        while (true)
        {

            if (isInCombat && !(characterHP <= 0) && !(characterCombat.CharacterHP <= 0f))
            {
                characterCombat.CharacterHP -= 10f;
                yield return new WaitForSeconds(2);
            }
            else
            {
                isInCombat = false;
                yield break;
            }
        }
    }

}
