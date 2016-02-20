using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Requirements
/*
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Text))]
*/
public class DebugHudController : MonoBehaviour {

    public Text stateText;
    public Text speedText;
    public PlayerController playerController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        stateText.text = playerController.getMovementState().ToString();
        speedText.text = playerController.getSpeed().ToString();
	
	}
}
  