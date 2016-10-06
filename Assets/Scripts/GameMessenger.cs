using UnityEngine;
using System.Collections;

public class GameMessenger : MonoBehaviour {
    MessageManager m_messenger;
    public MessageManager Messenger {
        get { return m_messenger; }
    }

    public static GameMessenger Instance = null;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            m_messenger = new MessageManager ();    
        }
        else {
            Destroy (gameObject);
        }
    }
}
