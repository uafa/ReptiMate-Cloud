using ConsoleApp1;

var server = new WebSocketServer();
await server.Start("http://localhost:8080/");