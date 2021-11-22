using System;

namespace ApiGameCatalog.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual Publisher Publisher { get; set; }
        public double Price { get; set; }
        public Guid PublisherId { get; set; }        
    }
}
