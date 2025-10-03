using EcommerceSite.Components.Storage;

namespace EcommerceSite.Components.Classes
{
    public class SalesHistory
    {
        public Guid TransactionID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid ProductID { get; set; }
        public Guid SellerID { get; set; }

        public SalesHistory()
        {
            TransactionID = Guid.NewGuid();
            CustomerID = Guid.NewGuid();
            ProductID = Guid.NewGuid();
            SellerID = Guid.NewGuid();
        }
    }

    public class SalesHistoryCollection
    {
        public List<SalesHistory> SalesHistory { get; set; }

        public SalesHistoryCollection()
        {
            SalesHistory = DataLayer.Load<SalesHistory>();
        }

        public void Save()
        {
            DataLayer.Save(SalesHistory);
        }
    }
}