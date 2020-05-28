using Flurl.Http;
using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using fondomerende.Main.Manager;

namespace fondomerende.Main.Services
{
    static class Services
    {
        public static bool test = true;    //switch tra server di prova e quello autentico
        
        private readonly static string protocollo = "http://";
        private readonly static string basePath = "fondomerende.madeinapp.net/api";
        private readonly static string authkey = "pxzkCBlHelBYCWho5qMk0kxaA2H8SAph8W";
        private static string token = UserManager.Instance.token;
        private readonly static string content = "application/x-www-form-urlencoded; param=value;charset=UTF-8";
        private readonly static string basePathTest = "192.168.0.191:8888/fondomerende/public/process-request.php";
        private readonly static string authkeyTest = "metticiquellochetipare";

        public static string GetAuthKey()
        {
            if (test) return authkeyTest;
            else return authkey;
        }
        public static string Concatenazione(string get = "")
        {
            if (test) return protocollo + basePathTest + get;
            return protocollo + basePath + get;
        }
        public static string LoginUrlRequest()
        {
            string app = Concatenazione();

            Cookie authKey = new Cookie("auth-key", Services.authkey);

            if(test) app.WithCookie("auth-key", authkey)
                        .WithHeader("Content-Type", content);

            else app.WithCookie("auth-key", authkeyTest)
                    .WithHeader("Content-Type", content);


            return app;
        }


        public static string GenericUrlRequest()
        {
            string app = Concatenazione();
            Cookie authKey = new Cookie("auth-key", Services.authkey);
            Cookie token = new Cookie("user-token", Services.token);

            app.WithCookies(new { authKey, token })
               .WithHeader("Content-Type", content);

            return app;
        }
    }
}
