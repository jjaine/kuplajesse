using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class LoadLevel : MonoBehaviour
{

	void Update(){
		if(Input.GetKeyDown(KeyCode.Return)){
			NextLevel();
		}
	}

	public void NextLevel()
	{
        int level = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(level + 1);
	}

	public void ReloadLevel()
	{
		int level = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(level);
	}
}