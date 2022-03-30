using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern
{
    public class Enemy
    {

        protected Transform enemy;

        Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));

        //Enemy FSM���� �����ϴ� ���
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
                    //�÷��̾��� �ݴ�������� �̵��Ѵ�.
                    enemy.Translate(enemy.forward * fleeSpeed * Time.deltaTime);
                    break;
                case EnemyFSM.Stroll:
                    if ( Vector3.Distance(randomPos, enemy.position) < 0.1f)
                    {
                        randomPos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
                    }
                    enemy.rotation = Quaternion.LookRotation(enemy.position - randomPos);
                    //���� �������� ����ؼ� �̵��Ѵ�.
                    enemy.Translate(enemy.forward * strollSpeed * Time.deltaTime); //�������� �̵��Ѵ�. 
                    break;
                case EnemyFSM.MoveTowardsPlayer:
                    //�÷��̾� �������� �̵��Ѵ�.
                    enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, 
                        player.position, attackSpeed * Time.deltaTime);
                    break;
            }
        }
    }
}
