using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScreen : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("game");
        }
    }
}

