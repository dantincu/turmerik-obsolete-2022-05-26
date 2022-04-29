using Microsoft.Graph;
using System;
using System.IO;
using System.Security.Claims;

namespace Turmerik.AspNetCore.MsIdentity.Graph
{
    public static class GraphClaimTypes
    {
        public const string DisplayName = "graph_name";
        public const string Email = "graph_email";
        public const string Photo = "graph_photo";
        public const string TimeZone = "graph_timezone";
        public const string TimeFormat = "graph_timeformat";
    }

    // Helper methods to access Graph user data stored in
    // the claims principal
    public static class GraphClaimsPrincipalExtensions
    {
        public static string GetUserGraphDisplayName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(GraphClaimTypes.DisplayName);
        }

        public static string GetUserGraphEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(GraphClaimTypes.Email);
        }

        public static string GetUserGraphPhoto(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(GraphClaimTypes.Photo);
        }

        public static string GetUserGraphTimeZone(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(GraphClaimTypes.TimeZone);
        }

        public static string GetUserGraphTimeFormat(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(GraphClaimTypes.TimeFormat);
        }

        public static void AddUserGraphInfo(this ClaimsPrincipal claimsPrincipal, User user)
        {
            var identity = claimsPrincipal.Identity as ClaimsIdentity;

            identity.AddClaim(
                new Claim(GraphClaimTypes.DisplayName, user.DisplayName));
            identity.AddClaim(
                new Claim(GraphClaimTypes.Email,
                    user.Mail ?? user.UserPrincipalName));
            identity.AddClaim(
                new Claim(GraphClaimTypes.TimeZone,
                    user.MailboxSettings.TimeZone));
            identity.AddClaim(
                new Claim(GraphClaimTypes.TimeFormat, user.MailboxSettings.TimeFormat));
        }

        public static void AddUserGraphPhoto(this ClaimsPrincipal claimsPrincipal, byte[]? photoBytes)
        {
            var identity = claimsPrincipal.Identity as ClaimsIdentity;

            if (photoBytes == null)
            {
                // Add the default profile photo
                identity.AddClaim(
                    new Claim(GraphClaimTypes.Photo, "/img/no-profile-photo.png"));
                return;
            }

            // Generate a date URI for the photo
            var photoUrl = $"data:image/png;base64,{Convert.ToBase64String(photoBytes)}";

            identity.AddClaim(
                new Claim(GraphClaimTypes.Photo, photoUrl));
        }

        public static byte[] AddUserGraphPhoto(this ClaimsPrincipal claimsPrincipal, Stream photoStream)
        {
            byte[] photoBytes = null;

            if (photoStream != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    photoStream.CopyTo(memoryStream);
                    photoBytes = memoryStream.ToArray();
                }
            }

            claimsPrincipal.AddUserGraphPhoto(photoBytes);
            return photoBytes;
        }
    }
}
