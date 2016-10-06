using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
    public float minDragDistance = 0.1f;
    Vector2 m_dragStart;
    bool m_startedDragging = false;

    void Update() {
        if (Input.GetMouseButtonDown (0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null) {
                if (hit.collider.GetComponentInChildren<RocketLauncher> ()) {
                    StartDrag (Camera.main.ScreenToWorldPoint (Input.mousePosition));    
                    m_startedDragging = true;
                }
                else {
                    m_startedDragging = false;
                }
            }
        } else if (Input.GetMouseButton (0)) {
            if (m_startedDragging) {
                UpdateDrag (Camera.main.ScreenToWorldPoint (Input.mousePosition));
            }
        } else if (Input.GetMouseButtonUp (0)) {
            if (m_startedDragging) {
                EndDrag (Camera.main.ScreenToWorldPoint (Input.mousePosition));
                m_startedDragging = false;
            }
        }

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch (0);   
            if (touch.phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay (touch.position);
                RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
                if (hit.collider != null) {
                    if (hit.collider.GetComponentInChildren<RocketLauncher> ()) {
                        StartDrag (Camera.main.ScreenToWorldPoint (touch.position));
                        m_startedDragging = true;
                    }
                    else {
                        m_startedDragging = false;
                    }
                }
            } else if (touch.phase == TouchPhase.Moved) {
                if (m_startedDragging) {
                    UpdateDrag (Camera.main.ScreenToWorldPoint (touch.position));
                }
            } else if (touch.phase == TouchPhase.Ended) {
                if (m_startedDragging) {
                    EndDrag (Camera.main.ScreenToWorldPoint (touch.position));
                    m_startedDragging = false;
                }
            }
        }
    }

    public void StartDrag(Vector2 startPosition) {
        m_dragStart = startPosition;
    }

    public void UpdateDrag(Vector2 currentPosition) {
        Vector2 dragVector = currentPosition - m_dragStart;

        if (dragVector.magnitude >= minDragDistance) {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["dragStart"] = m_dragStart;
            data["dragEnd"] = currentPosition;
            GameMessenger.Instance.Messenger.SendMessage (this, "Drag Update", data);
        }
    }

    public void EndDrag(Vector2 endPosition) {
        Vector2 dragVector = endPosition - m_dragStart;

        if (dragVector.magnitude >= minDragDistance) {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["dragStart"] = m_dragStart;
            data["dragEnd"] = endPosition;
            GameMessenger.Instance.Messenger.SendMessage (this, "Drag End", data);
        }
    }
}
