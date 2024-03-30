using System.Collections.Generic;

namespace StoredData
{
    internal class Program
    {
        public class Email
        {
            public string email { get; set; }
            public string password { get; set; }
            public string smtp { get; set; }

            public Email()
            {
                email = "testmail@gmail.com";
                password = "testpassword";
                smtp = "smtp.gmail.com";
            }
        }

        static void Main(string[] args)
        {
            StoredData data = new StoredData(".", "UOAlive", "Test Character");

            string json = data.ToString();

            data.StoreData(12, "integer_global", StoredData.StoreType.Global);
            data.StoreData(13, "integer_server", StoredData.StoreType.Server);
            data.StoreData(14, "integer_character", StoredData.StoreType.Character);

            var x = data.GetData<int>("integer_global", StoredData.StoreType.Global);
            var y = data.GetData<int>("integer_server", StoredData.StoreType.Server);
            var z = data.GetData<int>("integer_character", StoredData.StoreType.Character);

            data.StoreData(new Email(), "email", StoredData.StoreType.Character);
            Email email = data.GetData<Email>("email", StoredData.StoreType.Character);
            var email2 = data.GetData<Email>("email", StoredData.StoreType.Global);

            List<int> list = new List<int> { 1, 2, 3};
            data.StoreData(list, "list", StoredData.StoreType.Server);
            var list2 = data.GetData<List<int>>("list", StoredData.StoreType.Server);
        }
    }
}
