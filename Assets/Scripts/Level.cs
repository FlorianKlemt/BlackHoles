﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    // Use this for initialization
    public Transform big_black_hole;
    public Transform small_black_hole;
    public int map_width = 50;
    public float power_up_probability = 0.2f;
    public float big_hole_prob = 0.3f;
    private Transform player;
    public Transform target;
    public Transform shield_power_up;
    public Transform speed_power_up;
    public Transform damage_power_up;

    private int tile_height = 200;
    private int tile_width = 400;

    public MapGenerator.Tile[,] map;
    public static Transform[,] transform_map;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void Start()
    {
        MapGenerator map_generator = new MapGenerator(map_width,big_hole_prob, power_up_probability);
        //map = map_generator.generate_map();
        map = map_generator.generate_path_map();
        transform_map = new Transform[map_generator.width, map_generator.height];
        float var = tile_width / 8;
        for(int i=0; i<map_generator.width; i++)
        {
            for (int j = 0; j < map_generator.height; j++)
            {
                Vector3 real_pos = new Vector3(i * tile_width, 0, j * tile_height);
                real_pos += new Vector3(Random.Range(-var, var), 0.0f, Random.Range(-var, var));
                if (map[i, j] == MapGenerator.Tile.BigBlackHole)
                {
                    Instantiate(big_black_hole, real_pos, Quaternion.identity);
                }
                else if(map[i,j] == MapGenerator.Tile.SmallBlackHole)
                {
                    Instantiate(small_black_hole, real_pos, Quaternion.identity);
                }
                else if (map[i,j]==MapGenerator.Tile.Start)
                {
                    player.position = real_pos;
                }
                else if (map[i, j] == MapGenerator.Tile.Goal)
                {
                    Instantiate(target, real_pos, Quaternion.identity);
                }
                else if (map[i, j] == MapGenerator.Tile.ShieldPowerUp)
                {
                    Instantiate(shield_power_up, real_pos, Quaternion.identity);
                }
                else if (map[i, j] == MapGenerator.Tile.SpeedPowerUp)
                {
                    Instantiate(speed_power_up, real_pos, Quaternion.identity);
                }
                else if (map[i, j] == MapGenerator.Tile.DamagePowerUp)
                {
                    Instantiate(damage_power_up, real_pos, Quaternion.identity);
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

    public IEnumerator Restart(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
