using UnityEngine;
using System.Collections;

public class TeamMember : MonoBehaviour {
    public Team team;

	void Start () {
        if (team != null) {
            team.Initialize (gameObject);    
        }
	}
}
