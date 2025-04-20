using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) GoToSelectSubject();
    }

    public void GoToSelectSubject()
    {
        SceneManager.LoadScene("SelectSubject");
    }
}
