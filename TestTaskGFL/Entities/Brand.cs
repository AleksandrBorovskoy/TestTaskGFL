using System.Diagnostics.Metrics;

namespace TestTaskGFL.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public Brand()
        {
            Country = new Country();
        }
        public Country Country { get; set; }
        public string Name { get; set; }
    }
}
