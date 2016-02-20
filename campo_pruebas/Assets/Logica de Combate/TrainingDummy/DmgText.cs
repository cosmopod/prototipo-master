using UnityEngine;
using System.Collections;

public class DmgText : MonoBehaviour {

    public float maxLifeTime;
    public float speed;

    private bool launched = false;
    private float lifeTime = 0;
    private Transform transform;
    

	// Use this for initialization
	void Start () {
        //this.gameObject.SetActive(false);
        Debug.Log("START TEXTO");
	}
	
	// Update is called once per frame
	void Update () {
        if (launched)
        {
            //Aumentar contador
            lifeTime += Time.deltaTime;

            //Mover texto arriba
            transform = GetComponent<Transform>();
            Vector2 textPosition = transform.position;
            textPosition.y += speed * Time.deltaTime;
            transform.position = textPosition;
            
        }

        if (lifeTime > maxLifeTime)
        {
            Destroy(this.gameObject);
            Debug.Log("Destruyendo Texto");
        }
	}

    public void LaunchText(string text, Color textColor)
    {

        //this.gameObject.SetActive(true);
        launched = true;
        TextMesh dmgText = GetComponent<TextMesh>();
        dmgText.text = text;
        dmgText.color = textColor;
        
        
    }
}
