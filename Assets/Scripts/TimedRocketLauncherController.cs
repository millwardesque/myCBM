using UnityEngine;
using System.Collections;

public class TimedRocketLauncherController : MonoBehaviour {
    RocketLauncher m_launcher;
    public float timeBetweenRockets = 2f;
    public float minTimeBetweenRockets = 0.1f;
    public float timeDecreaseAfterRocket = 0.1f;

    [Header("Firing Direction")]
    public float firingAngle;
    public float firingAngleVariance;

    float elapsedTime = 0f;

    void Awake() {
        m_launcher = GetComponent<RocketLauncher> ();
    }


	void Update () {
        elapsedTime += Time.deltaTime;        
        while (elapsedTime >= timeBetweenRockets) {
            m_launcher.FiringAngle = firingAngle + Random.Range (-firingAngleVariance, firingAngleVariance);
            m_launcher.FireRocket ();
            elapsedTime -= timeBetweenRockets;

            if (timeBetweenRockets > minTimeBetweenRockets) {
                timeBetweenRockets -= timeDecreaseAfterRocket;
                timeBetweenRockets = Mathf.Max (timeBetweenRockets, minTimeBetweenRockets);
            }

            if (Mathf.Abs (timeBetweenRockets) < minTimeBetweenRockets) {
                break;
            }
        }
	}
}
