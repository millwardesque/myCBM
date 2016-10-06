using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject gameOverWindow;

    ScoreKeeper m_scoreKeeper;
    public ScoreKeeper Score {
        get { return m_scoreKeeper; }
    }

    GameObject m_player;
    public GameObject Player {
        get { return m_player; }
    }

    bool m_isPaused = false;

    public static GameManager Instance = null;
    void Awake() {
        if (Instance == null) {
            Instance = this;
            m_scoreKeeper = new ScoreKeeper ();

            m_player = GameObject.FindGameObjectWithTag ("Player");
        }
        else {
            Destroy (gameObject);
        }
    }

    void Start() {
        GameMessenger.Instance.Messenger.AddListener ("On Destructable Dead", OnDestructableDead);
        Time.timeScale = 1f;
    }

    void Update() {
        if (Input.GetKeyDown (KeyCode.R)) {
            RestartGame ();
        }
        else if (Input.GetKeyDown (KeyCode.P)) {
            TogglePause ();
        }
    }

    public void TogglePause() {
        if (m_isPaused) {
            Time.timeScale = 1f;
        }
        else {
            Time.timeScale = 0f;
        }

        m_isPaused ^= true;
    }

    public void RestartGame() {
        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }

    void OnDestructableDead(Message message) {
        Destructable dead = message.data as Destructable;
        if (m_player != null && dead == m_player.GetComponent <Destructable>()) {
            OnGameOver ();
        }
    }

    void OnGameOver() {
        GameObject gameOver = Instantiate<GameObject> (gameOverWindow);
        gameOver.transform.SetParent (FindObjectOfType<Canvas>().transform, false);
    }
}
