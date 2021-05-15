using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{

    EnemyBehavior enemyBehavior;

    // Start is called before the first frame update
    void Start()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
    }

    public void onAttack()
    {
        enemyBehavior.attack();
    }
}
