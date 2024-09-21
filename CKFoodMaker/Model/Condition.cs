namespace CKFoodMaker.Model
{
    public record Condition
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public double Duration { get; set; }
        public double Timer { get; set; }

        public Condition(int id, int value, double duration, double timer)
        {
            Id = id;
            Value = value;
            Duration = duration;
            Timer = timer;
        }
    }
}
