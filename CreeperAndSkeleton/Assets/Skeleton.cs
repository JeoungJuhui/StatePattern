using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class Skeleton : Enemy
    {
        EnemyFSM skeletonMode = EnemyFSM.Stroll;

        [SerializeField]
        float health = 100f;

        public Skeleton(Transform skeletonObj)
        {
            base.enemy = skeletonObj;
        }


        //Update the creeper's state
        public override void UpdateEnemy(Transform player)
        {
            //The distance between the Creeper and the player
            float distance = (base.enemy.position - player.position).magnitude;

            switch (skeletonMode)
            {
                case EnemyFSM.Attack:
                    if (health <= 20f)
                    {
                        skeletonMode = EnemyFSM.Flee;
                    }
                    else if (distance >= 6f)
                    {
                        skeletonMode = EnemyFSM.MoveTowardsPlayer;
                    }
                    break;
                case EnemyFSM.Flee:
                    break;
                case EnemyFSM.Stroll:
                    if (distance <= 10f)
                    {
                        skeletonMode = EnemyFSM.MoveTowardsPlayer;
                    }
                    break;
                case EnemyFSM.MoveTowardsPlayer:
                    if (distance <= 5f)
                    {
                        skeletonMode = EnemyFSM.Attack;
                        break;
                    }
                    else if (distance >= 15f)
                    {
                        skeletonMode = EnemyFSM.Stroll;
                    }
                    break;
            }

            //Move the enemy based on a state
            DoAction(player, skeletonMode);
        }
    }
}