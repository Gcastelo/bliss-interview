# Bliss API

## Introduction

Welcome to the Bliss API. This project was developed using Visual Studio Code, .net core 5, mongo and docker.

The purpose of this API is to allow saving and retrieving questions in a similar way that the mock API works, but using real data and databases.

Swagger is included in order to navigate the API, by going to http://localhost:5001/swagger/index.html


## Endpoints

TODO...




## Build an ASP.NET Core image

You can build and run a .NET-based container image using the following instructions:

```console
docker build --pull -t aspnetapp .
docker run --rm -it -p 8000:80 aspnetapp  
```

You should see the following console output as the application starts:

```console
> docker run --rm -it -p 8000:80 aspnetapp
Hosting environment: Production
Content root path: /app
Now listening on: http://[::]:80
Application started. Press Ctrl+C to shut down.
```

After the application starts, navigate to `http://localhost:8000` in your web browser.

> Note: The `-p` argument maps port 8000 on your local machine to port 80 in the container (the form of the port mapping is `host:container`). See the [Docker run reference](https://docs.docker.com/engine/reference/commandline/run/) for more information on command-line parameters. In some cases, you might see an error because the host port you select is already in use. Choose a different port in that case.

You can also view the ASP.NET Core site running in the container on another machine. This is particularly useful if you are wanting to view an application running on an ARM device like a Raspberry Pi on your network. In that scenario, you might view the site at a local IP address such as `http://192.168.1.18:8000`.

In production, you will typically start your container with `docker run -d`. This argument starts the container as a service, without any console interaction. You then interact with it through other Docker commands or APIs exposed by the containerized application.

We recommend that you do not use `--rm` in production. It cleans up container resources, preventing you from collecting logs that may have been captured in a container that has either stopped or crashed.

> Note: See [Establishing docker environment](../establishing-docker-environment.md) for more information on correctly configuring Dockerfiles and `docker build` commands.

