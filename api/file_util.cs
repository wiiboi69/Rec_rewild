using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using server;
using System.Collections;

namespace Rec_rewild.api
{
    internal class file_util
    {
        public static string SaveImageFile(byte[] request, out bool flag, out string rnfn)
        {
            rnfn = "";
            bool flag1;
            byte[] rnimg = ParceData(request, "image.dat", out flag1);
            //rnimg = MultiFormSplit(request);
            if (!flag1)
            {
                flag = true;
                return "error.png";
            }

            string fname = string.Concat(new string[]
            {
                "Rec_rewild_",
                DateTime.Now.Month.ToString(),
                "-",
                DateTime.Now.Day.ToString(),
                "-",
                DateTime.Now.Year.ToString(),
                "-",
                DateTime.Now.Hour.ToString(),
                "_",
                DateTime.Now.Minute.ToString(),
            });
            string imagefname = "";
            //"SaveData\\images\\camera_" + new Random().Next(1000, 99999999) + "_image.png"

            fname = fname + "_image.png";
            imagefname = fname;
            FileStream file = File.Create("SaveData\\images\\" + fname);
            file.Write(rnimg);
            file.Close();
            flag = false;
            rnfn = "SaveData/images/" + fname;
            return imagefname;
        }
        public static FileType GetFileType(byte[] request)
        {
            byte[] filetype = ParceData(request, "FileType", 1);

            File.WriteAllBytes("SaveData\\data_filetype.dat", filetype);


            return (FileType)filetype[0];
        }

        public enum FileType
        {
            // Token: 0x0400CF28 RID: 53032
            Unknown,
            // Token: 0x0400CF29 RID: 53033
            RoomSave,
            // Token: 0x0400CF2A RID: 53034
            Holotar,
            // Token: 0x0400CF2B RID: 53035
            Image,
            // Token: 0x0400CF2C RID: 53036
            Video,
            // Token: 0x0400CF2D RID: 53037
            Invention,
            // Token: 0x0400CF2E RID: 53038
            RoomMetadata
        }

        public static string SavedummyFile(byte[] request, out bool flag, out string rnfn)
        {
            flag = false;
            rnfn = "";
            return "";
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
            return null;
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
                Console.WriteLine("Failed to parse data " + ex.ToString());
            }
            DidItParced = false;
            Console.WriteLine("Can't find: \"" + name + "\" in the form data");
            return null;
        }

        public static FileType GetType(byte[] data,out byte[] data_out)
        {
            int header = IndexOfSequence(data, [0x0D, 0x0A])[0];
            string result = Encoding.UTF8.GetString(data[0..header]);
            int offset = Search(data, Encoding.UTF8.GetBytes(result));
            List<int> offsetdata = IndexOfSequence(data, Encoding.UTF8.GetBytes(result));

            byte[] datatype = data[offsetdata[1]..(offsetdata[2] + header + 4)];
            byte[] data_main = Combine(data[offsetdata[0]..offsetdata[1]],data[offsetdata[2]..(offsetdata[2] + header + 4)]);

            data_out = data_main;
            byte[] rnimg = ParceData(datatype, "FileType");
            return (FileType)int.Parse(Encoding.UTF8.GetString(rnimg));
        }

        public static List<int> IndexOfSequence(byte[] buffer, byte[] pattern, int startIndex = 0)
        {
            
            List<int> positions = new List<int>();
            int i = Array.IndexOf(buffer, pattern[0], startIndex);
            while (i >= 0 && i <= buffer.Length - pattern.Length)
            {
                byte[] segment = new byte[pattern.Length];
                Buffer.BlockCopy(buffer, i, segment, 0, pattern.Length);
                if (segment.SequenceEqual(pattern))
                    positions.Add(i);
                i = Array.IndexOf(buffer, pattern[0], i + 1);
            }
            return positions;

        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        public static int Search(byte[] src, byte[] pattern)
        {
            int maxFirstCharSlot = src.Length - pattern.Length + 1;
            for (int i = 0; i < maxFirstCharSlot; i++)
            {
                if (src[i] != pattern[0]) // compare only first byte
                    continue;

                // found a match on first byte, now try to match rest of the pattern
                for (int j = pattern.Length - 1; j >= 1; j--)
                {
                    if (src[i + j] != pattern[j]) break;
                    if (j == 1) return i;
                }
            }
            return -1;
        }


        public static byte[] ParceData(byte[] data, string name)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(data));

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
                    return file.ToArray();
                }
                if (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    reader.ReadBytes(ContentLength);
                }
            }

            return null;
        }


        public static byte[] ParceData(byte[] data, string name, int file_idx = 0)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(data));
            int idx = 0;
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
                    if (file_idx == idx)
                    {
                        idx += 1;
                        isFile = false;
                        goto skip_file;
                    }
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

                    idx += 1;
                    return file.ToArray();
                }
                skip_file:
                if (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    reader.ReadBytes(ContentLength);
                }
            }

            return null;
        }

        public static int ContentLength;

        public static bool isFile;
    }
}
