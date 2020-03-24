using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    private int opt = 1;
    public GameObject opt_1;
    public GameObject opt_2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Choice();
    }

    void Choice() {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            opt *= -1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            opt *= -1;
        }

        if (opt == 1)
        {
            transform.position = opt_1.transform.position;
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene("Game");
            }
        }
        else
        {
            transform.position = opt_2.transform.position;
        }
    }


}
