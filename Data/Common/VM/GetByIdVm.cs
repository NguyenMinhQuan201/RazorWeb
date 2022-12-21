namespace AdminWeb.Models.VM
{
    public class GetByIdVm<T>
    {
        public string massage { get; set; }
        public T data { get; set; }
    }
}
