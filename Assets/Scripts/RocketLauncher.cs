using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {
    public Rocket prefabRocket;
    public Team team;
    public Transform barrelTip;

    [Header("Rocket Direction")]
    public float angleVariance = 10f;

    [Header("Rocket Force")]
    public float rocketThrust = 10f;
    public float rocketThrustVariance = 2f;

    int m_rocketsLaunched = 0;

    float m_firingAngle;
    public float FiringAngle {
        get { return m_firingAngle; }
        set {
            m_firingAngle = value;
            transform.rotation = Quaternion.AngleAxis(m_firingAngle, Vector3.forward);
        }
    }

    public Rocket FireRocket() {
        Rocket newRocket = Instantiate<Rocket> (prefabRocket);
        newRocket.transform.position = (barrelTip != null ? barrelTip.position : this.transform.position);
        newRocket.name = "Rocket " + m_rocketsLaunched;
        newRocket.team = team;

        float angle = FiringAngle;
        newRocket.startDirection = Quaternion.AngleAxis (angle, Vector3.forward) * Vector3.right;

        float thrust = rocketThrust;
        thrust += Random.Range (-rocketThrustVariance, rocketThrustVariance);
        newRocket.thrust = thrust;

        m_rocketsLaunched++;

        return newRocket;
    }
}
