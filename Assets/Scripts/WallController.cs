using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {


    private EnvironmentController controladorEntorno;

	// Use this for initialization
	void Start () {
        controladorEntorno = Camera.main.GetComponent<EnvironmentController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay2D(Collision2D collision)
    {

    }
}
