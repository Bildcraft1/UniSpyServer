﻿using System;
using System.Reflection;
using StackExchange.Redis;
using UniSpyLib.Abstraction.Interface;
using UniSpyLib.Database;
using UniSpyLib.Database.DatabaseModel.MySql;
using UniSpyLib.Extensions;
using UniSpyLib.MiscMethod;
using UniSpyLib.UniSpyConfig;


namespace UniSpyLib.Abstraction.BaseClass
{
    public abstract class UniSpyServerFactoryBase
    {
        public static readonly string RetroSpyVersion = "0.5.3";
        public static ConnectionMultiplexer Redis { get; protected set; }
        public static IUniSpyServer Server { get; protected set; }
        public static readonly string ServerName = Assembly.GetEntryAssembly().GetName().Name;
        public UniSpyServerFactoryBase()
        {
        }

        public virtual void Start()
        {
            ShowRetroSpyLogo();
            LoadDatabaseConfig();
            LoadServerConfig();
            UniSpyJsonConverter.Initialize();
        }

        protected void LoadServerConfig()
        {
            //Add all servers
            foreach (UniSpyServerConfig cfg in ConfigManager.Config.Servers)
            {
                StartServer(cfg);
            }
            Server.Start();

            var table = new ConsoleTables.ConsoleTable("Server Name", "Listening Address", "Listening Port");
            table.AddRow(ServerName, Server.Endpoint.Address, Server.Endpoint.Port);
            table.Write(ConsoleTables.Format.Alternative);
            Console.WriteLine("Server successfully started! ");
        }

        /// <summary>
        /// Over write the specific functions you want to start the server
        /// You can start all servers if you want
        /// </summary>
        /// <param name="cfg"></param>
        protected abstract void StartServer(UniSpyServerConfig cfg);

        protected void LoadDatabaseConfig()
        {
            //Determine which database is used and establish the database connection.
            switch (ConfigManager.Config.Database.Type)
            {
                case DatabaseType.MySql:
                    retrospyContext.RetroSpyMySqlConnStr =
                    $"Server={ConfigManager.Config.Database.RemoteAddress};"
                    + $"Database={ConfigManager.Config.Database.DatabaseName};"
                    + $"Uid={ConfigManager.Config.Database.UserName};"
                    + $"Pwd={ConfigManager.Config.Database.Password};"
                    + $"Port={ConfigManager.Config.Database.RemotePort};"
                    + $"SslMode={ConfigManager.Config.Database.SslMode};"
                    + $"SslCert={ConfigManager.Config.Database.SslCert};"
                    + $"SslKey={ConfigManager.Config.Database.SslKey};"
                    + $"SslCa={ConfigManager.Config.Database.SslCa}";
                    break;
            }

            try
            {
                new retrospyContext().Database.CanConnect();
            }
            catch (Exception e)
            {
                throw new Exception($"Can not connect to {ConfigManager.Config.Database.Type}!", e);
            }

            Console.WriteLine($"Successfully connected to {ConfigManager.Config.Database.Type}!");

            try
            {
                Redis = ConnectionMultiplexer.Connect(
                    $"{ConfigManager.Config.Redis.RemoteAddress}:{ConfigManager.Config.Redis.RemotePort}");
            }
            catch (Exception e)
            {
                throw new Exception("Can not connect to Redis", e);
            }
            Console.WriteLine($"Successfully connected to Redis!");
        }

        public static void ShowRetroSpyLogo()
        {
            // the ascii art font name is "small"
            Console.WriteLine(@" _   _      _ ___           ___ ");
            Console.WriteLine(@"| | | |_ _ (_) __|_ __ _  _/ __| ___ _ ___ _____ _ _ ");
            Console.WriteLine(@"| |_| | ' \| \__ \ '_ \ || \__ \/ -_) '_\ V / -_) '_|");
            Console.WriteLine(@" \___/|_||_|_|___/ .__/\_, |___/\___|_|  \_/\___|_|");
            Console.WriteLine(@"                 |_|   |__/ ");
            Console.WriteLine(@"Version: " + RetroSpyVersion);
        }
    }
}
