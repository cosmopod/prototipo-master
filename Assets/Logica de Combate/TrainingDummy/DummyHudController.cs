using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DummyHudController : MonoBehaviour {

    public Text hpText;
    public Combatiente dummyCombatController;

	// Use this for initialization
	void Start () {
        if (!hpText) Debug.LogWarning("Falta hpText");
        hpText.text = "loading...";
	}
	
	// Update is called once per frame
	void Update () {
        if (dummyCombatController)
        {
            hpText.text = dummyCombatController.getHp() + "/" + dummyCombatController.getMaxHp();
        }
	}
}
