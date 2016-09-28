using UnityEngine;
using System.Collections;

public class PatrolBehaviour : MonoBehaviour {
    public Vector2 start;
    public Vector2 end;
    public float speed = 1f;

    bool goingTowardsEnd = true;
    float t = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float totalDistance = (end - start).magnitude;
        float duration = totalDistance / speed;

        if (goingTowardsEnd) {
            t += Time.deltaTime / duration;
            transform.position = Vector2.Lerp (start, end, t);

            while (t >= 1f) {
                t -= 1f;
                goingTowardsEnd = false;
            }
        }
        else {
            t += Time.deltaTime / duration;
            transform.position = Vector2.Lerp (end, start, t);

            while (t >= 1f) {
                t -= 1f;
                goingTowardsEnd = true;
            }
        }
	}
}
