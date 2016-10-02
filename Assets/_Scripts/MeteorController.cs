using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour {
    private int _spd = 4;

    private Transform _trfrm;


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
        this._spd = 4;
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
        this._trfrm.position = new Vector2(Random.Range(-270,270), 280);

    }
}
