using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour {

    public Transform astroid;

    // Use this for initialization
    public float speed, life_time, spawn_cooldown;
    float time_since_last = 0;
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        time_since_last += Time.deltaTime;
        if (time_since_last>=spawn_cooldown)
        {
            //TODO set dynamic spawn location
            spawn_asteroid(new Vector2Int(10, 10));
            time_since_last = 0;
        }
	}

    void spawn_asteroid(Vector2Int current_indizes)
    {
        Transform[,] areas = Level.get_transform_map();

        Vector2Int spawn_area_indizes = select_random_spawn_area(current_indizes);
        Transform spawn_area_tranform = areas[spawn_area_indizes.x, spawn_area_indizes.y];
        Vector2 spawn_point = select_random_point_in_area(spawn_area_tranform);

        Vector2 random_direction = Random.insideUnitCircle.normalized;

        Transform new_astroid = Instantiate(astroid, new Vector3(spawn_point.x, 0, spawn_point.y),
                                            Quaternion.identity);
        new_astroid.GetComponent<Rigidbody>().velocity 
            = new Vector3(random_direction.x, 0, random_direction.y) * speed;
        Destroy(new_astroid.gameObject, life_time);
    }

    Vector2 select_random_point_in_area(Transform area)
    {
        return new Vector2(area.position.x, area.position.z) + Random.insideUnitCircle;
    }

    //returns map indizes of selected spawn area
    Vector2Int select_random_spawn_area(Vector2Int player_indizes)
    {
        Vector2Int[] validChoices = { new Vector2Int(-1,-1),new Vector2Int(-1, 0),new Vector2Int(-1, 1),
                new Vector2Int(0,-1),new Vector2Int(0,1),
                new Vector2Int(1,-1),new Vector2Int(1,0),new Vector2Int(1,1)};
        Vector2Int spawn_offset = validChoices[Random.Range(0, validChoices.Length)];
        return player_indizes + spawn_offset;
    }
}
