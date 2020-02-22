using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTest : MonoBehaviour {
<<<<<<< HEAD
	//cxh
=======
>>>>>>> parent of 38eddee... test01_enter
    public GameObject obj;
    public GameObject target;
    [Range(0f,100f)]
    public float power;
	// Use this for initialization
	void Start () {
        StartCoroutine("UnLocked");
	}
	
	// Update is called once per frame
	void Update () {
        //this.GetComponent<Rigidbody>().AddForce((target.transform.position-obj.transform.position)*power*Time.deltaTime, ForceMode.Force);
                //StopCoroutine("corotine");
    }
    IEnumerator UnLocked()
    {
        while(!false)
        {
            //StopCoroutine("UnLocked");
            yield return 0;
        }

    }
}
