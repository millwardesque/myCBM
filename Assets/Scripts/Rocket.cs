using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;

public class Rocket : MonoBehaviour {
    Rigidbody2D m_rb;

    public Team team;
    public GameObject explosionPrototype;
    public Vector2 startDirection;
    public float explosivePower = 10f;
    public float thrustMultiplier = 1f;

    public ProjectileBehaviour behaviour;

    public float lifetime = 3f;
    float lifeRemaining;

    Vector2 lastDamagePoint;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody2D> ();

        lifeRemaining = lifetime;

        if (team != null) {
            team.Initialize (gameObject);    
        }

        if (behaviour != null) {
            behaviour.Initialize (gameObject);
        }
        else {
            Debug.Log (name + ": Null behaviour");
        }
	}

    void Update() {
        lifeRemaining -= Time.deltaTime;
        if (lifeRemaining <= 0f) {
            Destroy (gameObject);
        }
    }
	
	void FixedUpdate () {
        behaviour.OnFixedUpdate ();
	}

    void OnCollisionEnter2D(Collision2D col) {
        if (col.rigidbody != null) {
            col.rigidbody.AddForce (m_rb.velocity.normalized * explosivePower, ForceMode2D.Impulse);    
        }

        if (col.collider.GetComponent<Destructable>()) {
            col.collider.GetComponent<Destructable> ().CurrentHealth -= 5;
        }

        lastDamagePoint = col.contacts [0].point;
        GetComponent<Destructable> ().CurrentHealth = 0;
    }

    void OnDead() {
        if (team.teamName == "Enemy") {
            GameManager.Instance.Score.Score += 1;

            if (explosionPrototype != null) {
                GameObject explosion = Instantiate<GameObject> (explosionPrototype);
                explosion.transform.position = lastDamagePoint;
                explosion.GetComponent<ParticleSystemRenderer> ().sortingOrder = 100;
                Camera.main.GetComponent<ProCamera2DShake> ().Shake ();
            }
        }
    }
}
