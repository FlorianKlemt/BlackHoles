using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    public enum Tile { Empty, SmallBlackHole, BigBlackHole, Start, Goal, ShieldPowerUp, SpeedPowerUp, DamagePowerUp };

    public int width, height, min_start_pos_offset;
    public float big_hole_prob, power_up_prob;
    
    public MapGenerator(int width, float big_hole_prob, float power_up_prob)
    {
        this.width = width;
        this.height = 1;
        this.big_hole_prob = big_hole_prob;
        this.power_up_prob = power_up_prob;
    }

    List<Vector2Int> generate_black_holes()
    {
        List<Vector2Int> map_holes = new List<Vector2Int>();
        map_holes.Add(new Vector2Int(1, 0));

        int min_y = 0;
        int max_y = 0;
        for (int x = 2; x < width - 1; x++)
        {
            int y_offset = Random.Range(-1, 2);
            int y = map_holes[map_holes.Count - 1].y + y_offset;
            map_holes.Add(new Vector2Int(x, y));

            if (y < min_y)
                min_y = y;
            else if (y > max_y)
                max_y = y;
        }
        //Debug.Log("min_y" + min_y);
        for (int i = 0; i < map_holes.Count; i++)
        {
            int y = map_holes[i].y - min_y + 2;
            //Debug.Log("x: " + map_holes[i].x + " y: " + y);
            map_holes[i] = new Vector2Int(map_holes[i].x, y);
            if (height < y)
                height = y;
        }        

        return map_holes;
    }


    public Tile[,] generate_path_map()
    {
        List<Vector2Int> map_holes = generate_black_holes();        
        height += 2;

        Tile[,] map = new Tile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = Tile.Empty;
            }
        }

        foreach (var hole in map_holes)
        {
            map[hole.x, hole.y] = Tile.BigBlackHole;
            if (Random.Range(0.0f, 1.0f) > power_up_prob)
            {
                int power_index = Random.Range(0, 3);
                Tile power_up = Tile.Empty;
                if (power_index == 0)
                    power_up = Tile.ShieldPowerUp;
                else if (power_index == 1)
                    power_up = Tile.SpeedPowerUp;
                else if (power_index == 2)
                    power_up = Tile.DamagePowerUp;
                else
                    Debug.Log("something went wrong in map generator");
                int y_pos = hole.y + 1;
                if (Random.Range(0.0f, 1.0f) > 0.5f)
                    y_pos = hole.y - 1;
                map[hole.x, y_pos] = power_up;
            }
        }
        map[map_holes[0].x - 1, map_holes[0].y] = Tile.Start;
        map[map_holes[map_holes.Count - 1].x + 1, map_holes[map_holes.Count - 1].y] = Tile.Goal;
        return map;
    }
    /*public Tile[,] generate_path_map()
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
        this.height = max_y - min_y + 1;
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
                int power_index = Random.Range(0, 3);
                Tile power_up = Tile.Empty;
                if (power_index == 0)
                    power_up = Tile.ShieldPowerUp;
                else if (power_index == 1)
                    power_up = Tile.SpeedPowerUp;
                else if (power_index == 2)
                    power_up = Tile.DamagePowerUp;
                else
                    Debug.Log("something went wrong in map generator");
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
    */
}
