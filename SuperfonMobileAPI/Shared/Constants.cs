using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Shared
{
    public static class Constants
    {
        public const string UsernameOrPasswordInvalid = "İstifadəçi adı və ya şifrə yanlışdır";
        public const string UsernameInactive = "İstifadəçi adı passivdir";
        public const string UserConnectedSafeboxNotFound = "İstifadəçi adına bağlı kassa tapılmadı";
        public const string TransferTypeNotFound = "Transer növü tapılmadı";
        public const string WrongAmountEntered = "Məbləğ yanlış daxil edilib";
        public const string BasicAuthenticationScheme = "BasicAuthentication";
    }
}
