namespace WebMarket
{
    public interface ISingleton
    {
        string GetString();
    }

    public interface ITransient
    {
        string GetString();
    }

    public interface IScoped
    {
        string GetString();
    }

    public class TransientDependensy : ITransient
    {
        private ISingleton singleton;

        public TransientDependensy(ISingleton s)
        {
            //Console.WriteLine(this.ToString());
            this.singleton = s;
            // Console.WriteLine(this.ToString());
        }

        public string GetString()
        {
            return "(" + singleton.GetString() + "TransientDependensy"+ ")"+"\t";
        }

        public override string ToString()
        {

            return singleton.ToString() + "TransientDependensy\t";
        }
    }

    public class SingletonDependency : ISingleton
    {

        public SingletonDependency()
        {
            //Console.WriteLine(this.ToString());
            // Console.WriteLine(this.ToString());
        }

        public string GetString()
        {
            return " SingletonDependency ";
        }

        public override string ToString()
        {
            return "\tSingletonDependency\t";
        }
    }

    public class ScopedDependency : IScoped
    {
        private ISingleton singleton;

        public ScopedDependency(ISingleton s)
        {
            this.singleton = s;
            //Console.WriteLine(this.ToString());
        }

        public string GetString()
        {
            return "\t" + "(" + "ScopedDependency " + singleton.GetString() + ")" ;
        }

        public override string ToString()
        {
            return "("+"\tScopedDependency " + singleton.ToString();
        }

    }
}
