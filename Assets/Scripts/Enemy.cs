using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject bullet;
    public Sprite[] sprites;

    private SpriteRenderer sr;
    private Vector3 bullet_euler;

    private float h, v = -1;
    private float move_timer = 0.0f;
    private float shoot_cd = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot_cd >= 2.0f)
        {
            Attack();
        }
        else {
            shoot_cd += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    //封装移动方法
    private void Move()
    {

        if (move_timer >= 1.0f)
        {//移动四秒换方向
            int rand = Random.Range(0, 8);
            if (rand >= 4)
            {
                v = -1;
                h = 0;
            }
            else if (rand == 3 || rand == 2)
            {
                v = 1;
                h = 0;
            }
            else if (rand == 1)
            {
                v = 0;
                h = 1;
            }
            else
            {
                v = 0;
                h = -1;
            }
            move_timer = 0.0f; //每次转向计时器归零避免鬼畜转向
        }
        else {
            move_timer += Time.fixedDeltaTime;
        }
        transform.Translate(Vector3.right * h * speed * Time.deltaTime, Space.World);
        if (h < 0)//判断方向，用不同的图片替代当前方向的图片
        {
            sr.sprite = sprites[3];
            bullet_euler = new Vector3(0.0f, 0.0f, 90.0f);
        }
        else if (h > 0)
        {
            sr.sprite = sprites[1];
            bullet_euler = new Vector3(0.0f, 0.0f, -90.0f);
        }
        transform.Translate(Vector3.up * v * speed * Time.deltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = sprites[2];
            bullet_euler = new Vector3(0.0f, 0.0f, 180.0f);
        }
        else if (v > 0)
        {
            sr.sprite = sprites[0];
            bullet_euler = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    //封装攻击方法
    private void Attack()
    {
        //实例化子弹，并实现跟随坦克角度旋转，旋转角度为坦克角度+子弹旋转角度
        Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles + bullet_euler));
        shoot_cd = 0.0f;
    }
}
