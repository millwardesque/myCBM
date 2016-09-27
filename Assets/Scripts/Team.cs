using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Team", menuName = "Team", order = 1)]
public class Team : ScriptableObject{
    public string teamName;
    public Color32 teamColour;
    public string layerName;

    public void Initialize(GameObject owner) {
        owner.layer = LayerMask.NameToLayer (layerName);
        SpriteRenderer[] sprites = owner.GetComponentsInChildren<SpriteRenderer> ();
        foreach (SpriteRenderer sprite in sprites) {
            sprite.color = teamColour;
        }
    }
}
