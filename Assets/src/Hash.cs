using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class Hash : MonoBehaviour
{
    public string GetHash()
    {
        string Security = HashEncoding(GetRandomValue());
        return Security;
    }
    public static string GetRandomValue()
    {
        System.Random Seed = new System.Random();
        string RandomVaule = Seed.Next(1, int.MaxValue).ToString();
        return RandomVaule;
    }
    public static string HashEncoding(string Security)
    {
        byte[] Value;
        UnicodeEncoding Code = new UnicodeEncoding();
        byte[] Message = Code.GetBytes(Security);
        SHA512Managed Arithmetic = new SHA512Managed();
        Value = Arithmetic.ComputeHash(Message);
        Security = "";
        foreach (byte o in Value)
        {
            Security += (int)o;
        }
        return Security;
    }
}