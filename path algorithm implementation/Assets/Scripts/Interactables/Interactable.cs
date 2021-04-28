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
    private GameObject floatingTextCanvas;
    //private Text text;
    

    //private bool isMouseOver = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        floatingTextCanvas = GameObject.Find("Floating UI");

        outline = GetComponent<Outline>();
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;
        outline.OutlineMode = outlineMode;
        //outline.enabled = false;
        StartCoroutine(resetMouseOver());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void LateUpdate()
    {
        if (isMouseOver)
        {
            //display text
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
        }

    }

    void IInteractable.onInteract()
    {
        onInteract(); //we do this as a workaround, because child classes can not directly override members of an interface. 
    }

    protected abstract void onInteract();
    protected abstract void onMouseOver();

    protected virtual void displayText()
    {




    }


}
