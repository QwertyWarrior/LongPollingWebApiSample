# LongPollingWebApiSample
Simple project created to better understand how to work with long polling request

## Shared
Generic models, utilities, and signatures that can be used by both the server and the client.

### Shared/Data
Contains data signatures and containers

#### ApiResponse.
Response data structure that contains the result of the API method, or an error message if the method failed on the server side or the request was malformed.

#### WeatherForecastRequest
Sample request that contains the date of the required weather forecast.

### Shared/Models
#### WeatherForecast
Sample model that represents weather forecast.

### Shared/Wrappers
Wrappers for utilities that can be used by client and server.

#### ApiHelper
A utility that simplifies interaction with the API (used by the client in this project, but can also be used by the server to communicate with other APIs)

## WebApi
The server part of the project, containing the controllers and other objects associated with the server.

### WebApi/Controllers
#### WeatherForecastController
Sample controller with implemented Long Polling 

### WebApi/WeatherForecastStorage
#### WeatherForecastRequestStorage
Stores incoming weather forecast requests and implements response waiting mechanism

#### WeatherForecastStorage
Manages weather forecast requests and responses and implements all controller methods

## WebApiClient
Part of the project which contains an example of a client for API testing.

#### ApiClient
A client in which are implemented calls to all API methods (wraps ApiHelper).

#### Program
A simple console app designed for testing purpuses.

Any questions and suggestions are welcome in **Discussions** section
