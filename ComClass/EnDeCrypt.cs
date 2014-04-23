using System;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;

public class EnDeCrypt
{
    /// <summary>
    /// ���췽��
    /// </summary>
    public EnDeCrypt()
    {
    }

    private static string ende = "12345678";

    /// <summary>
    /// ʹ��ȱʡ��Կ�ַ�������
    /// </summary>
    /// <param name="original">����</param>
    /// <returns>����</returns>
    public static string Encrypt(string original)
    {
        return Encrypt(original, ende);
    }

    /// <summary>
    /// ʹ��ȱʡ��Կ����
    /// </summary>
    /// <param name="original">����</param>
    /// <returns>����</returns>
    public static string Decrypt(string original)
    {
        return Decrypt(original, ende, System.Text.Encoding.Default);
    }

    /// <summary>
    /// ʹ�ø�����Կ����
    /// </summary>
    /// <param name="original">����</param>
    /// <param name="key">��Կ</param>
    /// <returns>����</returns>
    public static string Decrypt(string original, string key)
    {
        return Decrypt(original, key, System.Text.Encoding.Default);
    }

    /// <summary>
    /// ʹ��ȱʡ��Կ����,����ָ�����뷽ʽ����
    /// </summary>
    /// <param name="original">����</param>
    /// <param name="encoding">���뷽ʽ</param>
    /// <returns>����</returns>
    public static string Decrypt(string original, Encoding encoding)
    {
        return Decrypt(original, ende, encoding);
    }

    /// <summary>
    /// ʹ�ø�����Կ����
    /// </summary>
    /// <param name="original">ԭʼ����</param>
    /// <param name="key">��Կ</param>
    /// <param name="encoding">�ַ����뷽��</param>
    /// <returns>����</returns>
    public static string Encrypt(string original, string key)
    {
        byte[] buff = System.Text.Encoding.Default.GetBytes(original);
        byte[] kb = System.Text.Encoding.Default.GetBytes(key);
        return Convert.ToBase64String(Encrypt(buff, kb));
    }

    /// <summary>
    /// ʹ�ø�����Կ����
    /// </summary>
    /// <param name="encrypted">����</param>
    /// <param name="key">��Կ</param>
    /// <param name="encoding">�ַ����뷽��</param>
    /// <returns>����</returns>
    public static string Decrypt(string encrypted, string key, Encoding encoding)
    {
        byte[] buff = Convert.FromBase64String(encrypted);
        byte[] kb = System.Text.Encoding.Default.GetBytes(key);
        byte[] s = Decrypt(buff, kb);
        return encoding.GetString(s, 0, s.Length);
    }

    /// <summary>
    /// ����MDժҪ
    /// </summary>
    /// <param name="original">����Դ</param>
    /// <returns>ժҪ</returns>
    public static byte[] MakeMD(byte[] original)
    {
        //System.Security.Cryptography.MD5CryptoServiceProvider
        MD5CryptoServiceProvider hashmd = new MD5CryptoServiceProvider();
        byte[] keyhash = hashmd.ComputeHash(original);
        hashmd = null;
        return keyhash;
    }

    /// <summary>
    /// ʹ�ø�����Կ����
    /// </summary>
    /// <param name="original">����</param>
    /// <param name="key">��Կ</param>
    /// <returns>����</returns>
    public static byte[] Encrypt(byte[] original, byte[] key)
    {
        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        des.Key = MakeMD(key);
        des.Mode = CipherMode.ECB;

        return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
    }

    /// <summary>
    /// ʹ�ø�����Կ��������
    /// </summary>
    /// <param name="encrypted">����</param>
    /// <param name="key">��Կ</param>
    /// <returns>����</returns>
    public static byte[] Decrypt(byte[] encrypted, byte[] key)
    {
        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        des.Key = MakeMD(key);
        des.Mode = CipherMode.ECB;

        return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
    }

    /// <summary>
    /// ʹ��ȱʡ��Կ����
    /// </summary>
    /// <param name="original">ԭʼ����</param>
    /// <returns>����</returns>
    public static byte[] Encrypt(byte[] original)
    {
        byte[] key = System.Text.Encoding.Default.GetBytes(ende);
        return Encrypt(original, key);
    }

    /// <summary>
    /// ʹ��ȱʡ��Կ��������
    /// </summary>
    /// <param name="encrypted">����</param>
    /// <returns>����</returns>
    public static byte[] Decrypt(byte[] encrypted)
    {
        byte[] key = System.Text.Encoding.Default.GetBytes(ende);
        return Decrypt(encrypted, key);
    }
}

