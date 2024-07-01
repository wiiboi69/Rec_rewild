using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using server;

namespace OpenRec.api
{
    internal class image_util
    {
        public static string SaveImageFile(byte[] request, out bool flag, out string rnfn)
        {
            rnfn = "";
            bool flag1;
            byte[] rnimg = ParceData(request, "image.dat", out flag1);
            rnimg = MultiFormSplit(request);
            if (!flag1)
            {
                flag = true;
                return "error.png";
            }

            string fname = "image_data_";
            string imagefname = "";
            //"SaveData\\images\\camera_" + new Random().Next(1000, 99999999) + "_image.png"

            fname = fname + new Random().Next(1000, 99999999) + "_image.png";
            imagefname = fname;
            FileStream file = File.Create("SaveData\\images\\" + fname);
            file.Write(rnimg);
            file.Close();
            flag = false;
            rnfn = "SaveData\\images\\" + fname;
            return imagefname;
        }
        public static string SaveRoomFile(byte[] request, out bool flag, out string rnfn)
        {
            rnfn = "";
            bool flag1;
            byte[] rnimg = ParceData(request, "File", out flag1);
            //rnimg = MultiFormSplit(request);
            if (!flag1)
            {
                flag = true;
                return "error";
            }

            string fname = "room_data_";
            string imagefname = "";
            //"SaveData\\images\\camera_" + new Random().Next(1000, 99999999) + "_image.png"

            fname = fname + new Random().Next(1000, 99999999) + ".room";
            imagefname = fname;
            FileStream file = File.Create("SaveData\\Rooms\\" + fname);
            file.Write(rnimg);
            file.Close();
            flag = false;
            rnfn = "SaveData\\Rooms\\" + fname;
            return imagefname;
        }
        public class ImageUploadResponse
        {
            public string ImageName { get; set; }
            public ImageUploadResponse(string name) => this.ImageName = name;
        }

        public static byte[] MultiFormSplit(byte[] data)
        {
            using (BinaryReader binaryReader = new BinaryReader((Stream)new MemoryStream(data)))
            {
                while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                {
                    bool flag1 = true;
                    bool flag2 = false;
                    MultiFormData multiFormData = new MultiFormData();
                    while (flag1)
                    {
                        List<byte> byteList = new List<byte>();
                        bool flag3 = true;
                        while (flag3)
                        {
                            byte num1 = binaryReader.ReadByte();
                            if (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                            {
                                if (num1 == (byte)13)
                                {
                                    int num2 = (int)binaryReader.ReadByte();
                                    flag3 = false;
                                }
                                else
                                    byteList.Add(num1);
                            }
                            else
                                flag3 = false;
                        }
                        string str = Encoding.ASCII.GetString(byteList.ToArray());
                        if (str.StartsWith("Content-Length: "))
                        {
                            string s = str.Remove(0, 16);
                            multiFormData.ContentLength = int.Parse(s);
                        }
                        if (str.Contains("image.dat"))
                            flag2 = true;
                        str.Contains("{\"");
                        if (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                        {
                            if (binaryReader.ReadByte() == (byte)13)
                            {
                                flag1 = false;
                                int num = (int)binaryReader.ReadByte();
                            }
                            else
                                --binaryReader.BaseStream.Position;
                        }
                        else
                            flag1 = false;
                    }
                    if (flag2)
                        return binaryReader.ReadBytes(multiFormData.ContentLength);
                    if (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                        binaryReader.ReadBytes(multiFormData.ContentLength);
                }
            }
            return (byte[])null;
        }
        public class MultiFormData
        {
            public string Name { get; set; }

            public string FileName { get; set; }

            public int ContentLength { get; set; }

            public byte[] Content { get; set; }
        }
        public static byte[] ParceData(byte[] data, string name, out bool DidItParced)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(data));
            try
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    bool isReading = true;
                    while (isReading)
                    {
                        List<byte> list = new List<byte>();
                        bool loop = true;
                        while (loop)
                        {
                            byte b = reader.ReadByte();
                            if (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                if (b == 13)
                                {
                                    reader.ReadByte();
                                    loop = false;
                                }
                                else
                                {
                                    list.Add(b);
                                }
                            }
                            else
                            {
                                loop = false;
                            }
                        }
                        string content = Encoding.ASCII.GetString(list.ToArray());
                        Console.WriteLine("data: " + content);
                        if (content.StartsWith("Content-Length: "))
                        {
                            ContentLength = int.Parse(content.Remove(0, 16));
                        }
                        if (content.Contains(name))
                        {
                            Console.WriteLine("file: " + name);
                            isFile = true;
                        }
                        if (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            if (reader.ReadByte() == 13)
                            {
                                isReading = false;
                                reader.ReadByte();
                            }
                            else
                            {
                                reader.BaseStream.Position -= 1L;
                            }
                        }
                        else
                        {
                            isReading = false;
                        }
                    }
                    if (isFile)
                    {
                        List<byte> file = new List<byte>();
                        for (; ; )
                        {
                            if (reader.ReadByte() == 13)
                            {
                                if (reader.ReadByte() == 10)
                                {
                                    if (reader.ReadByte() == 45)
                                    {
                                        break;
                                    }
                                    reader.BaseStream.Position -= 3L;
                                }
                                else
                                {
                                    reader.BaseStream.Position -= 2L;
                                }
                            }
                            else
                            {
                                reader.BaseStream.Position -= 1L;
                            }
                            byte item = reader.ReadByte();
                            file.Add(item);
                        }
                        DidItParced = true;
                        return file.ToArray();
                    }
                    if (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        reader.ReadBytes(ContentLength);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed to parse data " + ex.ToString());
            }
            DidItParced = false;
            Console.WriteLine("can't find: \"" + name + "\" in the form data");
            return null;
        }

        public static int ContentLength;

        public static bool isFile;
    }
}
