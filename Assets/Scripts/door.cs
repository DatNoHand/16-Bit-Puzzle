using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetButtonDown("k"))
        {
            SceneManager.LoadScene("datnohand");
        }

    }
}
