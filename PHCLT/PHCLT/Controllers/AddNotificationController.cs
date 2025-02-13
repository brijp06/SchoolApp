using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Data;

namespace PHCLT.Controllers
{
    public class AddNotificationController : Controller
    {
        // GET: AddNotification
        ClsSystem ob = new ClsSystem();
        public async Task<ActionResult> Index()
        {
            string token = await GetAccessToken();
            ViewBag.Token = token; // Pass token to View for display
            return View();
        }


        public static async Task<string> GetAccessToken()
        {
            string jsonPath = System.Web.HttpContext.Current.Server.MapPath("~/Token/chankyaschool-6f741-2ee9d97dac93.json");  // Update this with the correct path

            GoogleCredential credential;
            using (var stream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(new[] { "https://www.googleapis.com/auth/firebase.messaging" });
            }

            var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
            return token;
        }




        [HttpPost]
        public async Task<JsonResult> Sendmessage(string token, string msg, string deviceToken)
        {
            string fcmUrl = "https://fcm.googleapis.com/v1/projects/chankyaschool-6f741/messages:send";

            if (string.IsNullOrEmpty(token))
            {
                return Json(new { success = false, message = "Access token is required." }); 
            }

            if (string.IsNullOrEmpty(deviceToken))
            {
                return Json(new { success = false, message = "Device token is required." });
            }
            DataTable dt = ob.Returntable("select * from WebPushToken");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                var messagePayload = new
                {
                    message = new
                    {
                        token = dt.Rows[i]["DeviceToken"].ToString(), // Use the actual device token from the request
                        notification = new
                        {
                            title = "Chankya School",
                            body = msg // Use the message parameter from the request
                        },
                        webpush = new
                        {
                            headers = new { Urgency = "high" },
                            notification = new
                            {
                                body = msg,
                                requireInteraction = true,
                                badge = "/badge-icon.png"
                            }
                        }
                    }
                };
                // token = "BD2_3iTk6XS9q1Mq72jOkSM4HyB2nmoygX3E4L8xa9jnU6xSevUyV_g-EJM-rMEblr5ORMw1dP4_tV7WjDYYOH0";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var json = JsonConvert.SerializeObject(messagePayload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(fcmUrl, content);
                    string responseText = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        //return Json(new { success = true, message = "Notification sent successfully!", response = responseText });
                    }
                    else
                    {
                    }
                }

            }
            return Json(new { success = false, message = "" });

        }


    }
}