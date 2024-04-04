using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class MenuPermissionReadDTO
    {
        public int MenuPermissionId { get; set; }

        public int ParentId { get; set; }

        public byte MenuPermissionTypeId { get; set; }

        public string PermissionName { get; set; }

        public string KeyWord { get; set; }

        public bool IsActive { get; set; }

        public string IconName { get; set; }

        public string Link { get; set; }
    }
}
