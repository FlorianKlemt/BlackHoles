using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float thrust;
    public float rotate_speed;
    public float damping;
    public Rigidbody player_rb;
    public bool alive;
    public Transform applied_shield_prefab;
    private Transform applied_shield;
    public Transform astroid_explosion_prefab;
    public Transform ship_explosion_prefab;
    private Level level;
    public float speed_boost_duration;
    private float current_speed_boost_duration;
    private bool speedboosted;
    private bool can_shoot;
    public float weapon_duration;
    private float current_weapon_duration;
    public Transform bullet_prefab;
    public float bullet_speed;
    public float shooting_cooldown;
    private float shooting_cooldown_left;

    private PlayerFuel player_fuel;
    private PlayerPowerUpGUIController player_powerup_gui;

    void Start()
    {
        alive = true;
        player_rb = GetComponent<Rigidbody>();
        player_fuel = GetComponent<PlayerFuel>();
        player_powerup_gui = GetComponent<PlayerPowerUpGUIController>();
        level = GameObject.Find("GameController").GetComponent<Level>();
        current_speed_boost_duration = 0;
        speedboosted = false;
        current_weapon_duration = 0;
        can_shoot = false;
        shooting_cooldown_left = 0;
    }

    void FixedUpdate()
    {
        if (alive && !player_fuel.out_of_fuel())
        {   
            player_rb.velocity *= damping;
            if (Input.GetKey(KeyCode.A))
            {
                player_rb.AddForce(Vector3.left * thrust);
                player_fuel.useFuel(100.0f * Time.fixedDeltaTime);
            }
            //player_rb.transform.Rotate(-Vector3.up * rotate_speed * Time.fixedDeltaTime);
            if (Input.GetKey(KeyCode.D))
            {
                player_rb.AddForce(Vector3.right * thrust);
                player_fuel.useFuel(100.0f * Time.fixedDeltaTime);
            }
            //player_rb.transform.Rotate(Vector3.up * rotate_speed * Time.fixedDeltaTime);
            if (Input.GetKey(KeyCode.W))
            {
                player_rb.AddForce(Vector3.forward * thrust);
                player_fuel.useFuel(100.0f * Time.fixedDeltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                player_rb.AddForce(Vector3.back * thrust);
                player_fuel.useFuel(100.0f * Time.fixedDeltaTime);
            }
            if (can_shoot && Input.GetKey(KeyCode.Space))
            {
                shooting_cooldown_left -= Time.fixedDeltaTime;
                if (shooting_cooldown_left <= 0)
                {
                    var bullet = Instantiate(bullet_prefab, transform.position + transform.forward * 40,
                        new Quaternion(90, 0, 0, 1));
                    bullet.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * bullet_speed;
                    shooting_cooldown_left = shooting_cooldown;
                }
            }
            
        }
        if (player_rb.velocity != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(player_rb.velocity, Vector3.up);
        }

        if (speedboosted)
        {
            current_speed_boost_duration += Time.fixedDeltaTime;
            if (current_speed_boost_duration >= speed_boost_duration)
            {
                thrust /= 2;
                current_speed_boost_duration = 0;
                speedboosted = false;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Astroid")
        {
            Instantiate(astroid_explosion_prefab, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Death();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shield")
        {
            applied_shield = Instantiate(applied_shield_prefab, transform.position, new Quaternion(0, 0, 0, 1));
            applied_shield.parent = transform;

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Speed")
        {
            thrust *= 2;
            speedboosted = true;
            Destroy(other.gameObject);

            player_powerup_gui.set_current_upgrade(PlayerPowerUpGUIController.Powerup.SpeedPowerUp);
        }
        else if(other.gameObject.tag == "Weapon")
        {
            Destroy(other.gameObject);
            can_shoot = true;

            player_powerup_gui.set_current_upgrade(PlayerPowerUpGUIController.Powerup.DamagePowerUp);
        }
    }

    public void Death()
    {
        Instantiate(ship_explosion_prefab, transform.position, Quaternion.identity);
        player_rb.velocity = new Vector3(0, 0, 0);
        StartCoroutine(level.Restart(2));
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void get_lost()
    {

    }
}
