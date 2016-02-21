using UnityEngine;
using System.Collections;

public abstract class IArma : MonoBehaviour{

    
    public float fireRate = 1;            //Secs. between attacks
    public KeyCode shootKey;

    private float timeElapsed = 0;      //Time elapsed since last attack

    public abstract void shoot();

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > fireRate && ( Input.GetKey(shootKey) || Input.GetKeyDown(shootKey) ))
        {
            shoot();
            timeElapsed = 0;
        }

    }


}
