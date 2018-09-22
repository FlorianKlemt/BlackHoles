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

        public LayerMask LayersToPull;

        // Function that runs on every physics frame
        void FixedUpdate()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, PullRadius, LayersToPull);

            foreach (var collider in colliders)
            {            
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb == null)
                    continue; // Can only pull objects with Rigidbody

                Vector3 direction = transform.position - collider.transform.position;

                float distance = direction.magnitude;
                if (distance < MinRadius)
                    continue;
                
                if (distance < BlackHoleRadius) {
                    rb.velocity = new Vector3(0,0,0);
                    float force = (Gravitation * MassInside * rb.mass) / (distance * distance);
                    rb.AddForce(direction.normalized*force);
                } else {
                    float force = (Gravitation * MassOutside * rb.mass) / (distance * distance);
                    rb.AddForce(direction.normalized*force);
                }
                
            }
        }

    }
}