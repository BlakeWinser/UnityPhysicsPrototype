using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;
using System;

public class SteamSocketServer : SocketManager
{
	public override void OnConnecting(Connection connection, ConnectionInfo data)
	{
		connection.Accept();
		Console.WriteLine($"{data.Identity} is connecting");
	}

	public override void OnConnected(Connection connection, ConnectionInfo data)
	{
		Console.WriteLine($"{data.Identity} has joined the game");
	}

	public override void OnDisconnected(Connection connection, ConnectionInfo data)
	{
		Console.WriteLine($"{data.Identity} is out of here");
	}

	public override void OnMessage(Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel)
	{
		Console.WriteLine($"We got a message from {identity}!");

		// Send it right back
		connection.SendMessage(data, size, SendType.Reliable);
	}

	SteamSocketServer server = SteamNetworkingSockets.CreateNormalSocket<SteamSocketServer>(NetAddress.AnyIp(480));
}
