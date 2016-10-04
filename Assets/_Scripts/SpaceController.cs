/*TwinStick Assignment
 * Roald Russel T. Palaya
 * 300714999
 * Date last Modified: 10/3/2016
 */
using UnityEngine;
using System.Collections;
    

public class SpaceController : MonoBehaviour {
    private int _spd = 2;
    
    private Transform _trfrm;

    
    public int Spd {
        get {
            return this._spd;
        }

        set
        {
            this._spd = value;
        }
            
    }
     // Use this for initialization
    void Start () {
        this._trfrm = this.GetComponent<Transform>();
        _reset();
	}
	
	// Update is called once per frame
	void Update () {
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
        if(this._trfrm.position.y <= -266f)
        {
            this._reset();
        }

    }

    
    //resets the background to starting point to scroll again
    private void _reset()
    {
        this._spd = 2;
        this._trfrm.position = new Vector2(0f, 266f);

    }
}
