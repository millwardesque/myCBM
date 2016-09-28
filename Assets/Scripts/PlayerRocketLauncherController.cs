using UnityEngine;
using System.Collections;

public class PlayerRocketLauncherController : MonoBehaviour {
    public float minThrustMultiplier = 0.5f;
    public float maxThrustMultiplier = 1f;
    public float thrustChargeTime = 1f;

    float m_currentThrustMultiplier = 0f;

    RocketLauncher m_launcher;

    void Awake() {
        m_launcher = GetComponent<RocketLauncher> ();
    }

    void Update() {
        Vector2 mouseDirection = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector2 direction = mouseDirection - (Vector2)transform.position;
        float firingAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        m_launcher.FiringAngle = firingAngle;

        // @TODO Restrict to 0 <=> 180 degrees relative to parent.

        if (Input.GetMouseButtonDown (0)) {
            m_currentThrustMultiplier = minThrustMultiplier;
        }
        else if (Input.GetMouseButton(0)) { // Charge the projectile
            if (m_currentThrustMultiplier < maxThrustMultiplier) {
                m_currentThrustMultiplier += Time.deltaTime * (maxThrustMultiplier - minThrustMultiplier) / thrustChargeTime;
                m_currentThrustMultiplier = Mathf.Clamp (m_currentThrustMultiplier, minThrustMultiplier, maxThrustMultiplier);
            }
        }
        else if (Input.GetMouseButtonUp (0)) {   // Fire the rocket
            m_launcher.thrustMultiplier = m_currentThrustMultiplier;
            m_launcher.FireRocket ();

            // Reset the thrust immediately.
            m_launcher.thrustMultiplier = minThrustMultiplier;
        }
    }
}
