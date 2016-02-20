using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;

    public float offsetX;
    public float offsetY;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 new_position = new Vector3(target.position.x + offsetX, target.position.y + offsetY, -1);
        transform.position = new_position;
        transform.rotation = Quaternion.identity;
	}
}
