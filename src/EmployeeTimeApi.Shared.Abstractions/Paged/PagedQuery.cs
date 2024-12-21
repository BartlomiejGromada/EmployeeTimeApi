namespace EmployeeTimeApi.Application.Shared;

public interface IPagedQuery
{
    int Page { get; set; }
    int Results { get; set; }
}

public interface IPagedQuery<T> : IPagedQuery
{
}

public abstract class PagedQuery : IPagedQuery
{
    public int Page { get; set; }
    public int Results { get; set; }
}

public abstract class PagedQuery<T> : PagedQuery, IPagedQuery<Paged<T>>
{
}
