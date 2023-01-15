using System.Security.Cryptography;
using System.Text;
using Applications.Contracts.Interface;

namespace Infrastructure.Services.User;

public class UserToken : IUserToken
{
    public string EncodePassword(string password)
    {
        //Declarations
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;

        //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
        md5 = new MD5CryptoServiceProvider();
        originalBytes = ASCIIEncoding.Default.GetBytes(password);
        encodedBytes = md5.ComputeHash(originalBytes);

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < encodedBytes.Length; i++)
        {
            //change it into 2 hexadecimal digits  
            //for each byte  
            builder.Append(encodedBytes[i].ToString("x2"));
        }

        //Convert encoded bytes back to a ‘readable’ string
        return builder.ToString();
    }

    public string GenerateToken(string empCode, string empName)
    {
        return Guid.NewGuid().ToString();
    }
}