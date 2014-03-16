using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "SharperEpicSpaceShooter";
	private const string gameName = "SharperExclusiveRoomName";
	public GameObject playerPrefab;
	public GameObject gameController;
	public HostData[] hostList;

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

	/*void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}*/


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
