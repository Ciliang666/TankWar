using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;//控制移动速度
    private SpriteRenderer sr;
    public Sprite[] sprites;//存储坦克上下左右方向的图片
    public GameObject bullet;
    private Vector3 bullet_euler;
    private float shoot_cd = 0.4f;//设置射击间隔
    public GameObject die_effect;
    private bool isProtected = true;
    private float shield_timer = 3.0f;
    public GameObject shield_effect;
    public AudioClip[] move;
    public AudioSource audio_source;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shield_effect != null && isProtected)
        {
            shield_effect.SetActive(true);
            shield_timer -= Time.deltaTime;
            if (shield_timer <= 0.0f)
            {
                isProtected = false;
                shield_effect.SetActive(false);
            }
        }

    }

    //FixedUpdate():固定物理帧
    //解决了刚体碰撞的抖动（鬼畜）问题：把移动从Update()里放到这里就行了
    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat)
        {
            return;
        }
        Move();
        if (shoot_cd >= 0.4f)//控制射击冷却实践
        {
            Attack();
        }
        else
        {
            shoot_cd += Time.fixedDeltaTime;
        }
    }

    //封装移动方法
    private void Move() {
        float h = Input.GetAxisRaw("Horizontal");//获取用户输入，⬅️方向键h为-1，➡️方向键h为1
        transform.Translate(Vector3.right * h * speed * Time.deltaTime, Space.World);
        if (h < 0)//判断方向，用不同的图片替代当前方向的图片
        {
            sr.sprite = sprites[3];
            bullet_euler = new Vector3(0.0f,0.0f,90.0f);
        }
        else if (h > 0)
        {
            sr.sprite = sprites[1];
            bullet_euler = new Vector3(0.0f, 0.0f, -90.0f);
        }
        if (h != 0)
        {
            audio_source.clip = move[1];
            if (!audio_source.isPlaying)
            {
                audio_source.Play();
            }
        }

        if (h != 0)
        { //解决了同时按下两个方向键可以斜着移动的问题，本质上是判断按键的优先级
            return;
        }
        float v = Input.GetAxisRaw("Vertical");//获取用户输入，⬆️方向键为1，⬇️方向键为-1
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

        if (v != 0)
        {
            audio_source.clip = move[1];
            if (!audio_source.isPlaying)
            {
                audio_source.Play();
            }
        }
        else {
            audio_source.clip = move[0];
            if (!audio_source.isPlaying)
            {
                audio_source.Play();
            }
        }
    }

    //封装攻击方法
    private void Attack() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //实例化子弹，并实现跟随坦克角度旋转，旋转角度为坦克角度+子弹旋转角度
            Instantiate(bullet, transform.position,Quaternion.Euler(transform.eulerAngles+bullet_euler));
            shoot_cd = 0.0f;
        }
    }
}
