# PublicAPIProxy
The backend and frontend are located in the same repository but in separate folders. In `Client` is the frontend located, and in `ProxyServer` the backend/C# Proxy. The markdown in these projects present the structure of the respective project.


(For better workflow, I opened both folders separate IDE windows.)

## Running the project from the root folder

In the terminal execute `cd .\Client\dnd-api\` and then `ng serve`. It should run at [local](http://localhost:4200/).
For the Proxy execute in another terminal `cd .\ProxyServer\src\` and then `dotnet run`.

## Testing the project
In the terminal execute `cd .\Client\dnd-api\` and then `ng test`.
For the Proxy execute in another terminal `cd .\ProxyServer\` and then `dotnet test`.
