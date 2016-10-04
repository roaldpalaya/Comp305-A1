/*TwinStick Assignment
 * Roald Russel T. Palaya
 * 300714999
 * Date last Modified: 10/3/2016
 */

using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour {
    private int _spd;
    private Transform _trfrm;

    public int _dmg = 1;
    public Transform explosion;
    public int _healthEnemy = 1;

   
    public GameController gctrl;
    
    [Header("Audio Source")]
    public AudioSource Explosion3;

    public int Spd
    {
        get
        {
            return this._spd;
        }

        set
        {
            this._spd = value;
        }

    }
    // Use this for initialization
    void Start()
    {
        this._trfrm = this.GetComponent<Transform>();
        _reset();
    }

    // Update is called once per frame
    void Update()
    {
        this._movement();
        this._boundChk();


    }

    //moves the background
    private void _movement()
    {
        Vector2 newPos = this._trfrm.position;
        newPos.y -= this._spd;
        this._trfrm.position = newPos;

    }
    //Check if background is almost out of bounds
    private void _boundChk()
    {
        if (this._trfrm.position.y <= -255f)
        {
            this._reset();
        }

    }


    //resets the background to starting point to scroll again
    private void _reset()
    {
        this._spd = 6;
        this._trfrm.position = new Vector2(Random.Range(-270,270), 280);

    }
    //collision with weapon
    public void OnTriggerEnter2D(Collider2D attack)
    {
        

        if (attack.gameObject.tag.Contains("Weapon") )
        {
            this.gctrl.Score += 50;
            LaserController _laser = attack.gameObject.GetComponent("LaserController") as LaserController;
            _healthEnemy -= _laser._dmg;
            Destroy(attack.gameObject);

        }
        if (_healthEnemy <= 0)
        {
            this.Explosion3.Play();
            
            if (explosion) {
                Debug.Log("exploding");
                GameObject explode = ((Transform)Instantiate(explosion, this._trfrm.position, this._trfrm.rotation)).gameObject;
                explode.GetComponent<Renderer>().sortingLayerName = "Meteor";
                Destroy(explode, 2.0f);
                Debug.Log("exploded");
          
            }
            _reset();

        }
    }
   
}




