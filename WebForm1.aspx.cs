using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//use NuGet to instal Newtownsoft.Json
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Account
{
    public string Email { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedDate { get; set; }
    public IList<string> Roles { get; set; }
}

/*to work with JSON https://www.c-sharpcorner.com/article/how-to-work-with-json-in-Asp-Net/
To use the json.net nuGet package, I had to target framework 4.7
https://www.newtonsoft.com/json/help/html/SerializingJSON.htm
*/

/*I am confused.  In Arduino, you have C objects and you serialise/ deserialise
DynamicJsonBuffer jsonBuffer;
JsonObject& root = jsonBuffer.parseObject(payload);   where payload is the incoming string
we now have an object, you can pull strings out of it
String sType=root["type"];
and you can serialise your own object
DynamicJsonBuffer jsonBuffer;
  JsonObject& root = jsonBuffer.createObject();
  JsonObject& data = root.createNestedObject("data");
  root["type"] = "turnout";
  data["name"] = turnout[k].name;
  data["state"] = turnout[k].thrown?4:2;
  root["address"] = turnout[k].address;  
  String out;
  root.printTo(out);   //out string now contains the json string


    2019-10-03 i think its because the arduino routines treat the type as a string and then late-coerce to different types
    e.g. if it appears numeric, it gets converted to a numeric.  I work with the string forms anyway as i don't want "" to become 0


 */













namespace SimpleWS2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-serialize-and-deserialize-json-data
            //https://docs.microsoft.com/en-us/dotnet/framework/wcf/samples/json-serialization
            thingy();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //this deserializer needs to know the object structure
            //     Account m = JsonConvert.DeserializeObject<Account>(TextBox1.Text);

            //object j  = JsonConvert.DeserializeObject<object>(TextBox1.Text);
            //but how do you check a member exists?


            //string json = @"['Starcraft','Halo','Legend of Zelda']";
            //            List<string> videogames = JsonConvert.DeserializeObject<List<string>>(TextBox1.Text);

            //https://stackoverflow.com/questions/21246609/deserializing-an-unknown-type-in-json-net
            //needs Newtonsoft.Json.Linq

            /*
            IDictionary<string, JToken> Jsondata = JObject.Parse(TextBox1.Text);
            foreach (KeyValuePair<string, JToken> element in Jsondata)
            {
                string innerKey = element.Key;
                if (element.Value is JArray)
                {
                    // Process JArray
                    Response.Write("Array<br/>");
                    
                }
                else if (element.Value is JObject)
                {
                    // Process JObject
                    Response.Write("obj<br/>");
                }
            }
            */

            //this might do it?
            //YES this works, will parse to a JObject (which is a Linq construct) and 
            //you can then cast the "Email" member of this to a string.
            //https://www.newtonsoft.com/json/help/html/QueryingLINQtoJSON.htm
            JObject rss = JObject.Parse(TextBox1.Text);
            string rssTitle = (string)rss["Email"];

            //nested things are thus string rssTitle = (string)rss["Email"]["Nested"];
            //but it may be a Child... need to check

            Response.Write(rssTitle);


        }



        //https://www.newtonsoft.com/json/help/html/SerializeObject.htm
        public void thingy()
        {

            Account account = new Account
            {
                Email = "james@example.com",
                Active = true,
                CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                Roles = new List<string>
    {
        "User",
        "Admin"
    },

            };

            string json = JsonConvert.SerializeObject(account, Formatting.Indented);
            // {
            //   "Email": "james@example.com",
            //   "Active": true,
            //   "CreatedDate": "2013-01-20T00:00:00Z",
            //   "Roles": [
            //     "User",
            //     "Admin"
            //   ]
            // }

            TextBox1.Text = json;
            Console.WriteLine(json);
        }

    }
}