using System;

namespace ApiGameCatalog.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public double Price { get; set; }
    }
}
