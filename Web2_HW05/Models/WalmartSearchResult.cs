using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2_HW05.Models
{
    public interface IWalmartSearchApi
    {
        [Get("/v1/search?query={searchQuery}&format=json&apiKey={apiKey}")]
        Task <WalmartSearchResult> SearchAsync(string searchQuery, string apiKey);
    }

    public class WalmartSearchResult
    {
        public string Query { get; set; }
        public int TotalResults { get; set; }
        public int Start { get; set; }
        public int NumItems { get; set; }
        public IEnumerable<WalmartSearchItem> Items { get; set; }
    }

    public class WalmartSearchItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Msrp { get; set; }
        public decimal SalePrice { get; set; }
        public string Upc { get; set; }
        public string CategoryPath { get; set; }
        public string ShortDescription { get; set; }
        public string ThumbnailImage { get; set; }
    }
}
