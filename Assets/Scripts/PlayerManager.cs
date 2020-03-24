using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public int lifeValue = 3;
    public int score = 0;
    public bool isDead = false;
    public bool isDefeat = false;
    public GameObject born;
    public Text ui_tex_score;
    public Text ui_tex_life;
    public GameObject gameOver;

    public static PlayerManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
        gameOver.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ui_tex_life.text = lifeValue.ToString();
        ui_tex_score.text = score.ToString();
        ReBorn();
    }

    //重生
    void ReBorn() {
        if(lifeValue <= 0) {
            isDefeat = true;
        }
        if (isDefeat)
        {
            gameOver.SetActive(true);
            Invoke("ToTitle", 3.0f);
        }
        else {
            if (isDead)
            {
                lifeValue--;
                if (lifeValue > 0)
                {
                    born.GetComponent<Born>().isEnemy = false;
                    Instantiate(born, new Vector3(-10, -8, 0), Quaternion.identity);
                    isDead = false;
                }
            }
        }
    }

    void ToTitle() {
        SceneManager.LoadScene("Title");
    }
}
