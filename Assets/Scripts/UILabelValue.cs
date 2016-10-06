using UnityEngine;
using UnityEngine.UI;

public class UILabelValue : MonoBehaviour {
    public Text value;
    public string changeEventName;

	// Use this for initialization
	void Start () {
        GameMessenger.Instance.Messenger.AddListener (changeEventName, OnChange);
	}
	
    void OnChange(Message message) {
        if (value != null) {
            value.text = message.data.ToString ();
        }
    }
}
