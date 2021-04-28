using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
   public void DestroyParent()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        Destroy(parent);
    }

    private void Update()
    {
        transform.LookAt(2*transform.position - Camera.main.transform.position);
    }
}
