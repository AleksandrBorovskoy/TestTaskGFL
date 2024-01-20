namespace TestTaskGFL.Entities
{
    public class CarEntity
    {
        public int Id { get; set; }
        public CarEntity()
        {
            Brand = new Brand();
            Appearence = new Appearence();
        }
        public Brand? Brand { get; set; }
        public double EngineCapacity { get; set; }

        public Appearence? Appearence { get; set; }
    }
}
