using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern
{
    public class Creeper : Enemy
    {
        EnemyFSM creeperMode;

        float health = 100f;

        public Creeper(Transform creeper)
        {
            base.enemy = creeper;
            //몬스터가 배회하는 것에서 시작.
            creeperMode = EnemyFSM.Stroll;
        }


        //Update the creeper's state
        public override void UpdateEnemy(Transform player)
        {
            //The distance between the Creeper and the player
            float distance = (base.enemy.position - player.position).magnitude;

            switch (creeperMode)
            {
                case EnemyFSM.Attack:
                    break;
                case EnemyFSM.Flee:
                    break;
                case EnemyFSM.Stroll:
                    break;
                case EnemyFSM.MoveTowardsPlayer:
                    break;
            }

            //Move the enemy based on a state
            DoAction(player, creeperMode);
        }
    }

}
