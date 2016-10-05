using UnityEngine;
using System.Collections;

public class GameOverWindow : MonoBehaviour {

    public void OnRestartClick() {
        GameManager.Instance.RestartGame ();
    }
}
