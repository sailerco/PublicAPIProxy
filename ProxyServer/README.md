# Architecure
The project is seperated in two packages src and test. 
The src packages contains
- the `Program.cs`
- the `ClientController.cs` -> catches all the HTTP requests from the frontend
- the `APIService.cs` -> intereacts with the dnd API
- the `Components.cs` -> classes representing specific JSON datastructures

## More
In the project I used the `Newtonsoft.Json` Library to improve the comparison of two JSON strings.
Additionally the library `xunit` and `moq` library were used for testing purposes.

To run the project use the command:  
```dotnet run --project .\src\ProxyServer.csproj```

To test the project us the command:   
```dotnet test```