namespace AdminWeb.Models.VM
{
    public class PagedResult<T>
    {
        public string massage { get; set; }
        public List<T> data { get; set; }
    }
}
