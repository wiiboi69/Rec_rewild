using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using Newtonsoft.Json;

using vaultgamesesh;

// Token: 0x020000A5 RID: 165
namespace vaultgamesesh
{

    internal sealed class c0000a5
    {
        public static byte[] m00005d(byte[] p0, string p1)
        {
            BinaryReader binaryReader = new BinaryReader(new MemoryStream(p0));
            try
            {
                while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                {
                    bool flag = true;
                    bool flag2 = false;
                    c0000a7 c0000a = new c0000a7();
                    while (flag)
                    {
                        List<byte> list = new List<byte>();
                        bool flag3 = true;
                        while (flag3)
                        {
                            byte b = binaryReader.ReadByte();
                            if (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                            {
                                if (b == 13)
                                {
                                    binaryReader.ReadByte();
                                    flag3 = false;
                                }
                                else
                                {
                                    list.Add(b);
                                }
                            }
                            else
                            {
                                flag3 = false;
                            }
                        }
                        string @string = Encoding.ASCII.GetString(list.ToArray());
                        Console.WriteLine(@string);
                        if (@string.StartsWith("Content-Length: "))
                        {
                            string text = @string.Remove(0, 16);
                            c0000a.m000017(int.Parse(text));
                        }
                        if (@string.Contains(p1))
                        {
                            Console.WriteLine("Has file");
                            flag2 = true;
                        }
                        if (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                        {
                            if (binaryReader.ReadByte() == 13)
                            {
                                flag = false;
                                binaryReader.ReadByte();
                            }
                            else
                            {
                                binaryReader.BaseStream.Position -= 1L;
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    if (flag2)
                    {
                        List<byte> list2 = new List<byte>();
                        for (; ; )
                        {
                            if (binaryReader.ReadByte() == 13)
                            {
                                if (binaryReader.ReadByte() == 10)
                                {
                                    if (binaryReader.ReadByte() == 45)
                                    {
                                        break;
                                    }
                                    binaryReader.BaseStream.Position -= 3L;
                                }
                                else
                                {
                                    binaryReader.BaseStream.Position -= 2L;
                                }
                            }
                            else
                            {
                                binaryReader.BaseStream.Position -= 1L;
                            }
                            byte b2 = binaryReader.ReadByte();
                            list2.Add(b2);
                        }
                        return list2.ToArray();
                    }
                    if (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                    {
                        binaryReader.ReadBytes(c0000a.m000016());
                    }
                }
            }
            finally
            {
                ((IDisposable)binaryReader).Dispose();
            }
            return null;
        }

        public sealed class c0000a7
        {
            // Token: 0x060003B5 RID: 949 RVA: 0x00003926 File Offset: 0x00001B26
            public string m000001()
            {
                return this.f00000a;
            }

            // Token: 0x060003B6 RID: 950 RVA: 0x0000392E File Offset: 0x00001B2E
            public void m000034(string p0)
            {
                this.f00000a = p0;
            }

            // Token: 0x060003B7 RID: 951 RVA: 0x00003937 File Offset: 0x00001B37
            public string m000005()
            {
                return this.f000002;
            }

            // Token: 0x060003B8 RID: 952 RVA: 0x0000393F File Offset: 0x00001B3F
            public void m00003f(string p0)
            {
                this.f000002 = p0;
            }

            // Token: 0x060003B9 RID: 953 RVA: 0x00003948 File Offset: 0x00001B48
            public int m000016()
            {
                return this.f000020;
            }

            // Token: 0x060003BA RID: 954 RVA: 0x00003950 File Offset: 0x00001B50
            public void m000017(int p0)
            {
                this.f000020 = p0;
            }

            // Token: 0x060003BB RID: 955 RVA: 0x00003959 File Offset: 0x00001B59
            public byte[] m000061()
            {
                return this.f000084;
            }

            // Token: 0x060003BC RID: 956 RVA: 0x00003961 File Offset: 0x00001B61
            public void m000062(byte[] p0)
            {
                this.f000084 = p0;
            }

            // Token: 0x04000201 RID: 513
            private string f00000a;

            // Token: 0x04000202 RID: 514
            private string f000002;

            // Token: 0x04000203 RID: 515
            private int f000020;

            // Token: 0x04000204 RID: 516
            private byte[] f000084;
        }
    }

}
