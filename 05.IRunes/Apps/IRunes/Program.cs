﻿using System;
using IRunes.Controllers;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Results;
using SIS.WebServer.Routing;

namespace CakesWebApp
{
    class Program
    {
        static void Main()
        {
            var serverRoutingTable = new ServerRoutingTable();

            ConfigureRoutes(serverRoutingTable);

            var server = new Server(1984, serverRoutingTable);
            server.Run();
        }

        private static void ConfigureRoutes(ServerRoutingTable serverRoutingTable)
        {
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = req => new HomeController().Index(req);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/home/index"] = req => new RedirectResult("/");

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/login"] = req => new UsersController().Login(req);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/register"] = req => new UsersController().Register(req);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/login"] = req => new UsersController().DoLogin(req);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/register"] = req => new UsersController().DoRegister(req);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/logout"] = req => new UsersController().Logout(req);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/all"] = req => new AlbumsController().All(req);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/create"] = req => new AlbumsController().Create(req);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/albums/create"] = req => new AlbumsController().DoCreate(req);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/details"] = req => new AlbumsController().Details(req);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/tracks/create"] = req => new TracksController().Create(req);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/tracks/create"] = req => new TracksController().DoCreate(req);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/tracks/details"] = req => new TracksController().Details(req);
        }
    }
}
