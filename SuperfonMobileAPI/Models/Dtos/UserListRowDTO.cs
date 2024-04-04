using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SuperfonMobileAPI.Models.Entities;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class UserListRowDTO
    {
        public int UserId { get; set; }
        [DisplayName("Login")]
        public string Username { get; set; }
        [DisplayName("Adı, Soyadı")]
        public string DisplayName { get; set; }
        [DisplayName("Aktiv")]
        public bool IsActive { get; set; }
        [DisplayName("E-Poçt")]
        public string Email { get; set; }
        [DisplayName("Tel no")]
        public string Phone { get; set; }
        [DisplayName("FİN")]
        public string UserPID { get; set; }
        public int? UserGroupId { get; set; }
        [DisplayName("Qrup")]
        public string GroupName { get; set; }
        public List<TigerWarehouse> Warehouses { get; set; }
        [DisplayName("Yeni Anbar №")]
        public int WarehouseNumberAdd { get; set; }
        public IEnumerable<MenuPermissionReadDTO> MenuPermissions { get; set; }
        public IEnumerable<MenuPermissionReadDTO> MenuPermissionsFromGroup { get; set; }
        [DisplayName("Yeni Menyu izni")]
        public int MenuPermissionIdAdd { get; set; }
        [DisplayName("Yeni Filial izni")]
        public int BranchNumberAdd { get; set; }
        public List<TigerCard> Cards { get; set; }
        [DisplayName("Yeni Cari Kodu")]
        public string CardCodePermissionAdd { get; set; }
        public List<TigerBranch> Branches { get; set; }
        public List<TigerSafebox> Safeboxes { get; set; }
        public List<TigerBankAccount> BankAccounts { get; set; }
        [DisplayName("Yeni Kassa Kodu")]
        public string SafeboxPermissionAdd { get; set; }
        public string BankAccPermissionAdd { get; set; }

        public string RandomPassword { get; set; }

    }
}
