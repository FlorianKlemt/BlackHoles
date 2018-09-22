using UnityEngine;

namespace _Scripts
{
    /// <summary>
    /// Gravity behavioura added to object
    /// </summary>
    public class Gravity : MonoBehaviour
    {
        public float PullRadius; // Radius to pull
        public float BlackHoleRadius;
        public float MassOutside; // Pull force
        public float MassInside;
        public float Gravitation;
        public float MinRadius = 0.1f; // Minimum distance to pull from
        //public float DistanceMultiplier; // Factor by which the distance affects force
        public Transform player;
        public float fuel_load_distance;

        public LayerMask LayersToPull;
        public float lerp_time;
        private float lerp_value;
        Vector3 lerp_from, lerp_to;

        void Start()
        {
            lerp_value = 0;
            player = GameObject.Find("Player").transform;
        }

        // Function that runs on every physics frame
        void FixedUpdate()
        {
            //fuel reload
            if (Vector3.Distance(player.position, transform.position)<fuel_load_distance)
            {
                player.gameObject.GetComponent<PlayerFuel>().addFuel(2);
            }

            //gravity
            Collider[] colliders = Physics.OverlapSphere(transform.position, PullRadius, LayersToPull);
            foreach (var collider in colliders)
            {            
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb == null)
                    continue; // Can only pull objects with Rigidbody

                Vector3 direction = transform.position - collider.transform.position;

                float distance = direction.magnitude;
                if (distance < MinRadius)
                {
                    continue;
                }
                
                if (distance < BlackHoleRadius) {
                    if (lerp_value == 0)
                    {
                        rb.velocity = new Vector3(0, 0, 0);
                        lerp_from = rb.transform.position;
                        lerp_to = transform.position;
                    }
                    lerp_value += Time.fixedDeltaTime / lerp_time;
                    rb.transform.position = Vector3.Lerp(lerp_from, lerp_to, lerp_value);

                    /*rb.velocity = new Vector3(0,0,0);
                    float force = (Gravitation * MassInside * rb.mass) / (distance * distance);
                    rb.AddForce(direction.normalized*force);*/
                    if (rb.name == "Player")
                    {
                        rb.gameObject.GetComponent<PlayerController>().alive = false;
                    }
                } else {
                    float force = (Gravitation * MassOutside * rb.mass) / (distance * distance);
                    rb.AddForce(direction.normalized*force);
                }
                
            }
        }

    }
}