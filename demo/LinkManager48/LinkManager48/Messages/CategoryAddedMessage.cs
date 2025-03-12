namespace LinkManager48
{
    internal class CategoryAddedMessage
    {
        public string CategoryName { get; }

        public CategoryAddedMessage(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}