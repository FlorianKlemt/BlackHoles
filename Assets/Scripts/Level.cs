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

    public int[,] map;

    void Start()
    {
        MapGenerator map_generator = new MapGenerator(xDimension,yDimension,minProbability,maxProbability);
        map = map_generator.generate_map();
        for(int i=0; i<map_generator.width; i++)
        {
            for (int j = 0; j < map_generator.height; j++)
            {
                if (map[i, j] == 1)
                {
                    Instantiate(black_hole, new Vector3(i*100, 0, j*100), Quaternion.identity);
                } else if (map[i, j] == 0) {
                    Instantiate(white_hole, new Vector3(i*100, 0, j*100), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
