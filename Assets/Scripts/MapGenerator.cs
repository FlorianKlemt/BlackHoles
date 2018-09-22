using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator
{
    public int width, height;
    public float max_distance, min_blackhole_prob, max_blackhole_prob, blackhole_prob_range;
    System.Random random;
    public MapGenerator(int width, int height, float min_blackhole_prob, float max_blackhole_prob)
    {
        this.width = width;
        this.height = height;
        this.max_distance = new Vector2(width, height).magnitude;
        this.min_blackhole_prob = min_blackhole_prob;
        this.max_blackhole_prob = max_blackhole_prob;
        this.blackhole_prob_range = max_blackhole_prob - min_blackhole_prob;
        this.random = new System.Random();
    }

    public int[,] generate_map()
    {
        //TODO generate start and end
        var start_pos = new Vector2(1, 1);
        var target_pos = new Vector2(width - 1, height - 1);

        int[,] map = new int[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float distance_to_goal = Vector2.Distance(new Vector2(i, j), target_pos);
                float normalized_distance_to_goal = distance_to_goal / max_distance;

                float probability = min_blackhole_prob 
                                    + blackhole_prob_range * normalized_distance_to_goal;
                if(random.NextDouble() < probability){
                    //is black hole
                    map[i,j] = 1;
                }
                else
                {
                    map[i, j] = 0;
                }
            }
        }
        return map;
    }

}
