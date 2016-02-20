using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

    public string[] text;

    private Text dialogText;

	// Use this for initialization
	void Start () {
        int random = Random.Range(0, text.Length);

        dialogText = GetComponentInChildren<Text>();


        dialogText.text = text[random];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
