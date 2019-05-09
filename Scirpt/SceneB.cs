using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneB : MonoBehaviour
{

    //显示进度的文本
    private Text progress;
    //进度条的数值
    private float progressValue;
    //进度条
    private Slider slider;
    [Tooltip("下个场景的名字")]
    public string nextSceneName;

    private AsyncOperation async = null;

    private void Start()
    {
        progress = GetComponent<Text>();
        slider = FindObjectOfType<Slider>();
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                progressValue = async.progress;
            else
                progressValue = 1.0f;

            slider.value = progressValue;
            progress.text = (int)(slider.value * 100) + " %";

            if (progressValue >= 0.9)
            {
                progress.text = "触摸屏幕继续";
                if (Input.GetMouseButtonDown(0))
                {
                    async.allowSceneActivation = true;
                }
            }

            yield return null;
        }

    }

}