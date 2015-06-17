namespace Models.ViewModel
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }

        public int NewsTypeId { get; set; }

        public string NewsPath { get; set; }

        public string NewsTypeName { get; set; }

        public string NewsTitle { get; set; }

        public string NewsContent { get; set; }

        public int IsTop { get; set; }

        public string NewsImagePath { get; set; }

        public string NewsAttachPath { get; set; }

        public string NewsExternalPath { get; set; }

        public News ToNewsModel()
        {
            //Todo:
            return new News() {NewsId=NewsId,NewsTitle = NewsTitle };
        }
    }
}
