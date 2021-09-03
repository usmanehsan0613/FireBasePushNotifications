using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public partial class zu_push_notifications_admin_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public static bool validateCaptcha(string captcha)
    {
        string Response = captcha;  
        bool Valid = false;
        //Request to Google Server
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create
        (" https://www.google.com/recaptcha/api/siteverify?secret=XXXX-your-secret-here&response=" + Response);
        try
        {
            //Google recaptcha Response
            using (WebResponse wResponse = req.GetResponse())
            {
                using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    MyObject data = js.Deserialize<MyObject>(jsonResponse);// Deserialize Json
                    Valid = Convert.ToBoolean(data.success);
                }
            }

            return Valid;
        }
        catch (WebException ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static JsResponse procFrm(FCMNotification notif)
    {
        // string location = HttpContext.Current.Request.QueryString["ven"].ToString();
        
        JsResponse res = new JsResponse();

        res.html = "Receive";
        res.responseCode = 200;


        if (!validateCaptcha(notif.captcha))
        {
            res.responseCode = 0;
            res.html = "Invalid Captcha.";
            res.html = Resources.Resource.serverErrorRobot.ToString();
            //return res;
        }

        if (notif.title != null && notif.url != null && notif.redirectUrl != null && notif.message != null && notif.key != null)
        {
            res.html = "Working...";
            FCMNotification fcm = new FCMNotification();
            ///DataTable dt = fcm.readTokens();
            ///
            res.html = SendMessage();
            res.responseCode = 1;
            

        }
        else
        {
            res.responseCode = 0;
            res.html = "Error saving information.Please enter all the required fields.";
        }


        return res;
    }


    public static string SendMessage()
    {
        string serverKey = FCMNotification.SERVER_KEY;

        try
        {
            var result = "-1";
            var webAddr = "https://fcm.googleapis.com/fcm/send";

            var regID = "phoneRegID";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"to\": \"topics/all\"\",\"notification\": {\"title\": \"WelcomeZU\",\"body\": \"Welcome to ZU!\"},\"priority\":10}";
                //registration_ids, array of strings -  to, single recipient
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return "err";
        }
    }
}