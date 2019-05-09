using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
public class SceneState : MonoBehaviour {
    public bool UnLocked;
    public GameObject Model;
    public bool FirstUnlocked;
    // Use this for initialization


    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update () {
        if (UnLocked&&FirstUnlocked)
        {
            StartCoroutine("skinUnlock");
        }
	}
    IEnumerator  skinUnlock()
    {
        //解锁场景，场景可进入，播放场景解锁特效
        for(float i=0;i<=2;i+=Time.deltaTime)
        {
            Model.GetComponent<MeshRenderer>().material.color = new Color(i/2f, i / 2f, i / 2f);
            yield return 0;
        }
        FirstUnlocked = false;
        sceneUnlockedParticle();
        this.GetComponent<BoxCollider>().enabled = true;

    }
    public void sceneUnlockedParticle()
    {

    }
}
