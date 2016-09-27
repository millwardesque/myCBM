using UnityEngine;
using System.Collections;

public class PlayerRocketLauncherController : MonoBehaviour {
    public float minThrust = 10f;
    public float maxThrust = 20f;
    public float thrustChargeTime = 1f;

    float m_currentThrust = 0f;

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
            m_currentThrust = minThrust;
        }
        else if (Input.GetMouseButton(0)) { // Charge the rocket
            if (m_currentThrust < maxThrust) {
                m_currentThrust += Time.deltaTime * (maxThrust - minThrust) / thrustChargeTime;
                m_currentThrust = Mathf.Clamp (m_currentThrust, minThrust, maxThrust);
            }
        }
        else if (Input.GetMouseButtonUp (0)) {   // Fire the rocket
            m_launcher.rocketThrust = m_currentThrust;
            m_launcher.FireRocket ();

            // Reset the thrust immediately.
            m_launcher.rocketThrust = minThrust;
        }
    }
}
