using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {
    public int maxHealth;

    int m_currentHealth = 1;
    public int CurrentHealth {
        get { return m_currentHealth; }
        set {
            if (m_currentHealth > 0) {
                m_currentHealth = value;

                m_currentHealth = Mathf.Clamp (m_currentHealth, 0, maxHealth);
                if (m_currentHealth == 0) {
                    Dead ();
                }
            }
        }
    }

    void Awake() {
        CurrentHealth = maxHealth;
    }

    public void Dead() {
        gameObject.SendMessage ("OnDead", null, SendMessageOptions.DontRequireReceiver);
        GameManager.Instance.Messenger.SendMessage (this, "On Destructable Dead", this);
        Destroy (gameObject);
    }
}
