using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "SharperEpicSpaceShooter";
	private const string gameName = "SharperExclusiveRoomName";
	public GameObject playerPrefab;
	public GameObject gameController;
	public HostData[] hostList;
	public Camera camera;
	public bool gameIsRunning = false;

	private float screenWidth = 0;
	private float screenHeight = Screen.height;
	

	void Start()
	{
		screenWidth = camera.pixelWidth;

	}


	public void SpawnPlayer()
	{
		Network.Instantiate(playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
	}
	
	public void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		Network.Instantiate(gameController, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
		SpawnPlayer();
		Debug.Log("Server Initializied");
	}

	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer && !gameIsRunning)
		{
			if (GUI.Button(new Rect((screenWidth/2 - screenWidth/4), 
			                        (screenHeight/6 * 2), 
			                        (screenWidth/2), 
			                        (screenHeight/6)), "Start Game"))
			{
				StartServer();
			}
			
			if (GUI.Button(new Rect((screenWidth/2 - screenWidth/4), 
			                        (screenHeight/6 * 3), 
			                        (screenWidth/2), 
			                        (screenHeight/6)), "Join Game"))
			{
				RefreshHostList();
			}
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect((screenWidth/2 - screenWidth/4), 
					               			(screenHeight/6 * 4), 
					              		 	(screenWidth/2), 
					               			(screenHeight/6)), hostList[i].gameName))
					{
						JoinServer(hostList[i]);
					}
				}
			}
		}
	}


	public void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	public void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

	void OnConnectedToServer()
	{
		SpawnPlayer();
		Debug.Log("Server Joined");
	}


}
