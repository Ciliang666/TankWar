using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject[] map_elements;//顺序：0家，1砖墙，2铁墙，3出生效果，4水，5草，6空气墙

    private List<Vector3> usedPositionList = new List<Vector3>();
    public int map_complexity = 20;
    public int enemy_wave = 3;

    void Awake()
    {
        InitMap();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_wave <= 0) {
            CancelInvoke("GenerateEnemy");
        }
    }

    //初始化地图
    void InitMap() {
        //生成 家
        GenerateMapElement(map_elements[0], new Vector3(0.0f, -8.0f, 0.0f), Quaternion.identity);//Quaternion.identity代表无旋转
        GenerateMapElement(map_elements[1], new Vector3(0.0f, -7.0f, 0.0f), Quaternion.identity);
        GenerateMapElement(map_elements[1], new Vector3(1.0f, -7.0f, 0.0f), Quaternion.identity);
        GenerateMapElement(map_elements[1], new Vector3(-1.0f, -7.0f, 0.0f), Quaternion.identity);
        GenerateMapElement(map_elements[1], new Vector3(1.0f, -8.0f, 0.0f), Quaternion.identity);
        GenerateMapElement(map_elements[1], new Vector3(-1.0f, -8.0f, 0.0f), Quaternion.identity);

        //生成 玩家
        map_elements[3].GetComponent<Born>().isEnemy = false;
        GenerateMapElement(map_elements[3], new Vector3(-10.0f, -8.0f, 0.0f), Quaternion.identity);

        //生成 敌人
        map_elements[3].GetComponent<Born>().isEnemy = true;
        GenerateMapElement(map_elements[3], new Vector3(-10.0f, 8.0f, 0.0f), Quaternion.identity);
        GenerateMapElement(map_elements[3], new Vector3(0.0f, 8.0f, 0.0f), Quaternion.identity);
        GenerateMapElement(map_elements[3], new Vector3(10.0f, 8.0f, 0.0f), Quaternion.identity);
        InvokeRepeating("GenerateEnemy", 5, 3);
        

        //生成 其他元素
        GenerateRandomElement();

        //生成 空气墙
        GenerateMapElement(map_elements[6], new Vector3(0.0f, 9.0f, 0.0f), Quaternion.identity);
        GenerateMapElement(map_elements[6], new Vector3(0.0f, -9.0f, 0.0f), Quaternion.identity);
        GenerateMapElement(map_elements[6], new Vector3(-11.0f, 0.0f, 0.0f), Quaternion.Euler(new Vector3(0, 0, 90)));
        GenerateMapElement(map_elements[6], new Vector3(11.0f, 0.0f, 0.0f), Quaternion.Euler(new Vector3(0, 0, 90)));
    }

    //实例化地图元素并将坐标记录到usedPositionList
    void GenerateMapElement(GameObject go, Vector3 v, Quaternion r) {
        GameObject obj = Instantiate(go, v, r);
        obj.transform.SetParent(gameObject.transform);
        usedPositionList.Add(v); 
    }

    //随机生成地图元素
    void GenerateRandomElement() {
        for (int i = 0; i < map_complexity; i++)
        {
            GenerateMapElement(map_elements[1], GenerateRandomPosition(), Quaternion.identity);
            GenerateMapElement(map_elements[2], GenerateRandomPosition(), Quaternion.identity);
            GenerateMapElement(map_elements[4], GenerateRandomPosition(), Quaternion.identity);
            GenerateMapElement(map_elements[5], GenerateRandomPosition(), Quaternion.identity);
        }
    }

    //随机生成坐标
    Vector3 GenerateRandomPosition() {
        while (true) {
            Vector3 pos = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!Positioned(pos))
            {
                return pos;
            }
        }
    }

    //判断是否使用过该坐标
    bool Positioned(Vector3 v) {
        return usedPositionList.Contains(v);
    }

    //周期性生成敌人
    void GenerateEnemy() {
        enemy_wave--;
        map_elements[3].GetComponent<Born>().isEnemy = true;
        int rand = Random.Range(0, 3);
        switch(rand){
            case 0:
                GenerateMapElement(map_elements[3], new Vector3(-10.0f, 8.0f, 0.0f), Quaternion.identity);
                break;
            case 1:
                GenerateMapElement(map_elements[3], new Vector3(0.0f, 8.0f, 0.0f), Quaternion.identity);
                break;
            case 2:
                GenerateMapElement(map_elements[3], new Vector3(10.0f, 8.0f, 0.0f), Quaternion.identity);
                break;
            default:
                break;
        }
    }

}
