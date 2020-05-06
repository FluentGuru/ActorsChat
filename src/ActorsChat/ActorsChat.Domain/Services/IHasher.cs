using System;
using System.Collections.Generic;
using System.Text;

namespace ActorsChat.Domain.Services
{
    public interface IHasher
    {
        string GetSalt(int lenght);
        string Generate(string source, string salt);
    }
}
