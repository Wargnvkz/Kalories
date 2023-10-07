using System.Collections.Generic;

namespace Kalories.Models
{
    public class IndexViewModel
    {
        public List<KaloriesDB.Food> Foods { get; set; }
        public KaloriesDB.Food Food { get; set;}
        public string Text {  get; set; }
    }
}
