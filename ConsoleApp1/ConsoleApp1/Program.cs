using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			WebRequest request = WebRequest.Create(@"http://localhost:2058/testapi.aspx");
			request.Method = "POST";
			request.ContentType = "application/html";//x-ww-form-urlencoded";
													 //request.ContentType = "application/x-ww-form-urlencoded";
			request.Timeout = 300000;
			//request.ReadWriteTimeout = 300000;

			Dictionary<string, object> dic = new Dictionary<string, object>();

			Dictionary<string, string> dic2 = new Dictionary<string, string>();
			dic2["user_logname"] = "admin";
			dic2["user_pwd"] = "admin321";
			dic["head"] = dic2;


			var body = JsonConvert.SerializeObject(dic);

			JObject o = JObject.Parse(body);

			var vv = o["head"];
			string vv2 = vv["user_pwd"].ToString();


			byte[] dataArray = Encoding.UTF8.GetBytes(body);
			request.ContentLength = dataArray.Length;

			Encoding encoding = Encoding.GetEncoding("utf-8");

			Stream myRequestStream = request.GetRequestStream();
			myRequestStream.Write(dataArray, 0, dataArray.Length);
			myRequestStream.Close();

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream myResponseStream = response.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myResponseStream,
				encoding);

			string retString = myStreamReader.ReadToEnd();
			myStreamReader.Close();
			myResponseStream.Close();
		}
	}
}
