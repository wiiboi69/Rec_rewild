using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using api;

namespace server
{
	internal class ImageServer
	{
		public ImageServer()
		{
			try
			{
				Console.WriteLine("[ImageServer.cs] has started.");
				new Thread(new ThreadStart(this.StartListen)).Start();
			}
			catch (Exception ex)
			{
				Console.WriteLine("An Exception Occurred while Listening :" + ex.ToString());
			}
		}

		private void StartListen()
		{
			this.listener.Prefixes.Add("http://localhost:20213/");
			byte[] notfound = new WebClient().DownloadData("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/notfoundimage.jpg");

            for (; ; )
			{
				this.listener.Start();
				Console.WriteLine("{ImageServer.cs] is listening.");
				HttpListenerContext context = this.listener.GetContext();
				HttpListenerRequest request = context.Request;
				HttpListenerResponse response = context.Response;
				string rawUrl = request.RawUrl;
				string text;
				byte[] i = new WebClient().DownloadData("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/notfoundimage.jpg");
                using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
				{
					text = streamReader.ReadToEnd();
				}
				if (rawUrl.StartsWith("/alt/"))
				{
					i = File.ReadAllBytes("SaveData\\profile.png");
				}
				else if (rawUrl.StartsWith("/" + File.ReadAllText("SaveData\\Profile\\username.txt")))
				{
					i = File.ReadAllBytes("SaveData\\profile.png");
				}
				else if (rawUrl.StartsWith("//room/"))
				{
                    rawUrl = rawUrl.Substring("//room".Length);
                    try
                    {
                        i = new WebClient().DownloadData("https://cdn.rec.net" + rawUrl.Remove(0, 1));
                    }
                    catch
                    {
                        Console.WriteLine($"[ImageServer.cs] {rawUrl} DataBlob not found on cdn.rec.net. trying to download from github");
                        i = new WebClient().DownloadData("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/CDN/room" + rawUrl);
                    }
                }
                else if (rawUrl.StartsWith("//video/"))
                {
                    rawUrl = rawUrl.Substring("//video".Length);
                    try
                    {
                        i = new WebClient().DownloadData("https://cdn.rec.net" + rawUrl.Remove(0, 1));
                    }
                    catch
                    {
                        Console.WriteLine($"[ImageServer.cs] {rawUrl} video not found on cdn.rec.net. trying to download from github");
                        i = new WebClient().DownloadData("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/CDN/video" + rawUrl);
                    }
                }

                else if (rawUrl.StartsWith("//data/"))
				{
					i = new WebClient().DownloadData("https://cdn.rec.net" + rawUrl.Remove(0, 1));
				}
                //SaveData\\images\\
                else if (rawUrl.StartsWith("/Community"))
                {
					rawUrl = rawUrl.Substring("/Community".Length);
                    try
                    {
                        string[] stringSeparators = new string[] { "?width=" };
                        string[] subs = rawUrl.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        rawUrl = subs[0];
                        Console.WriteLine(rawUrl);
                        i = new WebClient().DownloadData("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/Images" + rawUrl);
                    }
                    catch
                    {
                        Console.WriteLine($"[ImageServer.cs] {rawUrl} Image not found on img.rec.net. using Default Room Image");
                        i = notfound;
                    }
                }
                //i = new WebClient().DownloadData("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/notfoundimage.jpg");
                else if (rawUrl.StartsWith("SaveData/images/"))
                {
					string temp = rawUrl.Substring("SaveData/images/".Length);
                    i = File.ReadAllBytes("SaveData\\images\\" + temp);
                }
                else if (rawUrl.StartsWith("/Profile") || rawUrl.StartsWith("/profile"))
                {
                    i = File.ReadAllBytes("SaveData\\profileimage.png");
                }
                else
                {
					try
					{
                        string[] stringSeparators = new string[] { "?width=" };
                        string[] subs = rawUrl.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
						rawUrl = subs[0];
						Console.WriteLine(rawUrl);
						i = new WebClient().DownloadData("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/Images" + rawUrl);
					}
					catch
					{
						Console.WriteLine("[ImageServer.cs] Image not found on img.rec.net. using Default Room Image");
						i = notfound;
                    }
				}
				if (rawUrl.StartsWith("/CustomRoom.png"))
                {
					try
					{
						i = new WebClient().DownloadData("https://img.rec.net/" + File.ReadAllText("SaveData\\Rooms\\Downloaded\\imagename.txt"));
					}
					catch
					{
                        Console.WriteLine($"[ImageServer.cs] {rawUrl} Image not found on img.rec.net. using Default Room Image");
                        i = new WebClient().DownloadData("https://img.rec.net/DefaultRoomImage.jpg");
					}
				}
				Console.WriteLine("Image Requested: " + rawUrl);
				Console.WriteLine("Image Data: " + text);
				Console.WriteLine("Image Response: ");
				byte[] bytes = i;
				response.ContentLength64 = (long)bytes.Length;
				Stream outputStream = response.OutputStream;
                context.Response.AppendHeader("content-signature", "key-id=KEY:RSA:p1.rec.net; data=IWwe/pZ5vWWqNSkSM/54isgDxlZkdrP0sUrppKCbNktO2yCOTjq746xWiiLsueGuVcAGQqkjeRTimxolHckS/YXSYkEJxtiCXbLlsRia2DyAqtWVkGWsfczzFhp/56U66FVzolTspPCvjScOVlGO7dDIK7sJ+ndcRauWjsQsC6g3e7rUc6uwY099a6gy7sw6xr5BFZQSz8wg+fqyHYD/Sc4nQQVOTFZNNASqbJYhpNhEMXRnafCMuLl8a3mkGwvy3t4q2D/7SM48xrGZjEV47qNx1A91KCe28XVToFh4BzwEUU8nZ0d+KwV79MGarLo1cY8igc8FcoThKcovI4ClOg==");
                outputStream.Write(bytes, 0, bytes.Length);
                Thread.Sleep(1);
				outputStream.Close();
				this.listener.Stop();
			}
		}
		public static string VersionCheckResponse = "{\"ValidVersion\":true}";
		public static string BlankResponse = "";

		private HttpListener listener = new HttpListener();
		
    }
}
