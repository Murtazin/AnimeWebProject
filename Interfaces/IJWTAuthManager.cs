using System;
using AnimeWebProject.Models;

namespace AnimeWebProject.Interfaces
{
    public interface IJWTAuthManager
    {
        string Authenticate(User user);
    }
}