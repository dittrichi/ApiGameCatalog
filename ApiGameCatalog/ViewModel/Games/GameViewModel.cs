using System;

namespace ApiGameCatalog.ViewModel
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PublisherID { get; set; }
        public virtual string Publisher { get; set; } 
        public double Price{ get; set; }
    }
}
