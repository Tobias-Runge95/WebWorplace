using System.Security.Cryptography;

namespace WebWorkPlace.API;

public static class KeyMaster
{
    private static RSA Rsa;

    public static byte[] GetPrivateKey() => Rsa.ExportRSAPrivateKey();

    public static RSA GetRSAKey() => Rsa;

    public static void SetKey()
    {
        Rsa = RSA.Create();
    }
}