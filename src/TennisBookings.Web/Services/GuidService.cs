using System;

namespace TennisBookings.Web.Services
{
    public class GuidService
    {
        private readonly Guid ServiceGuid;

        public GuidService()
        {
            ServiceGuid = Guid.NewGuid();
        }

        public string GetGuid() => ServiceGuid.ToString();
    }
}