using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    public Text 

    IEnumerator Start()
    {
        asyncOperation = SceneManager.LoadSceneAsync("{SceneName}");

        while(!asyncOperation.isDone)
        {
            float progress = Clamp01(asyncOperation.progress / 0.9f);


            yield return null;


            Debug.log(progress * 100f);
        }


      
    }

   
}
