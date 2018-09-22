using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    public enum Tile { Empty, SmallBlackHole, BigBlackHole, Start, Goal };

    public int width, height, min_start_pos_offset;
    public float max_distance, min_blackhole_prob, max_blackhole_prob, blackhole_prob_range, big_hole_prob;
    public MapGenerator(int width, int height, float min_blackhole_prob, float max_blackhole_prob,
                        float big_hole_prob)
    {
        this.width = width;
        this.height = height;
        this.max_distance = new Vector2(width, height).magnitude;
        this.min_blackhole_prob = min_blackhole_prob;
        this.max_blackhole_prob = max_blackhole_prob;
        this.blackhole_prob_range = max_blackhole_prob - min_blackhole_prob;
        this.big_hole_prob = big_hole_prob;
    }

    public Vector2Int get_start_pos()
    {
        return new Vector2Int(Random.Range(0+min_start_pos_offset, width-min_start_pos_offset),
                           Random.Range(0+min_start_pos_offset, height+min_start_pos_offset));
    }

    //target is spawned on the opposite side as the player
    public Vector2Int get_target_pos(Vector2Int start_pos)
    {
        return new Vector2Int(width - start_pos.x, height - start_pos.y);
    }

    public Tile[,] generate_map()
    {

        Tile[,] map = new Tile[width, height];
        //safety init default
        for(int i=0;i<width;i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i,j] = Tile.Empty;
            }
        }

        var start_pos = get_start_pos();
        var target_pos = get_target_pos(start_pos);
        map[start_pos.x, start_pos.y] = Tile.Start;
        map[target_pos.x, target_pos.y] = Tile.Goal;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //avoid blackholes at start or goal
                if(map[i,j]==Tile.Start || map[i, j] == Tile.Goal)
                {
                    continue;
                }

                float distance_to_start = Vector2.Distance(new Vector2(i, j), start_pos);
                float normalized_distance_to_start = distance_to_start / max_distance;

                float probability = min_blackhole_prob 
                                    + blackhole_prob_range * normalized_distance_to_start;
                if(Random.Range(0.0f,1.0f) < probability){
                    //is black hole
                    if (Random.Range(0.0f, 1.0f) < big_hole_prob)
                    {
                        map[i, j] = Tile.BigBlackHole;
                    }
                    else
                    {
                        map[i, j] = Tile.SmallBlackHole;
                    }
                }
                else
                {
                    map[i, j] = Tile.Empty;
                }
            }
        }
        return map;
    }

}
