using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
namespace LudoGameTemplate
{
    public class SecurePlayerPrefs
    {
        static string password = "LUDOMONEY_INS";
        public static void SetString(string key, string value)
        {
            try
            {
                var desEncryption = new DESEncryption();
                string hashedKey = GenerateMD5(key);
                string encryptedValue = desEncryption.Encrypt(value, password);
                //PlayerPrefs.SetString(key, value);
                PlayerPrefs.SetString(hashedKey, encryptedValue);
                PlayerPrefs.Save();
            }
            catch (System.Exception ex)
            {

            }
        }
        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public static string GetString(string key)
        {

            //  return PlayerPrefs.GetString(key);
            if (HasKey(key))
            {
                string hashedKey = GenerateMD5(key);
                if (PlayerPrefs.HasKey(hashedKey))
                {
                    var desEncryption = new DESEncryption();
                    string encryptedValue = PlayerPrefs.GetString(hashedKey);
                    string decryptedValue;
                    desEncryption.TryDecrypt(encryptedValue, password, out decryptedValue);
                    return decryptedValue;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
            //  return PlayerPrefs.GetString(key);
        }

        public static bool HasKey(string key)
        {
            string hashedKey = GenerateMD5(key);
            bool hasKey = PlayerPrefs.HasKey(hashedKey);
            return hasKey;
        }



        public static int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public static float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        static string GenerateMD5(string text)
        {
            var md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}