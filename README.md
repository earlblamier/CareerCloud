
# CareerCloud Web API

## Overview

CareerCloud Web API is a RESTful service built using ASP.NET Core, providing endpoints for managing applicant profiles, job applications, resumes, and various other career-related entities. This project uses Entity Framework for data access, a generic repository pattern for CRUD operations, and includes Swagger for API documentation and testing.

### Key Features:
- **Applicant Education Management**
- **Job Application Management**
- **Resume Management**
- **Company Job Management**
- **Security and Roles Management**

## Requirements

- .NET 6 or higher
- Docker (for containerization)

## Installation

### 1. Clone the Repository

```bash
git clone https://github.com/your-repo/careercloud-webapi.git
cd careercloud-webapi
```

### 2. Install Dependencies

Make sure you have Docker and .NET SDK installed. Then, restore the dependencies:

```bash
dotnet restore
```

### 3. Setup Docker

To run the project using Docker, you need to build the Docker image. Make sure you have Docker installed and running on your machine.

1. **Build the Docker image:**

```bash
docker build -t careercloud-api .
```

2. **Run the container:**

```bash
docker run -d -p 5000:80 careercloud-api
```

This will run the API inside a Docker container, exposing it on port 5000.

## Running Tests

### 1. Test Explorer

To ensure all tests are passing, you can run the tests via Visual Studio Test Explorer or use the command line.

```bash
dotnet test
```

All tests should pass successfully. If any issues arise, ensure that the project has been properly built and dependencies are correctly restored.

## Usage

### 1. Swagger UI

Once the application is running, you can access the Swagger UI to test the API. Navigate to:

```
http://localhost:5000/swagger
```

The Swagger UI provides an interactive interface where you can test various API endpoints. You can make requests and check the responses for all endpoints such as **Applicant Education**, **Applicant Profile**, **Company Job**, etc.

### 2. Accessing Applicant Education Example (Swagger Issue)

While interacting with the **Applicant Education** API through Swagger, you may encounter an issue when trying to get the `ID` due to a conflict with two constructors in the `ApplicantEducationPoco` class.

This issue happens because the class has more than one constructor that can be invoked, causing ambiguity when trying to fetch or manipulate the ID. To resolve this issue, ensure that Swagger uses the correct constructor by adjusting the class or adding annotations like `JsonConstructor` to specify which constructor should be used.

For example:
```csharp
[JsonConstructor]
public ApplicantEducationPoco(Guid id, string name, string description)
{
    Id = id;
    Name = name;
    Description = description;
}
```

### Known Issues

- **Swagger ID Conflict**: When testing the `ApplicantEducation` endpoint, a conflict may occur due to multiple constructors in the `ApplicantEducationPoco` class. This issue can be resolved by using the `JsonConstructor` attribute on the correct constructor to inform Swagger which constructor to use.
  
---

### Troubleshooting

If you're facing issues while building, running tests, or with Docker, here are a few tips:

1. Ensure that Docker is properly installed and running. Check the status with `docker --version`.
2. Make sure the database connection string in `appsettings.json` is correctly configured.
3. If tests fail, try running them individually to isolate the issue:
   ```bash
   dotnet test path/to/test
   ```

For any further assistance, feel free to raise an issue on the GitHub repository.

---

### License

MIT License. See the [LICENSE](LICENSE) file for more details.
