using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject produce_player;
    public int num_of_enemy_types = 2; 
    public GameObject[] produce_enemies;
    public bool isEnemy;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Produce",0.5f);//延时调用Produce()函数
        Destroy(gameObject, 0.5f);//销毁出生特效
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Produce() {
        if (isEnemy)
        {
            int enemy_type = Random.Range(0, num_of_enemy_types);
            Instantiate(produce_enemies[enemy_type], transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(produce_player, transform.position, Quaternion.identity);
        }
    }
}
