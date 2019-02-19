using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Web;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;

namespace 学校签到
{
    class Program
    {
        /// ========================================程序配置参数区开始

        //接口生产地址(应用上线后正式环境必须使用该地址)
        //private  static String url = "http://www.etuocloud.com/gateway.action";

        //接口测试地址（未上线前测试环境使用）
        private static String url = "http://www.etuocloud.com/gatetest.action";

        //应用 app_key
        private static String APP_KEY = "cmm97jEzPJ7DTfIwcd0c04EnLBUtHZDa";
        //应用 app_secret
        private static String APP_SECRET = "Naolrqc7LKstKApsB2tslY1LRebTIaT1kEjlk085uzXEEPstxFvplCZM4iBRoiFG";

        //接口响应格式 json或xml
        private static String FORMAT = "json";

        /// ========================================程序配置参数区结束
        static void Main(string[] args)
        {

            DataTable dt = new DataTable();
            string path = ConfigurationManager.AppSettings["EmailUrl"].ToString();
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            dt = ExeclHelper.ExcelToTable(dir + @"\cs.xlsx");
            Console.WriteLine(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string str;
                string username = dt.Rows[i]["name"].ToString();
                string pwd = dt.Rows[i]["pwd"].ToString();
                username = "201663450632";
                pwd = "144145";
                StringBuilder sb = new StringBuilder();
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
                {
                    Console.WriteLine("操作失败，用户名或者密码为空");
                    Console.ReadKey();
                }
                //获取Token
                string token = GetToken(username, pwd, out str);
                username = str; //防止输入学号
                if (string.IsNullOrEmpty(token))
                {
                    sb.Append("获取到Token失败");
                    return;
                }

                //将Token同步到服务器
                if (!DoServerToken(token))
                    sb.Append("将Token同步到服务器失败");
                //签到
                if (!DoSign(
                    token,
                    username,
                    string.IsNullOrEmpty(dt.Rows[i]["lat"].ToString()) ? "22.495777" : dt.Rows[i]["lat"].ToString(),
                    string.IsNullOrEmpty(dt.Rows[i]["lng"].ToString()) ? "113.91978" : dt.Rows[i]["lng"].ToString(),
                    string.IsNullOrEmpty(dt.Rows[i]["signAddr"].ToString()) ? "南山大厦-广东省深圳市南山区南海大道1065号" : dt.Rows[i]["signAddr"].ToString()))
                    sb.Append("签到失败");
                //发送日志
                if (!DoLog(
                    token, 
                    username, 
                    string.IsNullOrEmpty(GetRandomChinese(30)) ? "日志日志日志日志日志日志日志日志日志日志日志日志日志日志日志日志日志日志" : GetRandomChinese(30)))
                    sb.Append("发送日志失败");



                if (sb.Length == 0)
                {
                    Console.WriteLine("成功");
                }
                else
                {
                    Console.WriteLine(sb.ToString());
                }

            }

            Console.ReadKey();
        }

        public static string GetRandomChinese(int strlength)
        {
            // 获取GB2312编码页（表） 
            Encoding gb = Encoding.GetEncoding("gb2312");

            object[] bytes = CreateRegionCode(strlength);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < strlength; i++)
            {
                string temp = gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[])));
                sb.Append(temp);
            }

            return sb.ToString();
        }

        /** 
        此函数在汉字编码范围内随机创建含两个元素的十六进制字节数组，每个字节数组代表一个汉字，并将 
        四个字节数组存储在object数组中。 
        参数：strlength，代表需要产生的汉字个数 
        **/
        private static object[] CreateRegionCode(int strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素 
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            Random rnd = new Random();

            //定义一个object数组用来 
            object[] bytes = new object[strlength];

            /**
             每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bytes数组中 
             每个汉字有四个区位码组成 
             区位码第1位和区位码第2位作为字节数组第一个元素 
             区位码第3位和区位码第4位作为字节数组第二个元素 
            **/
            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位 
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();

                //区位码第2位 
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i); // 更换随机数发生器的 种子避免产生重复值 
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //区位码第3位 
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //区位码第4位 
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();

                // 定义两个字节变量存储产生的随机汉字区位码 
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                // 将两个字节变量存储在字节数组中 
                byte[] str_r = new byte[] { byte1, byte2 };

                // 将产生的一个汉字的字节数组放入object数组中 
                bytes.SetValue(str_r, i);
            }

            return bytes;
        }


        public static string HttpGet(HttpWebRequest request)
        {
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string responseText = myreader.ReadToEnd();
                myreader.Close();
                return responseText;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static string HttpPost(string url, string paramData, Dictionary<string, string> headerDic = null)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "POST";
                wbRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                wbRequest.ContentLength = Encoding.UTF8.GetByteCount(paramData);
                if (headerDic != null && headerDic.Count > 0)
                {
                    foreach (var item in headerDic)
                    {
                        wbRequest.Headers.Add(item.Key, item.Value);
                    }
                }
                using (Stream requestStream = wbRequest.GetRequestStream())
                {
                    using (StreamWriter swrite = new StreamWriter(requestStream))
                    {
                        swrite.Write(paramData);
                    }
                }
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sread = new StreamReader(responseStream))
                    {
                        result = sread.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                result = "日志发送失败";
            }

            return result;
        }



        /// <summary>
        /// 实现URL编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }

        /// <summary>
        /// 根据账号获取token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private static string GetToken(string username, string pwd, out string str)
        {

            string Url = "http://e.yvtc.edu.cn:12800/WCF/JCSJ/User/Login";
            string QueryString = "jsoncallback=jQuery183014759118188133535_1539057122764&para={\"LOGIN_NAME\":\"" + username + "\",\"LOGIN_PWD\":\"" + pwd + "\"}&_=" + (long)(DateTime.Now - new System.DateTime(1970, 1, 1, 8, 0, 0)).TotalMilliseconds;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + "?" + QueryString);
            request.Method = "GET";

            //获取token
            string tokenStr = HttpGet(request);
            string token = "";
            if (tokenStr.IndexOf("token") >= 0)
            {
                token = tokenStr.Substring(tokenStr.IndexOf("token") + 10, 32);
                str = tokenStr.Substring(tokenStr.IndexOf("XH") + 7, 12);
                return token;
            }
            else
            {
                str = "";
                return token;
            }

        }

        /// <summary>
        /// 将Token同步到服务器
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool DoServerToken(string token)
        {
            try
            {
                //把token传给服务器
                string Url = "http://e.yvtc.edu.cn/subsystems.html";
                string QueryString = "token=" + token;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + "?" + QueryString);
                request.Method = "GET";
                request.Referer = "http://e.yvtc.edu.cn/login.html";

                string responStr = HttpGet(request);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 发送日志
        /// </summary>
        /// <param name="token"></param>
        /// <param name="username"></param>
        public static bool DoLog(string token, string username, string logText)
        {
            try
            {
                string para = "{\"BATCH_ID\":16,\"RECORDER_TYPE\":0,\"USER_CODE\":\"" + username + "\",\"LOG_TITLE\":\"日志\",\"LOG_CONTENT\":\"" + logText + "\"}";
                string url = "http://e.yvtc.edu.cn:805/WebAPI/Supervise/LogAPI.aspx";
                string result = HttpPost(url, "para=" + UrlEncode(para) + "&token=" + token + "&func=add");
                if (string.IsNullOrEmpty(result))
                    return false;

                if (result.Contains("成功"))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 发送签到请求
        /// </summary>
        /// <param name="token"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool DoSign(string token, string username, string lat, string lng, string signAddr)
        {
            try
            {
                string para = UrlEncode("{\"BATCH_ID\":16,\"USER_CODE\":\"" + username + "\",\"LOCATION_JSON\":\"{\\\"lat\\\":\\\"" + lat + "\\\",\\\"lng\\\":\\\"" + lng + "\\\"}\",\"SIGNIN_ADDR\":\"" + signAddr + "\"}");//如需更换位置，要自己抓取lat ng signin_addr 参数值

                string QueryString = "jsoncallback=jQuery1830626980813438146_1547601564345&para=" + para + "&token=" + token + "&_=" + (long)(DateTime.Now - new System.DateTime(1970, 1, 1, 8, 0, 0)).TotalMilliseconds;
                string Url = "http://e.yvtc.edu.cn:12805/WCF/DGSX/SIGNIN/AddItem";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + "?" + QueryString);
                request.Method = "GET";
                string responStr = HttpGet(request);


                if (string.IsNullOrEmpty(responStr))
                    return false;
                if (responStr.Contains("成功"))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="to">电话号</param>
        /// <param name="template">短信模板ID</param>
        /// <param name="smscode">验证码</param>
        /// <param name="appkey"></param>
        /// <param name="formmat"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string sendText(string to, int template, string smscode)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("app_key", APP_KEY);
            parameters.Add("view", FORMAT);
            parameters.Add("method", "cn.etuo.cloud.api.sms.simple");
            parameters.Add("out_trade_no", "");//商户订单号，可空
            parameters.Add("to", to);
            parameters.Add("template", template.ToString());
            parameters.Add("smscode", smscode);
            parameters.Add("sign", getsign(parameters));
            return HttpClient.HttpPost(url, parameters);
        }

        /// <summary>
        /// 获取param签名
        /// </summary>
        /// <param name="sParams"></param>
        /// <returns></returns>
        private static string getsign(NameValueCollection parameters)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
            foreach (string key in parameters.Keys)
            {
                sParams.Add(key, parameters[key]);
            }

            string sign = string.Empty;
            StringBuilder codedString = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in sParams)
            {
                if (temp.Value == "" || temp.Value == null || temp.Key.ToLower() == "sign")
                {
                    continue;
                }

                if (codedString.Length > 0)
                {
                    codedString.Append("&");
                }
                codedString.Append(temp.Key.Trim());
                codedString.Append("=");
                codedString.Append(temp.Value.Trim());
            }

            // 应用key
            codedString.Append(APP_SECRET);
            string signkey = codedString.ToString();
            sign = GetMD5(signkey, "utf-8");

            return sign;
        }


        private static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用XXX编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = System.Text.Encoding.UTF8.GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();

            //  return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ConvertString, "MD5").ToLower(); ;

            return retStr;
        }


    }






    public class HttpClient
    {
        /// <summary>
        /// POST请求与获取结果  
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string HttpPost(string Url, NameValueCollection parameters)
        {
            return HttpPost(Url, toParaData(parameters));
        }



        //调用http接口,接口编码为utf-8
        private static string toParaData(NameValueCollection parameters)
        {

            //设置参数，并进行URL编码
            StringBuilder codedString = new StringBuilder();
            foreach (string key in parameters.Keys)
            {
                // codedString.Append(HttpUtility.UrlEncode(key));
                codedString.Append(key);
                codedString.Append("=");
                codedString.Append(HttpUtility.UrlEncode(parameters[key], System.Text.Encoding.UTF8));
                codedString.Append("&");
            }
            string paraUrlCoded = codedString.Length == 0 ? string.Empty : codedString.ToString().Substring(0, codedString.Length - 1);


            return paraUrlCoded;
        }


        /// <summary>  
        /// POST请求与获取结果  
        /// </summary>  
        public static string HttpPost(string Url, string postDataStr)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            //request.ContentLength = postDataStr.Length;
            //StreamWriter writer = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8);
            // writer.Write(postDataStr);
            // writer.Flush();


            //将URL编码后的字符串转化为字节
            byte[] payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();

            //获得响应流
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));

            string retString = reader.ReadToEnd();
            return retString;
        }



    }
}
