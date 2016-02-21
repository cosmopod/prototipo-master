using UnityEngine;
using System.Collections;

public class CheckerController : MonoBehaviour
{

    public enum Place { Bottom, Top, Right, Left };
    public Place collisionPlace;

    private bool is_collisioned = false;

    private PlayerController parentController;


    // Use this for initialization
    void Start()
    {
        parentController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        parentController.CollisionChange(collisionPlace, collider, true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        parentController.CollisionChange(collisionPlace, collider, false);
    }
}
