using UnityEngine;

public class InWorldText : MonoBehaviour
{
    private void OnEnable()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }

}
