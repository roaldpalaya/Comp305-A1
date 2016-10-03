using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShipController : MonoBehaviour {

    
    private float _shipSpd = 100.0f;
    private Transform _trfrm;
    public int _health=1;
    public Transform _laser;
    public Transform explosion;


    public float _laserDistance = 15.0f;
    public float _fireDelay = 0.5f;
    public float _nextShot = 0.0f;

    public List<KeyCode> shootBtn;

    public AudioSource Explosion3;
    public AudioSource Powerup;
    public AudioSource Laser_Shoot;
    public float ShpSpeed
    {
        get
        {
            return this._shipSpd;
        }
        set
        {
            this._shipSpd = value;
        }
    }
    
    // Current speed of the player
    public float curSpeed = 0.0f;

    // The last movement done
    //private Vector3 lastMovement = new Vector3();
    // Use this for initialization
    void Start () {
        _reset();
    }
	
	// Update is called once per frame
	void Update () {
        _movement();

        foreach (KeyCode element in shootBtn)
        {
            if (Input.GetKey(element) && _nextShot < 0)
            {
                _nextShot = _fireDelay;
                _Shoot();
                break;
            }
        }
        _nextShot -= Time.deltaTime;
    }
    void _reset()
    {
        this._trfrm = this.GetComponent<Transform>();
        this._shipSpd = 100.0f;
        _trfrm.position = new Vector2(0, -217);
        _health = 1;

    }
    //Where the horizontal movement is done
    private  void _movement()
    {
        Vector2 _move = new Vector2();
        _move.x += Input.GetAxis("Horizontal");

        _move.Normalize();

        if (_move.magnitude > 0)
        {
            curSpeed = _shipSpd;
            this.transform.Translate(_move * Time.deltaTime * _shipSpd, Space.World);
            //lastMovement = _move;
        }
        
        Vector2 _pos = _trfrm.position;
        _pos.x=Mathf.Clamp(this._trfrm.position.x, -270, 270);
        this._trfrm.position = _pos;
        
    }

    void _Shoot()
    {

        Vector2 _laserPos = this._trfrm.position;
        float rotationAngle = _trfrm.localEulerAngles.z - 90;
        _laserPos.x += (Mathf.Cos((rotationAngle) *
               Mathf.Deg2Rad) * -_laserDistance);
        _laserPos.y += (Mathf.Sin((rotationAngle) *
                     Mathf.Deg2Rad) * -_laserDistance);

        Instantiate(_laser, _laserPos, this._trfrm.rotation);
        this.Laser_Shoot.Play();
    }

    void OnTriggerEnter2D (Collider2D crash)
    {
        
            
            if (crash.gameObject.tag.Contains("Meteor"))
            {
                MeteorController _meteor = crash.gameObject.GetComponent("MeteorController") as MeteorController;
                _health -= _meteor._dmg;
                this.Explosion3.Play();
                
                
            }
            if (crash.gameObject.tag.Contains("Star"))
        {
            this.Powerup.Play();
        }
            if (_health <= 0)
            {

                if (explosion)
                {
                    Debug.Log("exploding");
                    GameObject explode = ((Transform)Instantiate(explosion, this._trfrm.position, this._trfrm.rotation)).gameObject;
                    explode.GetComponent<Renderer>().sortingLayerName = "Meteor";
                    Destroy(explode, 2.0f);
                    Debug.Log("exploded");
                    //     RespawnMeteor();
                }
                _reset();
                
             }

        
    }
}
