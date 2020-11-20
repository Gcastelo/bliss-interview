# Bliss API

## Introduction

Welcome to the Bliss API. This project was developed using Visual Studio Code, .net core 5 and mongo.

The purpose of this API is to allow saving and retrieving questions in a similar way that the mock API works, but using real data and database.

Swagger is included in order to navigate the API, by going to http://localhost:5001/swagger/index.html

Note: Mongo is running in a free Mongo Atlas cluster, there's no need to install it locally.

## How to run

1. Install .net core skd 5 https://dotnet.microsoft.com/download/dotnet/5.0

1. Clone repository from https://github.com/Gcastelo/bliss-interview

1. Execute ```dotnet build```

1. Go to ```Bliss.Api.Tests``` and run ```dotnet test```

1. Go to ```Bliss.Api``` and run ```dotnet run```

1. Open http://localhost:5001/swagger/index.html
