using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern
{
    public class GameController : MonoBehaviour //컴포넌트, 기본적으로 제공되는 기능 (Start, Update)
    {
        [SerializeField]
        GameObject player;

        [SerializeField]
        GameObject creeper;

        [SerializeField]
        GameObject skeleton;

        List<Enemy> enemies = new List<Enemy>();

        // Start is called before the first frame update
        void Start()
        {
            //각 객체를 직접 인스턴스로 만들어 리스트에 넣는다.
            enemies.Add(new Creeper(creeper.transform));
            enemies.Add(new Skeleton(skeleton.transform));
        }

        // Update is called once per frame
        void Update()
        {
            for (int i=0; i < enemies.Count; i++)
            {
                enemies[i].UpdateEnemy(player.transform);
            }

        }
    }


}
