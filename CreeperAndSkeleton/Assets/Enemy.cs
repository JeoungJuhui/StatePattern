using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern
{
    public class Enemy
    {

        protected Transform enemy;

        Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));

        //Enemy FSM에서 제공하는 기능
        protected enum EnemyFSM 
        {
            Attack,
            Flee,
            Stroll,
            MoveTowardsPlayer
        }

        //Update the enemy by giving it a new state
        public virtual void UpdateEnemy(Transform player)
        {

        }


        //Do something based on a state
        protected void DoAction(Transform player, EnemyFSM enemyMode)
        {
            float fleeSpeed = 10f;
            float strollSpeed = 1f;
            float attackSpeed = 5f;

            switch (enemyMode)
            {
                case EnemyFSM.Attack:
                    break;
                case EnemyFSM.Flee:
                    enemy.rotation = Quaternion.LookRotation(enemy.position - player.position);
                    //플레이어의 반대방향으로 이동한다.
                    enemy.Translate(enemy.forward * fleeSpeed * Time.deltaTime);
                    break;
                case EnemyFSM.Stroll:
                    if ( Vector3.Distance(randomPos, enemy.position) < 0.1f)
                    {
                        randomPos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
                    }
                    enemy.rotation = Quaternion.LookRotation(enemy.position - randomPos);
                    //랜덤 방향으로 계속해서 이동한다.
                    enemy.Translate(enemy.forward * strollSpeed * Time.deltaTime); //정면으로 이동한다. 
                    break;
                case EnemyFSM.MoveTowardsPlayer:
                    //플레이어 방향으로 이동한다.
                    enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, 
                        player.position, attackSpeed * Time.deltaTime);
                    break;
            }
        }
    }
}
