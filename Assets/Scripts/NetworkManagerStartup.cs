﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;

public class NetworkManagerStartup : MonoBehaviour {

	private NetworkManager manager;
	private NetworkDiscovery discovery;
	private JSONNode config;

	// Use this for initialization
	void Awake()
	{
		manager = GetComponent<NetworkManager>();
		discovery = GetComponent<NetworkDiscovery>();
		// this path is relative to the working directory
		string text = System.IO.File.ReadAllText(@"network_config.json");
		config = JSON.Parse(text);
	}

	void Start()
	{
		string role = config["role"].Value;
		string address = config["server-address"].Value;
		int port = config["server-port"].AsInt;

		Debug.Log("NetworkDiscovery starting as " + role);

		manager.networkAddress = address;
		manager.networkPort = port;

		if (role == "client") {
			manager.StartClient();
		} else if (role == "server") {
			manager.StartServer();
		} else if (role == "host") {
			manager.StartHost();
		} else {
			Debug.Log("NetworkDiscovery config missing 'role'.");
		}
	}
}
	
