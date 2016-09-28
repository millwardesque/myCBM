using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {
    public Rocket prefabRocket;
    public Team team;
    public Transform barrelTip;

    public ProjectileBehaviour[] rocketBehaviours;

    [Header("Rocket Direction")]
    public float angleVariance = 10f;

    public float thrustMultiplier = 1f;

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
        newRocket.name = "Rocket " + m_rocketsLaunched + " (" + name + ")";
        newRocket.team = team;
        newRocket.behaviour = rocketBehaviours [Random.Range (0, rocketBehaviours.Length)];

        float angle = FiringAngle;
        newRocket.startDirection = Quaternion.AngleAxis (angle, Vector3.forward) * Vector3.right;
        newRocket.thrustMultiplier = thrustMultiplier;

        m_rocketsLaunched++;

        return newRocket;
    }
}
