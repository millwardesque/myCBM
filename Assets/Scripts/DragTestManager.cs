using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragTestManager : MonoBehaviour {
    public static DragTestManager Instance = null;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy (gameObject);
        }
    }
}
