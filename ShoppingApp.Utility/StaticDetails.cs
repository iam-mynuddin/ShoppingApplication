namespace ShoppingApp.Utility
{
    public static class StaticDetails
    {
        public const string ROLE_CUSTOMER = "Customer";
        public const string ROLE_COMPANY = "Company";
        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_EMPLOYEE = "Employee";

		public const string StatusPending = "Pending"; 
        public const string StatusApproved = "Approved"; 
        public const string StatusInProcess = "Processing"; 
        public const string StatusShipped = "Shipped"; 
        public const string StatusCancelled = "Cancelled"; 
        public const string StatusRefunded = "Refunded";

        public const string PaymentStatusPending = "Pending"; 
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "Approved For Delayed Payment";
        public const string PaymentStatusRejected = "Rejected";
	}
}
