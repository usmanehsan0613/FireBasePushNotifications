using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class zu_push_notifications_server_TokenReceive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static JsResponse procRequest(string FCMtoken)
    {
        // string location = HttpContext.Current.Request.QueryString["ven"].ToString();

        JsResponse res = new JsResponse();

        res.html = "Receive " + FCMtoken;
        res.responseCode = 200;
        int insertId = 0;
        string token = FCMtoken;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            con.Open();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = con;

            // -- first check if same reocrd exists or not. i am checking with phone no. 
            string query = " SELECT Id FROM firebase_tokens WHERE FCM_Token = @FFCM_Token";
            comm.CommandText = query;
            comm.Parameters.AddWithValue("@FFCM_Token", FCMtoken);
            int oldid = Convert.ToInt32(comm.ExecuteScalar());

            if (oldid > 0)
            {
                res.html = "No action required.";
                res.responseCode = 200;
                return res;
            }

            string sql = " INSERT INTO firebase_tokens (FCM_Token) VALUES (@FCM_Token) ";
            comm.CommandText = sql;
            comm.Parameters.AddWithValue("@FCM_Token", FCMtoken);

            try
            {
                insertId = Convert.ToInt32(comm.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                res.html = ex.Message;
                res.responseCode = 500;
            }
            con.Close();
        }


        if (insertId > 0)
        {
            res.html = "Token added successfully.";
            res.responseCode = 200;
        }

        return res;
    }

}