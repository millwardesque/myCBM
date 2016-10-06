using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDragRocketLauncherController : MonoBehaviour {
    public float minThrustMultiplier = 0.25f;
    public float maxThrustMultiplier = 2f;
    public float dragLengthPerThrustUnit = 2f;

    LineRenderer m_directionIndicator;
    RocketLauncher m_launcher;

    void Awake() {
        m_launcher = GetComponent<RocketLauncher> ();
        m_directionIndicator = gameObject.AddComponent<LineRenderer> ();
        m_directionIndicator.SetWidth (0.4f, 0.05f);
    }

    void Start() {
        GameMessenger.Instance.Messenger.AddListener ("Drag Update", OnDragUpdate);
        GameMessenger.Instance.Messenger.AddListener ("Drag End", OnDragEnd);
        m_directionIndicator.enabled = false;
    }

    void Update() {
        Vector2 mouseDirection = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector2 direction = mouseDirection - (Vector2)transform.position;
        float firingAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        m_launcher.FiringAngle = firingAngle;
    }

    void OnDragUpdate(Message message) {
        Dictionary<string, object> data = (Dictionary<string, object>)message.data;

        Vector2 dragStart = (Vector2)data ["dragStart"];
        Vector2 dragEnd = (Vector2)data ["dragEnd"];
        Vector2 direction = dragEnd - dragStart;
        float magnitude = direction.magnitude;
        direction.Normalize ();

        dragEnd = dragStart - direction * Mathf.Clamp (magnitude / dragLengthPerThrustUnit, minThrustMultiplier, maxThrustMultiplier);

        if (m_directionIndicator != null) {
            m_directionIndicator.SetPosition (0, dragStart);
            m_directionIndicator.SetPosition (1, dragEnd);
            m_directionIndicator.enabled = true;
        }
    }

    void OnDragEnd(Message message) {
        if (m_directionIndicator != null) {
            m_directionIndicator.enabled = false;
        }

        Dictionary<string, object> data = (Dictionary<string, object>)message.data;

        Vector2 dragStart = (Vector2)data ["dragStart"];
        Vector2 dragEnd = (Vector2)data ["dragEnd"];
        Vector2 dragVector = dragStart - dragEnd;
        float thrustMagnitude = Mathf.Clamp (dragVector.magnitude / dragLengthPerThrustUnit, minThrustMultiplier, maxThrustMultiplier);
        Vector2 direction = dragVector.normalized;

        float firingAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (m_launcher != null) {
            m_launcher.thrustMultiplier = thrustMagnitude;
            m_launcher.FiringAngle = firingAngle;
            m_launcher.FireRocket ();
        }
    }
}
