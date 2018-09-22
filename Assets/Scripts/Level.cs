using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    // Use this for initialization
    public Transform black_hole;
    public Transform white_hole;
    public int xDimension = 50;
    public int yDimension = 50;
    public float minProbability = 0.01f;
    public float maxProbability = 0.2f;
    public Transform player;

    public int[,] map;
    public static Transform[,] transform_map;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        player.position = new Vector3(0, 0, 0);
        //player = Instantiate(player_prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void Start()
    {
        MapGenerator map_generator = new MapGenerator(xDimension,yDimension,minProbability,maxProbability);
        map = map_generator.generate_map();
        transform_map = new Transform[map_generator.width, map_generator.height];
        for(int i=0; i<map_generator.width; i++)
        {
            for (int j = 0; j < map_generator.height; j++)
            {
                if (map[i, j] == 1)
                {
                    Transform b_hole = Instantiate(black_hole, new Vector3(i*100, 0, j*100), Quaternion.identity);
                    transform_map[i, j] = b_hole;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public static Transform[,] get_transform_map()
    {
        return transform_map;
    }
}
