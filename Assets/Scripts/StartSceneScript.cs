using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject mainMenu;

    void Start()
    {
        gameObject.SetActive(true);
        StartCoroutine(EndAnimation());
    }
    IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(3f);    //sorry for this
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
