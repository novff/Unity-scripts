using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
	**		simple modular script for both in-game and main menus
	**		HOW TO USE:
	**			-supply your own ui elements into the serialized fields in editor
	**			-apply wanted methods to unity buttons 
	**		TODO:
	**			-add scripted placeholders for portability
	**			-add HUD serialized field and toggles for it
	**
    **                                                                  -novff
*/
public class SimpleMenuAndUIScript : MonoBehaviour
{
	private bool IsPaused = false;
	private bool InInventory = false;
	private bool InOptions = false;
	[SerializeField]private GameObject PauseUI;
	[SerializeField]private GameObject InventoryUI;
	[SerializeField]private GameObject OptionsUI;

	void Awake()
	{
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
	}
	public void StartGame(){SceneManager.LoadScene(1);}

    public void NextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
	}

    public static void Restart(){SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);}

	public void MainMenu(){SceneManager.LoadScene(0);}

	public void QuitGame(){Application.Quit();}

    public void Link(){Application.OpenURL("https://example.com");}

    public void Pause()
	{
		if(IsPaused)
		{
			PauseUI.SetActive(false);
			Time.timeScale = 1f;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false; 
		}
		else
		{
			PauseUI.SetActive(true);
			Time.timeScale = 0f;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true; 
		}
		IsPaused = !IsPaused;
	}
	
	public void Options()
	{
		if(InOptions)
		{
			PauseUI.SetActive(true);
			OptionsUI.SetActive(false);
		}
		else
		{
			PauseUI.SetActive(false);
			OptionsUI.SetActive(true);
		}
		InOptions = !InOptions;
	}
	
	public void Inventory()
	{
		if(!IsPaused)
		{
			if(InInventory)
			{
				InventoryUI.SetActive(false);
				Time.timeScale = 1f;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				InventoryUI.SetActive(true);
				Time.timeScale = 0f;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true; 
			}
			InInventory = !InInventory;
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(InInventory)
				Inventory();
			else
			{
				if(!InOptions){Pause();}
				else{Options();}
			}
		}
		if(Input.GetKeyDown(KeyCode.E))
			Inventory();
	}
}