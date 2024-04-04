using SuperfonMobileAPI.Models.Entities;
using System.Collections.Generic;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class TigerProductListDTO
    {
        public IEnumerable<TigerProduct> Products { get; set; }

        ///<summary>
        /// Gets or sets CurrentPageIndex.
        ///</summary>
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }

        public int SelectedBranch { get; set; }
        public string Text { get; set; }
    }
}
