namespace PracticingDDD.Logic
{
    public class Snack : Entity
    {
        public virtual string Name { get; protected set; }

        protected Snack()
        {
            
        }

        public Snack(string name) : this()
        {
            Name = name;
        }
    }
}