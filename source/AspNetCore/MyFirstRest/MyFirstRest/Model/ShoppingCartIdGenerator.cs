using System;
using System.Security.Cryptography;
using System.Text;

namespace MyFirstRest.Model
{
    public class ShoppingCartIdGenerator
    {
        public string GenerateId()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[10];
            rng.GetBytes(bytes);

            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }

            return sb.ToString();
        }
    }
}