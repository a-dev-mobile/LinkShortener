LinkShortener
=============

LinkShortener is a web application designed to create shortened URLs from longer ones, making them easier to share and manage. This project includes an API service built with .NET 8, utilizing Entity Framework Core for data access, and Swagger for API documentation.

Features
--------

* Shorten URLs with a simple API call.
* Redirect to original URLs using the shortened link.
* Docker support for easy deployment.
* Swagger integration for API documentation and testing.

Project Structure
-----------------

* `LinkShortener/`: Root directory of the project.
  * `Dockerfile`: Contains instructions for Docker to build and run the application.
  * `LinkShortener.API/`: The web API project.
    * `Controllers/`: Contains the API controllers.
    * `Models/`: Data models used by the controllers.
    * `SwaggerExtensions/`: Custom Swagger extensions for enhanced API documentation.
  * `LinkShortener.Data/`: The data access layer project.
    * `Interfaces/`: Repository interfaces.
    * `Models/`: Entity models for the database.
    * `Repositories/`: Implementation of repository interfaces.
  * `LinkShortener.Service/`: The service layer project.
    * `Interfaces/`: Service interfaces.
    * `Services/`: Implementation of service interfaces.
  * `LinkShortener.Tests/`: Unit tests for the application.

Getting Started
---------------

To run the LinkShortener application, ensure you have Docker installed and running on your machine.

1. Clone the repository to your local machine.
2. Navigate to the root directory of the project.
3. Build the Docker image:

    Copy code

    `docker build -t linkshortener .`

4. Run the container:

    arduinoCopy code

    `docker run -d -p 80:80 linkshortener`

5. Access the API through `http://localhost/swagger` to view the Swagger UI and test the API endpoints.

API Endpoints
-------------

* `POST /create`: Accepts an original URL and returns a shortened version.
* `GET /{shortUrl}`: Redirects to the original URL based on the shortened version.

Configuration
-------------

* `appsettings.json`: Contains application settings, including logging and database connection strings.
* `appsettings.Development.json`: Development-specific settings overriding those in `appsettings.json`.

Contributing
-------------

Contributions are welcome! Please feel free to submit a pull request or create an issue for any bugs, features, or improvements.

License
-------

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.
