using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    MessageManager m_messenger;
    public MessageManager Messenger {
        get { return m_messenger; }
    }

    ScoreKeeper m_scoreKeeper;
    public ScoreKeeper Score {
        get { return m_scoreKeeper; }
    }

    GameObject m_player;
    public GameObject Player {
        get { return m_player; }
    }

    public static GameManager Instance = null;
    void Awake() {
        if (Instance == null) {
            Instance = this;

            m_messenger = new MessageManager ();
            m_scoreKeeper = new ScoreKeeper ();

            m_player = GameObject.FindGameObjectWithTag ("Player");

            Messenger.AddListener ("Destructable Dead", OnDestructableDead);
        }
        else {
            Destroy (gameObject);
        }
    }

    void OnDestructableDead(Message message) {
        Debug.Log (name + ": I'm dead!");

        Destructable dead = message.data as Destructable;
        if (dead == m_player.GetComponent <Destructable>()) {
            Debug.Log ("Game. Over.");
        }
    }
}
