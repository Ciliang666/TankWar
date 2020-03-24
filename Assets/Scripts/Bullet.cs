using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f; //设置子弹运动速度
    public GameObject explode_effect;
    public Sprite commander_die;
    public bool isEnemyBullet;
    public AudioClip die;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //实现子弹运动
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag) {
            case "Shield":
                if (isEnemyBullet) {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                    
                }
                break;
            case "Tank":
                if (isEnemyBullet) {
                    Destroy(collision.gameObject);
                    Instantiate(explode_effect, collision.transform.position, collision.transform.rotation);
                    Destroy(gameObject);
                    PlayerManager.Instance.isDead = true;
                }
                break;
            case "Enemy":
                if (!isEnemyBullet)
                {
                    Destroy(collision.gameObject);
                    Instantiate(explode_effect, collision.transform.position, collision.transform.rotation);
                    Destroy(gameObject);
                    PlayerManager.Instance.score++;
                }
                break;
            case "BWall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                if (!isEnemyBullet)
                {
                    collision.SendMessage("playHitAudio");
                }
                break;
            case "MWall":
                Destroy(gameObject);
                if (!isEnemyBullet)
                {
                    collision.SendMessage("playHitAudio");
                }
                break;
            case "Commander":
                AudioSource.PlayClipAtPoint(die, collision.transform.position);
                collision.GetComponent<SpriteRenderer>().sprite = commander_die;
                Destroy(gameObject);
                PlayerManager.Instance.isDefeat = true;
                break;
            default:
                break;
        }
    }   
}
