using System.Collections.Generic;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class GroupMenuPermissionDTO
    {
        public int GroupId { get; set; }
        public int MenuPermissionIdAdd { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<MenuPermissionReadDTO> MenuPermissions { get; set; }
    }
}
