using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using SuperfonWorks.Data.Entities;

namespace SuperfonMobileAPI.Shared
{
    public static class ExtensionMethods
    {
        public static int GetUserId(this ControllerBase controllerBase)
        {
            return Convert.ToInt32(controllerBase.User.FindFirstValue("UserId"));
        }
        public static string ToStringWithLineBreaks(this string input)
        {
            return ConvertLineBreaks(input);
        }

        public static string ConvertLineBreaks(string input)
        {
            try
            {
                var lines = input.Split("\n");
                return string.Join(Environment.NewLine, lines);
            }
            catch
            {
                return input;
            }
        }

        public static string ReverseLineBreaks(string input)
        {
            try
            {
                var lines = input.Split("\r\n");
                return string.Join("\n", lines);
            }
            catch
            {
                return input;
            }

        }

        public static string Aggregate(this IEnumerable<UserBranchPermission> userBranches)
        {
            return userBranches.Aggregate(seed: string.Empty, (seed, permission) => seed + "," + permission.BranchNumber, resultSelector: res => res.TrimStart(','));
        }
    }
}
