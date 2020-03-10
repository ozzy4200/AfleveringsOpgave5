using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AfleveringsOpgave5
{
    class Program
    {
        public static void DoClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            Bog returnerBog = JsonConvert.DeserializeObject<Bog>(sr.ReadLine());

            string line = sr.ReadLine();
            string answer = "";

            while (line != null && line != "")
            {
                Console.WriteLine("Bog information" + "" + line);
                answer = line.ToUpper();
                sw.WriteLine(answer);
                line = sr.ReadLine();
                returnerBog = JsonConvert.DeserializeObject<Bog>(sr.ReadLine());
                line = returnerBog.ToString();

                string[] lineArray = line.Split("");
                string param = line.Substring(line.IndexOf("") + 1);
                string command = lineArray[0];

                switch (command)
                {
                    // henter alle bøger
                    case "GetAll":
                        sw.WriteLine("Hent alle bøger");
                        sw.WriteLine(JsonConvert.SerializeObject(bogs));
                        break;

                    // Henter isbn nummeret
                    case "Get":
                        sw.WriteLine("Hent min bog og isbn" + lineArray[1] + bogs);
                        sw.WriteLine(JsonConvert.SerializeObject(bogs.Find(bog =>bog.Isbn13 == param)));
                        break;

                    // gemmer bog

                    case "Save":
                        sw.WriteLine("Gem en bog");
                        Bog gemBog = JsonConvert.DeserializeObject<Bog>(param);
                        bogs.Add(gemBog);
                        break;

                    // kigger hvis en søgning er forkert

                    default:
                        sw.WriteLine("Du har søgt på en forkert bog");
                        break;
                }

                sr.ReadLine();
            }   

            ns.Close();
            socket.Close();
        }

        private static List<Bog> bogs = new List<Bog>()
        {
            new Bog("Osmans bog", "Osman", 367, "0123456789111"),
            new Bog("Francos bog", "Franco", 123, "0123456789444"),
            new Bog("Daniels bog", "Daniel", 345, "0123456734555"),
            new Bog("Magnus bog", "Magnus", 456, "0123456734666")
        };

        static void Main(string[] args)
        {
            // opretter ip addresse og port nr
            TcpListener listener = new TcpListener(IPAddress.Loopback, port: 4646);
            listener.Start();
            Console.WriteLine("Server Started");

            while (true)
            {
                Task.Run(() =>
                {
                    // venter på at den connecter 
                    TcpClient connectionSocket = listener.AcceptTcpClient();
                    TcpClient tempSocket = connectionSocket;
                    Console.WriteLine("Server activated");

                    // Kalder metoden DoClient
                    DoClient(tempSocket);

                });
            }

            TcpClient socket = listener.AcceptTcpClient();

            socket.Close();
            listener.Stop();
        }
    }
}
