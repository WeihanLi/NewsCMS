namespace IBLL
{
    public interface IBLLSession
    {
        IUser IUser { get; set; }

        INews INews { get; set; }

        ICategory ICategory { get; set; }

        int SaveChanges();
    }
}
