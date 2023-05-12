﻿using System.Net.WebSockets;
using System.Text;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocket;
using WebSocket.Gateway;
using WebSocket.Services;

public class WebSocketClient : IWebSocketClient
{
    private readonly Uri _url;
    private ClientWebSocket _socket;
    private readonly IMeasurementsServiceWS measurementsService;
    private readonly ITerrariumServiceWS terrariumService;
    private DataConvertor dataConvertor;
    public WebSocketClient(string url, IMeasurementsServiceWS measurementsService, ITerrariumServiceWS terrariumService)
    {
        _url = new Uri(url);
        this.measurementsService = measurementsService;
        this.terrariumService = terrariumService;
        dataConvertor = new DataConvertor();
    }

    public async Task ConnectAsync()
    {
        _socket = new ClientWebSocket();

        await _socket.ConnectAsync(_url, CancellationToken.None);

        Console.WriteLine($"WebSocket connected to {_url}");
    }

    public async Task StartReceivingAsync()
    {
        var buffer = new byte[1024];

        TerrariumLimits currentLimits = await terrariumService.GetTerrariumLimitsAsync();

        await SendConfigurationAsync(currentLimits);

        while (_socket.State == WebSocketState.Open)
        {

            var result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            else
            {
                var data = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($" WebSocketClient: Received data: {data}");

                // Handle incoming data

                var jObject = JObject.Parse(data);

                if (jObject["cmd"].Value<string>() == "rx")
                {
                    Console.WriteLine($" WebSocketClient: Received data: ");
                    var convertedData = dataConvertor.GetData(data);
                    measurementsService.SendMeasurements(convertedData);
                }
            }
            var possibleNewLimits = await terrariumService.GetTerrariumLimitsAsync();
            if (CompareLimits(currentLimits, possibleNewLimits))
            {
                await SendConfigurationAsync(possibleNewLimits);
                currentLimits = possibleNewLimits;
            }
        }
    }

    private bool CompareLimits(TerrariumLimits currentLimits, TerrariumLimits possibleNewLimits)
    {
        if (currentLimits.TemperatureLimitMax != possibleNewLimits.TemperatureLimitMax)
            return true;

        if (currentLimits.TemperatureLimitMin != possibleNewLimits.TemperatureLimitMax)
            return true;

        return false;
    }


    public async Task SendConfigurationAsync(TerrariumLimits terrariumLimits)
    {
        if (_socket.State != WebSocketState.Open)
            throw new Exception("WebSocket connection has not been established");
        
        Console.WriteLine("Preparing to send data.");
        
        var terrariumLimitsInHexa = dataConvertor.ConvertTemperatureLimitsToHex(terrariumLimits);
        
        Console.WriteLine("Converted data to hex: " + terrariumLimitsInHexa);
        
        var buffer = Encoding.UTF8.GetBytes(terrariumLimitsInHexa);
        
        await _socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true,
            CancellationToken.None);
        Console.WriteLine("Sent data: ");
    }

    public async Task CloseAsync()
    {
        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }
}