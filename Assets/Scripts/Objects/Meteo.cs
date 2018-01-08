using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : Block {
    [SerializeField]
    private TextMesh text;
	// Update is called once per frame
	void Update () {
        Move();
        text.text = HP.ToString();
	}
}
