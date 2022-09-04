using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




public  class HttpModule
{
    public static async Task SendText(HttpContext http, Encoding encoding, string text )
    {
        http.Response.ContentType = $"text/html; charset={encoding.WebName}";
        await http.Response.WriteAsync(text);
    }
    public static Task SendHtml(HttpContext http, string text)
    {
        return SendText(http, Encoding.Default, text); 
    }
}