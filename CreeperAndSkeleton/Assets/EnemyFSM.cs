using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    // Start is called before the first frame update


    protected Transform enemy;

    Vector3 randomPos;

    protected enum EnemyState
    {
        Attack,
        Flee,
        Stroll,
        MoveTowardsPlayer
    }

    EnemyState enemyMode;


    [SerializeField]
    float health = 100f;
    public Transform player;
    public bool chase=false;
    public bool attack=false;

    void Start()
    {
        //현재 사용하고 있는 object로 지정.
        enemy = gameObject.transform;
        enemyMode = EnemyState.Stroll;
        randomPos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
    }

    // Update is called once per frame
    void Update()
    {
        //The distance between the Creeper and the player

        switch (enemyMode)
        {
            case EnemyState.Attack:
                if (health <= 20f)
                {
                    enemyMode = EnemyState.Flee;
                }
                else if (!attack)
                {
                    enemyMode = EnemyState.MoveTowardsPlayer;
                }
                break;
            case EnemyState.Flee:
                if (health >= 60f)
                {
                    enemyMode = EnemyState.Stroll;
                }
                break;
            case EnemyState.Stroll:
                if (chase)
                {
                    enemyMode = EnemyState.MoveTowardsPlayer;
                }
                break;
            case EnemyState.MoveTowardsPlayer:
                if (!chase)
                {
                    enemyMode = EnemyState.Stroll;
                }
                else if (attack)
                {
                    enemyMode = EnemyState.Attack;
                }
                break;
        }

        //Move the enemy based on a state
        DoAction(player, enemyMode);

    }


    void DoAction(Transform player, EnemyState enemyMode)
    {
        float fleeSpeed = 10f;
        float strollSpeed = 1f;
        float attackSpeed = 5f;

        switch (enemyMode)
        {
            case EnemyState.Attack:
                break;
            case EnemyState.Flee:
                enemy.rotation = Quaternion.LookRotation(enemy.position - player.position);
                //플레이어의 반대방향으로 이동한다.
                enemy.Translate(enemy.forward * fleeSpeed * Time.deltaTime);
                break;
            case EnemyState.Stroll:
                if (Vector3.Distance(randomPos, enemy.position) < 0.1f)
                {
                    randomPos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
                }
                enemy.rotation = Quaternion.LookRotation(enemy.position - randomPos);
                //랜덤 방향으로 계속해서 이동한다.
                enemy.Translate(enemy.forward * strollSpeed * Time.deltaTime); //정면으로 이동한다. 
                break;
            case EnemyState.MoveTowardsPlayer:
                //플레이어 방향으로 이동한다.
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position,
                    player.position, attackSpeed * Time.deltaTime);
                break;
        }
    }


    //ChaseBox가 감지될 경우 쫓는 상태가 가능하다.
    //Player가 감지될 경우 공격이 가능하다.
    private void OnTriggerStay(Collider other)
    {
        if(other.name=="ChaseBox")
        {
            chase = true;
        }
        if(other.name=="Player")
        {
            attack = true;
        }
    }

    //collider 밖으로 벗어나면 상태를 해제해준다.
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "ChaseBox")
        {
            chase = false;
        }
        if (other.name == "Player")
        {
            attack = false;
        }
    }
}





