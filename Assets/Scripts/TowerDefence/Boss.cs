using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private void OnDestroy()
    {
        ChangeScene();
    }

    private void ChangeScene()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
