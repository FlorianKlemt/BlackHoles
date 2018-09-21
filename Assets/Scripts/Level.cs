using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    // Use this for initialization
    public Transform black_hole;
    void Start()
    {
        MapGenerator map_generator = new MapGenerator(50,50,0.01f,0.2f);
        int[,] map = map_generator.generate_map();
        for(int i=0; i<map_generator.width; i++)
        {
            for (int j = 0; j < map_generator.height; j++)
            {
                if (map[i, j] == 1)
                {
                    Instantiate(black_hole, new Vector3(i, 0, j), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
