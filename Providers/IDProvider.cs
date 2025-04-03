using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Providers
{
    public static class IDProvider
    {
        // snowflake ID
        private static ulong _lastId = 0;
        private static readonly ulong epoch = 1420070400000;

        private static string ToBinaryString(this ulong value)
        {
            return Convert.ToString((long)value, 2);
        }

        private static ulong ToULong(this string value)
        {
            return Convert.ToUInt64(value, 2);
        }

        public static ulong GetNextID()
        {
            ulong unixTimestamp = (ulong)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            string timeSinceEpoch = ToBinaryString(unixTimestamp - epoch);
            string internalWorkerID = ToBinaryString((unixTimestamp & 0x3E0000) >> 17);
            string internalProcessID = ToBinaryString((unixTimestamp & 0x1F000) >> 12);
            string increment = ToBinaryString(unixTimestamp & 0xFFF);

            while (internalWorkerID.Length < 5)
            {
                internalWorkerID = "0" + internalWorkerID;
            }

            while (internalProcessID.Length < 5)
            {
                internalProcessID = "0" + internalProcessID;
            }

            while (increment.Length < 12)
            {
                increment = "0" + increment;
            }

            ulong ID = (timeSinceEpoch + internalWorkerID + internalProcessID + increment).ToULong();

            if (ID == _lastId)
            {
                ID = _lastId + 1;
            }

            _lastId = ID;
            return ID;
        }

        public static DateTime IDToDateAndTime(ulong ID)
        {
            string binaryID = ID.ToBinaryString();
            ulong timeSinceEpoch = binaryID.Substring(0, binaryID.Length - 22).ToULong() + epoch;
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime IDCreatedAt = origin.AddMilliseconds(timeSinceEpoch);
            return IDCreatedAt;
        }
    }
}
