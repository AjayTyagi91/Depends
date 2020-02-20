using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Microsoft.Azure.NotificationHubs;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using risersoft.shared.cloud;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace PushNotificationsTest
{
   public class Program
    {
         private static void ADDPush()
         {
            string responseFromServer = null;
            string json = @"{
                'notification_content' : {
                    'name' : 'Project Nirvana',
                    'title' : 'Testing Notifications',
                    'body' : 'Update your Application',
                    'custom_data' : { 'key1' : 'val1', 'key2' : 'val2' }
                     }
                   }";
            Uri baseUrl = new Uri("https://appcenter.ms/api/v0.1/apps/ajay.tyagi-risersoft.com/Testing-Push-Notifications/push/notifications");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
            request.Method = "POST";
            request.Headers.Add("X-API-Token", "792496abe0db4a2927773de1c9d21597ca2b3cb7");
            request.Accept = "/";
            request.UseDefaultCredentials = true;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            Console.ReadLine();
            Console.WriteLine(responseFromServer);
        }
        static void Main(string[] args)
        {
            Push();
           // ADDPush();
            Console.WriteLine("Hello World!");

        }
        public static async void Push()
        {
            string title = "Abc";
            string message = "App Updated";
            Dictionary<string, string> customData = new Dictionary<string, string>();
            customData.Add("key", "value");
            customData.Add("key1", "value");
            customData.Add("key2", "value");
            string AppName = "ajay.tyagi-risersoft.com";
            string OrgName = "TestingNotifications";
            string AccessToken = "dcb8305046cdd4b20e79216d6a9116ef3e152814";
            var push = new clsAppCenterPush(AccessToken, "", AppName, OrgName);
            var success = await push.Notify(title, message, customData);
        }
    }
}
