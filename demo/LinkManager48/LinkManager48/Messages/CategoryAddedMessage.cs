namespace LinkManager48.Messages
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