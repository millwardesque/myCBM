using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Dumb Projectile", menuName = "Projectiles/Dumb", order = 1)]
public class DumbProjectileBehaviour : ProjectileBehaviour {
    public float initialThrust = 10f;

    Rigidbody2D m_rb;
    Transform m_transform;

    public override void Initialize(GameObject owner) {
        base.Initialize (owner);

        m_rb = owner.GetComponent<Rigidbody2D> ();
        m_transform = owner.transform;

        // Figure out where to send the projectile
        Vector2 startDirection = owner.GetComponent<Rocket> ().startDirection;
        float thrustMultiplier =  owner.GetComponent<Rocket> ().thrustMultiplier;

        m_rb.AddForce (startDirection * initialThrust * thrustMultiplier, ForceMode2D.Impulse);
        UpdateSpriteDirection (startDirection);
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate ();

        if (m_rb != null) {
            UpdateSpriteDirection (m_rb.velocity);    
        }
    }

    void UpdateSpriteDirection(Vector2 direction) {
        if (m_rb != null && m_transform != null) {
            // Rotate the sprite / collider to face the direction of motion.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            m_transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);    
        }
    }
}
