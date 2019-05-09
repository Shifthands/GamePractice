
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;
public class NewBehaviourScript : MonoBehaviour
{
    public enum ChildSceneItem
    {
        Entrance,
        Sea,
        Mountain,
        Fish,
        Car,
        Main
    }
    public string ChoseChildSceneName;
    public GameObject mainScene;
    public GameObject[] Scene;
    public  GameObject SceneNow;
    public GameObject LastScene;
    public GameObject ReturnButtom;
    private bool Waitting;
    public GameObject[] MainSceneChose;
    public float OpenSceneAnimTime;
    public GameObject BuyPanel;
    // Use this for initialization
    void Start()
    {
        Waitting = false;
        SceneNow = mainScene;
        ChoseChildSceneName = "Main";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {//判断是否是点击事件
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo)&&!Waitting)//等待时不可操作；
            {
                if(hitInfo.collider.gameObject.tag=="SceneChose")
                {
                    //切换场景事件
                    ChoseChildSceneName = hitInfo.collider.gameObject.name;
                }
                else if(hitInfo.collider.gameObject.tag == "SceneObj")
                {
                    //场景内部操作事件
                    Event(hitInfo.collider.gameObject);
                }
                //childscene.transform.position = mainScene.transform.position-new Vector3(0,0,-50);
                Debug.Log(hitInfo.collider.gameObject.name);
                //sceneSwitch(hitInfo.collider.gameObject.name);
                //如果是一根手指触摸屏幕而且是刚开始触摸屏幕 
                //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                //{
                //}
            }
        }
                //info.GetComponent<Text>().text=cube.transform.position.x+"";
        if(SceneNow.name!=ChoseChildSceneName)
        {
                sceneSwitch(ChoseChildSceneName);
        }
    }

    public void sceneSwitch(string ChoseScene)
    {
        int sceneNum=5;
        switch(ChoseScene)
        {
            case "Entrance":
                sceneNum = (int)ChildSceneItem.Entrance;
                break;
            case "Sea":
                sceneNum = (int)ChildSceneItem.Sea;
                break;
            case "Mountain":
                sceneNum = (int)ChildSceneItem.Mountain;
                break;
            case "Fish":
                sceneNum = (int)ChildSceneItem.Fish;
                break;
            case "Car":
                sceneNum = (int)ChildSceneItem.Car;
                break;
            default:
                sceneNum = (int)ChildSceneItem.Main;
                break;
        }
        sceneClose(SceneNow);
        SceneNow = Scene[sceneNum];
        sceneOpen(Scene[sceneNum]);
        ChoseChildSceneName = SceneNow.name;
     }
    public void sceneOpen(GameObject scene)
    {
        
        if(scene.name=="Main")
        {   
            ReturnButtom.SetActive(false);
        }
        else
        {
            //非主界面开启返回按钮启动协程
            StartCoroutine("ReturnButtomOpen");
        }
        //打开选择的场景
        StartCoroutine(SceneEventWait(scene));

    }
    public void sceneClose(GameObject scene)
    {
        if(!Waitting)
        {
            scene.GetComponent<Animator>().SetBool("Close", true);
            LastScene = scene;
            StartCoroutine("ObjWaitDisActivity");
        }
    }
    public void returnMain()
    {
        if(!Waitting)
        {
            ChoseChildSceneName = "Main";
        }
    }
    IEnumerator  ObjWaitDisActivity()
    {
        Waitting = true;
        for(float i=2;i>=0;i-=Time.deltaTime)
        {
            yield return 0;
        }
        if (LastScene!=null)
        {
            LastScene.SetActive(false);
            LastScene = null;
        }
        Waitting = false;

    }
    IEnumerator ReturnButtomOpen()
    {
        Waitting = true;
        for (float i = 2; i >= 0; i -= Time.deltaTime)
        {
            yield return 0;
        }
        ReturnButtom.SetActive(true);
        Waitting = false;
    }
    IEnumerator SceneEventWait(GameObject scene)
    {
        Waitting = true;
        scene.SetActive(true);
        scene.GetComponent<Animator>().SetBool("Close", false);
        for(float i=0;i>=OpenSceneAnimTime;i++)
        {
            yield return 0;
        }
        //场景打开动画播放完毕取消等待
        Waitting = false;
    }
    IEnumerator UnLocked()
    {
        while(!mainScene)
        {
            //等待主场景打开
            yield return 0;
        }
        for (int i = 0; i <= 3; i++)
            {
            if (MainSceneChose[i].GetComponent<SceneState>().FirstUnlocked == false&& MainSceneChose[i].GetComponent<SceneState>().UnLocked == false)//未解锁且第一次解锁
                {
                    MainSceneChose[i].GetComponent<SceneState>().FirstUnlocked = true;
                    MainSceneChose[i].GetComponent<SceneState>().UnLocked = true;
                }
            }
        
    }
    public void Event(GameObject EventObj)
    {
        switch(EventObj.name)
        {
            case "Ticket":
                StartCoroutine(UnLocked());
                break;
            default:
                break;
        }
    }
}


