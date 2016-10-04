/*TwinStick Assignment
 * Roald Russel T. Palaya
 * 300714999
 * Date last Modified: 10/3/2016
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShipController : MonoBehaviour {

    
    private float _shipSpd;
    public int _health = 1;
    private Transform _trfrm;

    public Transform _laser;
    public Transform explosion;


    public float _laserDistance = 15.0f;
    public float _fireDelay = 0.5f;
    public float _nextShot = 0.0f;

    public GameController gctrl;

    public List<KeyCode> shootBtn;

    [Header("Audio Source ")]
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

    //Resets the ship to start position
    void _reset()
    {
        this._trfrm = this.GetComponent<Transform>();
        this._shipSpd = 100.0f;
        _trfrm.position = new Vector2(0, -217);
        _health = 1;

    }
    //Where the horizontal movement happens
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
    //Allows the weapon to be fired
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

    //Collision with meteor and star
    void OnTriggerEnter2D (Collider2D crash)
    {
        
            
            if (crash.gameObject.tag.Contains("Meteor"))
            {
            Debug.Log("Ship hit by meteor");
                MeteorController _meteor = crash.gameObject.GetComponent("MeteorController") as MeteorController;
                _health -= _meteor._dmg;
                this.Explosion3.Play();
                this.gctrl.Lives -= 1;
            Debug.Log("Ship hit");

                
            }
            if (crash.gameObject.tag.Contains("Star"))
        {
            this.Powerup.Play();
            this.gctrl.Lives += 1;
            this.gctrl.Score += 20;
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
