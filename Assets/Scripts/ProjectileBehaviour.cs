using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : ScriptableObject {
    public virtual  void Initialize(GameObject owner) { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
}
