using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    public enum Tile { Empty, SmallBlackHole, BigBlackHole, Start, Goal, ShieldPowerUp, SpeedPowerUp, DamagePowerUp };

    public int width, height, min_start_pos_offset;
    public float max_distance, min_blackhole_prob, max_blackhole_prob, blackhole_prob_range, big_hole_prob, power_up_prob;
    public MapGenerator(int width, int height, float min_blackhole_prob, float max_blackhole_prob,
                        float big_hole_prob, float power_up_prob)
    {
        this.width = width;
        this.height = height;
        this.max_distance = new Vector2(width, height).magnitude;
        this.min_blackhole_prob = min_blackhole_prob;
        this.max_blackhole_prob = max_blackhole_prob;
        this.blackhole_prob_range = max_blackhole_prob - min_blackhole_prob;
        this.big_hole_prob = big_hole_prob;
        this.power_up_prob = power_up_prob;
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

    public Tile[,] generate_path_map()
    {
        List<Vector2Int> map_holes = new List<Vector2Int>();
        map_holes.Add(new Vector2Int(1, 0));

        int min_y = 0;
        int max_y = 0;
        for(int x = 2; x < width-1; x++)
        {
            int y_offset = Random.Range(-1, 2);
            int y = map_holes[map_holes.Count - 1].y + y_offset;
            map_holes.Add(new Vector2Int(x, y));

            if (y < min_y)
                min_y = y;
            else if (y > max_y)
                max_y = y;
        }

        min_y -= 1;
        max_y += 1;
        height = max_y - min_y + 1;
        Tile[,] map = new Tile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = Tile.Empty;
            }
        }

        foreach(var hole in map_holes)
        {
            int y = hole.y - min_y;
            map[hole.x, hole.y - min_y] = Tile.BigBlackHole;
            if(Random.Range(0.0f, 1.0f) > power_up_prob)
            {
                Tile power_up = Tile.ShieldPowerUp;
                if (Random.Range(0.0f, 1.0f) > 0.5f)
                    power_up = Tile.SpeedPowerUp;
                int y_pos = y + 1;
                if (Random.Range(0.0f, 1.0f) > 0.5f)
                    y_pos = y - 1;
                map[hole.x, y_pos] = power_up;
            }
        }
        map[map_holes[0].x -1, map_holes[0].y - min_y] = Tile.Start;
        map[map_holes[map_holes.Count - 1].x + 1, map_holes[map_holes.Count - 1].y - min_y] = Tile.Goal;
        return map;
    }

}
