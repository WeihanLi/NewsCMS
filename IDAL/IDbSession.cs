namespace IDAL
{
    /// <summary>
    /// 数据仓储 大接口
    /// </summary>
    public interface IDbSession
    {
        IUser IUser { get; set; }
        INews INews { get; set; }
        ICategory ICategory { get; set; }

        int SaveChanges();
    }
}
