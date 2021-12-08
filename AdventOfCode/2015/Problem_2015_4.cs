using System;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode
{
    class Problem_2015_4 : Problem_2015
    {
        public override int Number => 4;

        public override void Run()
        {
            int i = 1;
            bool foundFirstStar = false;

            while (true)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, $"{Text}{i}");

                    if (!foundFirstStar && HasLeadingZeroes(hash, 5))
                    {
                        Console.WriteLine($"First star: {i}");
                        foundFirstStar = true;
                    }
                    else if (HasLeadingZeroes(hash, 6))
                    {
                        Console.WriteLine($"Second star: {i}");
                        break;
                    }
                }

                i++;
            }
        }

        private bool HasLeadingZeroes(string str, int nbZeroes)
        {
            int count = 0;

            for (int i = 0; i < nbZeroes; i++)
            {
                count += (str[i] == '0' ? 1 : 0);
            }

            return count == nbZeroes;
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
