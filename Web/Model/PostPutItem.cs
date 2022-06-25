public class PostPutItem
{
    public PostPutItem(string name, string value)
    {
        this.name = name;
        this.value = value;
    }
    public PostPutItem()
    {
    }
    public void Zero()
    {
        this.name = "";
        this.value = "";
    }
    public string name { get; set; } = "";
    public string value { get; set; } = "";

    public bool IsValid => name.NotNullAndGreaterThen(0) && value.NotNullAndGreaterThen(0);
}
