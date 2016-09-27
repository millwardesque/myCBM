using UnityEngine;
using System.Collections;

public class ScoreKeeper {
    int m_score = 0;
    public int Score {
        get { return m_score; }
        set {
            int oldScore = m_score;
            m_score = value;
            if (m_score != oldScore) {
                GameManager.Instance.Messenger.SendMessage (GameManager.Instance, "Score Changed", m_score);
            }
        }
    }
}
