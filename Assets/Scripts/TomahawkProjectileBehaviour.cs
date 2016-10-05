using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Tomahawk Projectile", menuName = "Projectiles/Tomahawk", order = 1)]
public class TomahawkProjectileBehaviour : ProjectileBehaviour {
    public float verticalThrust = 10f;
    public float horizontalThrust = 10f;

    Rigidbody2D m_rb;
    Transform m_transform;
    Vector2 m_direction;
    Vector2 m_target;

    float m_thrustMultiplier = 0f;
    bool m_isElevating = false;

    public override void Initialize(GameObject owner) {
        base.Initialize (owner);

        m_rb = owner.GetComponent<Rigidbody2D> ();
        m_transform = owner.transform;
        m_thrustMultiplier = owner.GetComponent<Rocket> ().thrustMultiplier;
        m_target = (GameManager.Instance.Player != null ? (Vector2)GameManager.Instance.Player.transform.position : Vector2.left);

        float xDirection = (m_target.x - m_transform.position.x);
        xDirection = (xDirection > 0f ? 1f : -1f);

        float yDirection = (m_target.y - m_transform.position.y);
        yDirection = (yDirection > 0f ? 1f : -1f);

        m_direction = new Vector2 (xDirection, yDirection);

        // Figure out where to send the projectile
        m_rb.AddForce (new Vector2(0f, m_direction.y) * verticalThrust * m_thrustMultiplier, ForceMode2D.Impulse);
        m_isElevating = true;
        UpdateSpriteDirection (new Vector2(0f, m_direction.y));
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate ();

        if (m_isElevating) {
            if (m_transform.position.y >= m_target.y) {
                m_isElevating = false;
            }
        }
        else {
            m_rb.velocity = new Vector2 (m_direction.x, 0f) * horizontalThrust * m_thrustMultiplier;
        }

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
