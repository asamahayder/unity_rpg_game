using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{

    EnemyBehavior enemyBehavior;

    // Start is called before the first frame update
    void Start()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onAttack()
    {
        enemyBehavior.attack();
    }
}
