using UnityEngine;
using System.Collections;

public class ThrowAnimation : MonoBehaviour
{
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        StartCoroutine(ShootStar());
    }

    IEnumerator ShootStar()
    {
        while (GameplayManager.instance.locIndex < 4)
        {
            yield return new WaitUntil(() => GameplayManager.instance.callShootingStar == true);

            //turn on kinematic again so seed follows falling physics
            Projectile.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            GameplayManager.instance.planted = false;
            GameplayManager.instance.playedChime = false;

            // Move projectile to the position of throwing object + add some offset if needed.
            Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);
            Projectile.gameObject.GetComponent<Renderer>().enabled = true; // controlled in PlantSeed.cs
            Projectile.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            // Short delay added before Projectile is thrown
            yield return new WaitForSeconds(1.0f);

            // Calculate distance to target
            float target_Distance = Vector3.Distance(Projectile.position, Target.position);

            // Calculate the velocity needed to throw the object to the target at specified angle.
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

            // Extract the X  Y componenent of the velocity
            float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

            // Calculate flight time.
            float flightDuration = target_Distance / Vx;

            // Rotate projectile to face the target.
            Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

            float elapse_time = 0;

            while (elapse_time < flightDuration)
            {
                Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

                elapse_time += Time.deltaTime;

                yield return null;

            }

            //need to turn off isKinematic so it follows game's gravity and can drop down when planted
            Projectile.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            GameplayManager.instance.callShootingStar = false;
            GameplayManager.instance.callGodRay = true;

        }
    }
}