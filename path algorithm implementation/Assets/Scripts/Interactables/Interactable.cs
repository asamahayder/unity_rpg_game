using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{

    //cursor image variable
    Outline outline;
    protected Color outlineColor = Color.yellow;
    protected float outlineWidth = 3f;
    protected Outline.Mode outlineMode = 0; //outline all
    protected bool isMouseOver = false;
    protected GameObject infoUI;
    
    [SerializeField] protected Texture2D cursorImage;

    [SerializeField] private Texture2D cursorImage;

    protected virtual void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        setupInfoUI();
        infoUI.SetActive(false);
        outline = GetComponent<Outline>();
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;
        outline.OutlineMode = outlineMode;
        //outline.enabled = false;
        StartCoroutine(resetMouseOver());
    }

    
    protected virtual void Update()
    {
        
    }

    protected virtual void LateUpdate()
    {
        if (isMouseOver)
        {
            showUI();
            Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
        }
    }


    void IInteractable.onMouseOver()
    {
        isMouseOver = true;
        outline.enabled = true;
        
        onMouseOver(); //we do this as a workaround, because child classes can not directly override members of an interface. 

    }

    IEnumerator resetMouseOver()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            outline.enabled = false;
            isMouseOver = false;
            hideUI();
        }

    }

    void IInteractable.onInteract()
    {
        onInteract(); //we do this as a workaround, because child classes can not directly override members of an interface. 
    }

    protected abstract void onInteract();
    protected abstract void onMouseOver();

    protected virtual void showUI()
    {
        infoUI.SetActive(true);
    }

    protected virtual void hideUI()
    {
        infoUI.SetActive(false);
    }

    protected virtual void setupInfoUI()
    {
        infoUI = gameObject.transform.Find("GeneralInfo").gameObject;
        infoUI.transform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>().SetText(name);

        

    }


}
