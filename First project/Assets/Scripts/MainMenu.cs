using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public NetworkManager networkManager;
	public bool gameIsRunning = false;
	public Camera camera;
	private float screenWidth = 0;
	private float screenHeight = Screen.height;
	public GUIText scoreText;
	public GUIText gameOverText;
	public GUIText restartText;

	// Use this for initialization
	void Start () {

		scoreText.text = "";
		gameOverText.text = "";
		restartText.text = "";
		screenWidth = camera.pixelWidth;
		networkManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		if(!gameIsRunning)
		{
			if (GUI.Button(new Rect((screenWidth/2 - screenWidth/4), 
			                        (screenHeight/6 * 2), 
			                        (screenWidth/2), 
			                        (screenHeight/6)), "Start Game"))
			{	
				gameIsRunning = true;
				networkManager.StartServer();
			}
			
			if (GUI.Button(new Rect((screenWidth/2 - screenWidth/4), 
			                        (screenHeight/6 * 3), 
			                        (screenWidth/2), 
			                        (screenHeight/6)), "Join Game"))
			{
				networkManager.RefreshHostList();
				if (networkManager.hostList != null)
				{
					networkManager.JoinServer(networkManager.hostList[0]);
					gameIsRunning = true;
				}
			}

			/*if (networkManager.hostList != null)
			{
				for (int i = 0; i < networkManager.hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), networkManager.hostList[i].gameName))
						networkManager.JoinServer(networkManager.hostList[i]);
				}
			}*/
		}

	}
}
