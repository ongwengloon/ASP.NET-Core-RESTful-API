# ASP.NET-Core-RESTful-API
A Simple RESTful API project using ASP .NET Core and Swagger

## StartUp.cs (Start up)
Use InMemory Database as demo data
```cs
services.AddDbContext<EmployeeContext>(opt => opt.UseInMemoryDatabase("EmployeeList"));
```

## Employee.cs (Model Class)
```cs
    public class Employee
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
```

## EmployeeController.cs (Controller Class)
```cs
     //Read All Data
     [HttpGet]
     public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeItems(){}
     
     //Read Single Data
     [HttpGet("{id}")]
     public async Task<ActionResult<Employee>> GetEmployeeItem(long id){}
     
     //Create Data
     [HttpPost]
     public async Task<ActionResult<Employee>> PostEmployeeItem(Employee item){}

     //Update Data
     [HttpPut("{id}")]
     public async Task<ActionResult<Employee>> PutEmployeeItem(long id, Employee item){}

     //Delete Data
     [HttpDelete("{id}")]
     public async Task<IActionResult> DeleteEmployeeItem(long id){}
     
     //Update Partial Data
     [HttpPatch("{id}")]
     public async Task<ActionResult<Employee>> PatchEmployeeItem(long id, [FromBody]JsonPatchDocument<Employee> itemPatch){}
```

## POSTMAN API Testing
https://www.getpostman.com/collections/ddec90914d612cf9dcf8

## Implement Swagger
```ps
View > Other Windows > Package Manager Console
Install-Package Swashbuckle.AspNetCore -Version 5.0.0-rc4
```
![swagger_api](https://github.com/ongwengloon/ASP.NET-Core-RESTful-API/blob/master/swagger_api.png)

## FAQ
If encounter an issue about _'project.assets.json not found'_ try below solution.
```cs
     Tools > NuGet Package Manager > Package Manager Console run: dotnet restore
```
