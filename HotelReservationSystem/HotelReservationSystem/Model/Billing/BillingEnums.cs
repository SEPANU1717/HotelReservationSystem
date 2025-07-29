using System.ComponentModel;

namespace HotelReservationSystem.Model.Billing
{
    public enum PaymentMethod
    {
        [Description("Cash")]
        Cash = 1,
        
        [Description("Credit Card")]
        CreditCard = 2,
        
        [Description("Debit Card")]
        DebitCard = 3,
        
        [Description("Bank Transfer")]
        BankTransfer = 4,
        
        [Description("Check")]
        Check = 5,
        
        [Description("Mobile Payment")]
        MobilePayment = 6,
        
        [Description("Digital Wallet")]
        DigitalWallet = 7,
        
        [Description("Corporate Account")]
        CorporateAccount = 8,
        
        [Description("Gift Card")]
        GiftCard = 9,
        
        [Description("Store Credit")]
        StoreCredit = 10
    }

    public enum PaymentStatus
    {
        [Description("Pending")]
        Pending = 1,
        
        [Description("Partial")]
        Partial = 2,
        
        [Description("Paid")]
        Paid = 3,
        
        [Description("Overdue")]
        Overdue = 4,
        
        [Description("Cancelled")]
        Cancelled = 5,
        
        [Description("Refunded")]
        Refunded = 6,
        
        [Description("Failed")]
        Failed = 7
    }

    public enum TransactionType
    {
        [Description("Payment")]
        Payment = 1,
        
        [Description("Refund")]
        Refund = 2,
        
        [Description("Partial Refund")]
        PartialRefund = 3,
        
        [Description("Chargeback")]
        Chargeback = 4,
        
        [Description("Authorization")]
        Authorization = 5,
        
        [Description("Capture")]
        Capture = 6,
        
        [Description("Void")]
        Void = 7
    }

    public enum TransactionStatus
    {
        [Description("Pending")]
        Pending = 1,
        
        [Description("Processing")]
        Processing = 2,
        
        [Description("Approved")]
        Approved = 3,
        
        [Description("Completed")]
        Completed = 4,
        
        [Description("Failed")]
        Failed = 5,
        
        [Description("Cancelled")]
        Cancelled = 6,
        
        [Description("Declined")]
        Declined = 7,
        
        [Description("Expired")]
        Expired = 8,
        
        [Description("Voided")]
        Voided = 9
    }

    public enum BillingItemType
    {
        [Description("Room")]
        Room = 1,
        
        [Description("Food & Beverage")]
        FoodBeverage = 2,
        
        [Description("Spa Services")]
        SpaServices = 3,
        
        [Description("Laundry")]
        Laundry = 4,
        
        [Description("Room Service")]
        RoomService = 5,
        
        [Description("Minibar")]
        Minibar = 6,
        
        [Description("Telephone")]
        Telephone = 7,
        
        [Description("Internet")]
        Internet = 8,
        
        [Description("Parking")]
        Parking = 9,
        
        [Description("Transportation")]
        Transportation = 10,
        
        [Description("Conference Room")]
        ConferenceRoom = 11,
        
        [Description("Fitness Center")]
        FitnessCenter = 12,
        
        [Description("Business Center")]
        BusinessCenter = 13,
        
        [Description("Concierge Services")]
        ConciergeServices = 14,
        
        [Description("Late Checkout")]
        LateCheckout = 15,
        
        [Description("Pet Fee")]
        PetFee = 16,
        
        [Description("Resort Fee")]
        ResortFee = 17,
        
        [Description("Damage Fee")]
        DamageFee = 18,
        
        [Description("Cleaning Fee")]
        CleaningFee = 19,
        
        [Description("Miscellaneous")]
        Miscellaneous = 20
    }

    public enum DiscountType
    {
        [Description("None")]
        None = 0,
        
        [Description("Percentage")]
        Percentage = 1,
        
        [Description("Fixed Amount")]
        FixedAmount = 2,
        
        [Description("Senior Discount")]
        SeniorDiscount = 3,
        
        [Description("Military Discount")]
        MilitaryDiscount = 4,
        
        [Description("Corporate Rate")]
        CorporateRate = 5,
        
        [Description("Group Discount")]
        GroupDiscount = 6,
        
        [Description("Loyalty Member")]
        LoyaltyMember = 7,
        
        [Description("Promotional Code")]
        PromotionalCode = 8,
        
        [Description("Package Deal")]
        PackageDeal = 9,
        
        [Description("Early Bird")]
        EarlyBird = 10,
        
        [Description("Last Minute")]
        LastMinute = 11
    }

    public enum CardType
    {
        [Description("Visa")]
        Visa = 1,
        
        [Description("MasterCard")]
        MasterCard = 2,
        
        [Description("American Express")]
        AmericanExpress = 3,
        
        [Description("Discover")]
        Discover = 4,
        
        [Description("Diners Club")]
        DinersClub = 5,
        
        [Description("JCB")]
        JCB = 6,
        
        [Description("Union Pay")]
        UnionPay = 7
    }
}