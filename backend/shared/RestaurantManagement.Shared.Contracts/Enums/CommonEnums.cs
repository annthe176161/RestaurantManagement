namespace RestaurantManagement.Shared.Contracts.Enums;

public enum BranchStatus
{
    Active = 1,
    Inactive = 2,
    Maintenance = 3
}

public enum StaffPosition
{
    BranchManager = 1,
    FrontStaff = 2,
    KitchenStaff = 3
}

public enum ContractType
{
    FullTime = 1,
    PartTime = 2
}

public enum ScheduleStatus
{
    Scheduled = 1,
    SwapRequested = 2,
    Approved = 3,
    Cancelled = 4
}

public enum ApprovalStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3
}

public enum CheckInMethod
{
    QrCode = 1,
    Gps = 2,
    Manual = 3
}

public enum PayrollStatus
{
    Draft = 1,
    Confirmed = 2,
    Paid = 3
}

public enum TableStatus
{
    Available = 1,
    Occupied = 2,
    Reserved = 3,
    Cleaning = 4
}

public enum ReservationStatus
{
    Pending = 1,
    Confirmed = 2,
    Seated = 3,
    Completed = 4,
    Cancelled = 5,
    NoShow = 6
}

public enum OrderType
{
    DineIn = 1,
    QrSelfOrder = 2,
    TakeAway = 3
}

public enum OrderStatus
{
    Open = 1,
    InProgress = 2,
    Ready = 3,
    Completed = 4,
    Cancelled = 5
}

public enum KitchenItemStatus
{
    Pending = 1,
    Cooking = 2,
    Ready = 3,
    Served = 4,
    Cancelled = 5
}

public enum PaymentStatus
{
    Unpaid = 1,
    Paid = 2,
    Refunded = 3
}

public enum PaymentMethod
{
    Cash = 1,
    BankTransfer = 2,
    Card = 3,
    EWallet = 4
}

public enum InvoiceSource
{
    Manual = 1,
    AiOcr = 2
}

public enum PurchaseInvoiceStatus
{
    Draft = 1,
    Verified = 2,
    Approved = 3
}

public enum OcrFileType
{
    Image = 1,
    Pdf = 2
}

public enum OcrDocumentType
{
    PurchaseInvoice = 1,
    SalesReceipt = 2
}

public enum OcrJobStatus
{
    Pending = 1,
    Processing = 2,
    Completed = 3,
    Failed = 4
}

public enum ChatRoomType
{
    BranchGroup = 1,
    DepartmentGroup = 2,
    Direct = 3,
    CustomerSupport = 4
}

public enum ChatMessageType
{
    Text = 1,
    Image = 2,
    File = 3
}

public enum NotificationType
{
    NewBooking = 1,
    ShiftPublished = 2,
    Chat = 3,
    OcrDone = 4,
    MenuUpdated = 5,
    OrderReady = 6
}

public enum PostCategory
{
    Promotion = 1,
    NewMenu = 2,
    Event = 3,
    Review = 4
}

public enum ReportType
{
    Revenue = 1,
    Payroll = 2,
    Invoices = 3,
    Summary = 4
}
