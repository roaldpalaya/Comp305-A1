/*TwinStick Assignment
 * Roald Russel T. Palaya
 * 300714999
 * Date last Modified: 10/3/2016
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Start_Click()
        {
        SceneManager.LoadScene("GameScene");
        }

}
