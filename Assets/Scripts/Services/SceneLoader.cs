using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    private  IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("TestLevel", LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;
        
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                DOTween.Sequence().PrependInterval(0.3f).OnComplete(() =>
                {
                    asyncOperation.allowSceneActivation = true;
                });
            }
            
            yield return null;
        }
    }
}
