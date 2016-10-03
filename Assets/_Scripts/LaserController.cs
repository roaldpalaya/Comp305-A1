using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {
    public float _life = 15.0f;
    public float _speed = 50f;
    public int _dmg = 1;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, _life);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.up * Time.deltaTime * _speed);
	}

   
}
