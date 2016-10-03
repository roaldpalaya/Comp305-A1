using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour {
    private int _spd = 4;

    private Transform _trfrm;
    public Transform explosion;
    public int _health = 1;
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
        this._spd = Random.Range(3,7);
        this._trfrm.position = new Vector2(Random.Range(-270,270), 280);

    }

    void OnTriggerEnter2D(Collider2D attack)
    {
        Debug.Log("hit by" + attack.gameObject.tag);

        if (attack.gameObject.tag.Contains("Weapon"))
        {
            LaserController _laser = attack.gameObject.GetComponent("LaserController") as LaserController;
            _health -= _laser._dmg;
            Destroy(attack.gameObject);
        }
        if (_health <= 0)
        {
            Destroy(this.gameObject);
            if (explosion) {
                Debug.Log("exploding");
                GameObject explode = ((Transform)Instantiate(explosion, this._trfrm.position, this._trfrm.rotation)).gameObject;
                explode.GetComponent<Renderer>().sortingLayerName = "Meteor";
                Destroy(explode, 2.0f);
                Debug.Log("exploded");

            }
        }
    }
}
