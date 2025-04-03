using App1.Interfaces;
using App1.Providers;

namespace App1.Data
{
    public class UserModel : IUserModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public ulong? ID { get; set; }
        public DateTime? CreatedAt
        {
            get
            {
                if (ID == null)
                {
                    return null;
                }

                return IDProvider.IDToDateAndTime((ulong)ID);
            }
        }
    }
}