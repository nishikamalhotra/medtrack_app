using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using MedTrack.Resources.Entity;
using System.Security.Cryptography;

namespace MedTrack.Service
{
    class LoginService
    {
        private readonly DynamoDBService _dynamoDBService;

        public LoginService()
        {
            _dynamoDBService = new DynamoDBService();
        }
        public async Task<bool> loginAuthentication(string username, string password)
        {
            bool retValue = false;
            Task<byte[]> hash = FindHashPasswordByUsername(username);
            byte[] hashPassword = await hash;

            //hash the incoming password
            var data = Encoding.ASCII.GetBytes(password);
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(data);
            if (AreHashesEqual(sha1data, hashPassword))
            {
                retValue = true;
            }
            else
                retValue = false;

            return retValue;
        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int minHashLenght = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < minHashLenght; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
        public async Task<byte[]> FindHashPasswordByUsername(string username)
        {
            Task<byte[]> password = _dynamoDBService.FindHashPasswordByUsername(username);
            byte[] hash = await password;
            return hash;
        }

        public bool SaveNewUserDetails(User user)
        {
            _dynamoDBService.Store(user);
            return true;
        }
    }
}