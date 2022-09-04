using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Transport;

namespace eckumoc_netcore_httpclient
{
    public class HttpProgram//: EcKuMoC
    {


        public static void Main(string[] args) => OnStart(args);
        public static void OnStart(string[] args)
        {

           
            if (args.Length == 0)
            {
                
                Console.WriteLine("httpclient ");
                Console.WriteLine("parameters: ");
                Console.WriteLine("  1) method: 'get'|'post'|'put'|'delete'|'options'");
                Console.WriteLine("  2) url:    '[protocol]://[host]/[uri-segment-1]/[uri-segment-n]?[param-1-key]=[param1-value]&[param-n-key]=[param-n-value]' ");





            }
            else
            {
                foreach (var item in args)
                {
                    System.Console.WriteLine(item);
                }
                if(args.Length!=1)
                    throw new System.Exception("Требуется 1 аргумента, я получил "+args.Length);
                Console.WriteLine(
                    HttpService.RequestAsync("GET", args[0]).Result);
                Console.WriteLine( HttpService.RequestAsync( "", args[0] ).Result);
            }
        }
    }
}
