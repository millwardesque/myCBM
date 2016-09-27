using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
    Rigidbody2D m_rb;

    public Team team;
    public GameObject explosionPrototype;
    public Vector2 startDirection;
    public float thrust = 10f;
    public float explosivePower = 10f;

    public float lifetime = 3f;
    float lifeRemaining;

    Vector2 lastDamagePoint;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody2D> ();

        UpdateSpriteDirection (startDirection);

        m_rb.AddForce (startDirection * thrust, ForceMode2D.Impulse);

        lifeRemaining = lifetime;

        if (team != null) {
            team.Initialize (gameObject);    
        }
	}

    void Update() {
        lifeRemaining -= Time.deltaTime;
        if (lifeRemaining <= 0f) {
            Destroy (gameObject);
        }
    }
	
	void FixedUpdate () {
        UpdateSpriteDirection (m_rb.velocity);
	}

    void UpdateSpriteDirection(Vector2 direction) {
        // Rotate the sprite / collider to face the direction of motion.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
            }
        }
    }
}
