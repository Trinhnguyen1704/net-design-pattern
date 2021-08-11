using System;
using net_design_pattern.Domain.Services.Authorization;

namespace net_design_pattern.Services.Authorization
{
    public class PasswordService : IPasswordService
    {
        public string PasswordDecoder(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] toDecodeByte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(toDecodeByte, 0, toDecodeByte.Length);
            char[] decodedChar = new char[charCount];
            utf8Decode.GetChars(toDecodeByte, 0, toDecodeByte.Length, decodedChar,0);
            string result = new string(decodedChar);
            return result;
        }

        public string PasswordEncoder(string password)
        {
            try
            {
                byte[] encodeDataByte = new byte[password.Length];
                encodeDataByte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encodeDataByte);
                return encodedData;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}