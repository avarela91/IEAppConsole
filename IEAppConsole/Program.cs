using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace IEAppConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            uploadFile1ByFTP();
            uploadFile2ByFTP();
            
        }

       
        public static void uploadFile1ByFTP()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["urlFtp"].ToString();
                string FileName = ConfigurationManager.AppSettings["FileName"].ToString(); 
                string file = ConfigurationManager.AppSettings["file"].ToString();
                string username = ConfigurationManager.AppSettings["usernameFTP"].ToString();
                string password = ConfigurationManager.AppSettings["passwordFTP"].ToString();

                if (!File.Exists(file))
                {
                    return;
                }


                String uploadUrl = String.Format("{0}/{1}/{2}", url, "aplicacion", FileName);

                FtpWebRequest request =
                    (FtpWebRequest)WebRequest.Create(uploadUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(username, password);

                byte[] bytes = System.IO.File.ReadAllBytes(file);

                request.ContentLength = bytes.Length;
                using (Stream request_stream = request.GetRequestStream())
                {
                    request_stream.Write(bytes, 0, bytes.Length);
                    request_stream.Close();
                }
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine("Complete, status {0}", response.StatusDescription);
            }
            catch (WebException e)
            {
                
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                Console.WriteLine(e.Message+" "+status);
                uploadFile1ByFTP();
            }

        }
        public static void uploadFile2ByFTP()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["urlFtp"].ToString();
                string FileName = ConfigurationManager.AppSettings["FileName2"].ToString(); ;
                string file = ConfigurationManager.AppSettings["file2"].ToString();
                string username = ConfigurationManager.AppSettings["usernameFTP"].ToString();
                string password = ConfigurationManager.AppSettings["passwordFTP"].ToString();

                String uploadUrl = String.Format("{0}/{1}/{2}", url, "aplicacion", FileName);
                FtpWebRequest request =
                    (FtpWebRequest)WebRequest.Create(uploadUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials =
                    new NetworkCredential(username, password);

                byte[] bytes = System.IO.File.ReadAllBytes(file);

                request.ContentLength = bytes.Length;
                using (Stream request_stream = request.GetRequestStream())
                {
                    request_stream.Write(bytes, 0, bytes.Length);
                    request_stream.Close();
                }
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine("Complete, status {0}", response.StatusDescription);
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                Console.WriteLine(e.Message + " " + status);
                uploadFile2ByFTP();
            }
            
        }
       

    }
}
