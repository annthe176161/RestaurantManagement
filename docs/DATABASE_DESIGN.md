# Thiết Kế Cơ Sở Dữ Liệu (Database Design - Database per Service)

Tài liệu thiết kế chi tiết hệ thống cơ sở dữ liệu cho kiến trúc **ABP Microservice** sử dụng **SQL Server** và **Entity Framework Core (Code-First)**.

---

## 1. Quy Ước Chung

1. **Database per Service:** Mỗi Microservice sở hữu một database độc lập trên SQL Server để đảm bảo tính tách biệt và scale độc lập.
2. **ABP Base Entities & Audit:** Các bảng nghiệp vụ chính kế thừa `FullAuditedAggregateRoot<Guid>` hoặc `FullAuditedEntity<Guid>` của ABP Framework (tự động có `Id`, `CreationTime`, `CreatorId`, `LastModificationTime`, `LastModifierId`, `IsDeleted`, `DeleterId`, `DeletionTime`).
3. **Liên kết giữa các Service (Logical Foreign Key):** Không tạo Foreign Key vật lý xuyên database. Service này lưu `Guid` định danh của service kia (ví dụ: `BranchId`, `UserId`, `OrderId`) và đồng bộ dữ liệu qua **RabbitMQ Distributed Event Bus**.
4. **Quy ước đặt tên Database (Local SQL Server):** `RM_<TenService>Db` (ví dụ: `RM_IdentityDb`, `RM_BranchDb`...).

---

## 2. Chi Tiết Database Theo Từng Microservice

### 2.1. `RM_IdentityDb` (`IdentityService` & `AuthServer`)
Quản lý tài khoản, phân quyền RBAC và cấp phát token OpenIddict/JWT.
- **Các bảng chuẩn của ABP Identity & OpenIddict:** `AbpUsers`, `AbpRoles`, `AbpUserRoles`, `AbpPermissionGrants`, `OpenIddictApplications`, `OpenIddictAuthorizations`, `OpenIddictScopes`, `OpenIddictTokens`.
- **Mở rộng bảng `AbpUsers` (Extra Properties):**
  - `FullName` (`nvarchar(150)`)
  - `AvatarUrl` (`nvarchar(500)`, nullable)
  - `DefaultBranchId` (`uniqueidentifier`, nullable — gán cơ sở làm việc chính cho Quản lý, Nhân viên ngoài, Bếp)
  - `PreferredLanguage` (`nvarchar(10)` — `vi`, `ko`, `en`)
- **5 Roles mặc định cần Seed:** `Boss` (Sếp), `BranchManager` (Quản lý), `FrontStaff` (Nhân viên ngoài), `KitchenStaff` (Bếp), `Customer` (Khách hàng).

---

### 2.2. `RM_BranchDb` (`BranchService`)
Quản lý thông tin chuỗi nhà hàng, các cơ sở và khu vực bàn.

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`Branches`** | `Id`, `Code` (unique), `Name`, `Address`, `Phone`, `Email`, `OpenTime`, `CloseTime`, `TotalTables`, `Status` (Active/Inactive/Maintenance), `Latitude`, `Longitude`, `ManagerUserId` | Thông tin từng cơ sở trong chuỗi |
| **`BranchAreas`** | `Id`, `BranchId` (FK), `Name` (Phòng VIP, Khu BBQ, Ngoài trời...), `Description`, `SortOrder`, `IsActive` | Các khu vực phục vụ trong cơ sở |

---

### 2.3. `RM_EmployeeDb` (`EmployeeService`)
Quản lý hồ sơ nhân sự, xếp lịch làm việc theo tuần, check-in/check-out và tính lương.

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`Employees`** | `Id`, `UserId` (Logical FK), `BranchId` (Logical FK), `EmployeeCode` (unique), `Position` (Manager/FrontStaff/KitchenStaff), `ContractType` (FullTime/PartTime), `BaseSalary`, `HourlyRate`, `HireDate`, `Status` | Hồ sơ nhân viên & mức lương cơ bản |
| **`ShiftTemplates`** | `Id`, `BranchId`, `Name` (Ca sáng, Ca chiều, Ca tối), `StartTime`, `EndTime`, `BreakMinutes` | Khung ca làm chuẩn của cơ sở |
| **`WeeklySchedules`** | `Id`, `BranchId`, `EmployeeId` (FK), `ShiftTemplateId` (FK), `WorkDate`, `StartTime`, `EndTime`, `Status` (Scheduled/SwapRequested/Approved/Cancelled), `Note` | Lịch phân công làm việc theo tuần |
| **`ShiftSwapRequests`** | `Id`, `ScheduleId` (FK), `FromEmployeeId`, `ToEmployeeId`, `Reason`, `Status` (Pending/Approved/Rejected), `ReviewedByUserId` | Yêu cầu đổi ca của nhân viên |
| **`Attendances`** | `Id`, `EmployeeId` (FK), `BranchId`, `ScheduleId` (nullable FK), `WorkDate`, `CheckInTime`, `CheckOutTime`, `CheckInMethod` (QR/GPS/Manual), `LateMinutes`, `EarlyLeaveMinutes`, `OvertimeMinutes`, `WorkedHours` | Dữ liệu check-in / check-out thực tế |
| **`Payrolls`** | `Id`, `EmployeeId` (FK), `BranchId`, `Year`, `Month`, `TotalWorkedHours`, `OvertimeHours`, `BaseAmount`, `OvertimeAmount`, `Allowance`, `Bonus`, `Deduction`, `NetSalary`, `Status` (Draft/Confirmed/Paid) | Bảng lương tổng hợp hàng tháng |

---

### 2.4. `RM_MenuDb` (`MenuService`)
Quản lý danh mục món ăn, thực đơn đa ngôn ngữ, giá/trạng thái theo từng cơ sở và mẫu in menu.

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`MenuCategories`** | `Id`, `NameVi`, `NameKo`, `NameEn`, `SortOrder`, `IsActive` | Danh mục (Thịt nướng, Lẩu, Tokbokki, Đồ uống...) |
| **`MenuItems`** | `Id`, `CategoryId` (FK), `Code`, `NameVi`, `NameKo`, `NameEn`, `Description`, `ImageUrl`, `DefaultPrice`, `Unit`, `IsCombo`, `IsActive` | Danh sách món ăn chuẩn của toàn chuỗi |
| **`BranchMenuItems`** | `Id`, `BranchId` (Logical FK), `MenuItemId` (FK), `Price`, `IsAvailable` (Còn món / Hết món), `IsFeatured` | Giá bán & trạng thái còn/hết món tại từng cơ sở |
| **`MenuPrintTemplates`** | `Id`, `Name`, `PaperSize` (A4/A3), `LayoutType`, `ConfigJson` | Cấu hình mẫu in PDF Menu |

---

### 2.5. `RM_TableBookingDb` (`TableBookingService`)
Quản lý sơ đồ bàn trực quan và đặt bàn trực tuyến.

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`DiningTables`** | `Id`, `BranchId`, `AreaId` (Logical FK), `TableCode`, `Capacity`, `Status` (Available/Occupied/Reserved/Cleaning), `QrCodeToken` | Danh sách bàn & trạng thái thời gian thực |
| **`Reservations`** | `Id`, `BookingCode`, `BranchId`, `CustomerUserId` (nullable), `CustomerName`, `CustomerPhone`, `ReservationTime`, `GuestCount`, `PreferredAreaId`, `AssignedTableId` (nullable FK), `Note`, `Status` (Pending/Confirmed/Seated/Completed/Cancelled/NoShow), `ConfirmedByUserId` | Đơn đặt bàn của khách hàng |

---

### 2.6. `RM_OrderDb` (`OrderService`)
Quản lý gọi món tại bàn (Nhân viên ngoài / QR Order) và luồng chế biến của Bếp (KDS).

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`Orders`** | `Id`, `OrderCode`, `BranchId`, `TableId` (Logical FK), `OrderType` (DineIn/QrSelfOrder/TakeAway), `CreatedByEmployeeId` (nullable), `CustomerUserId` (nullable), `Status` (Open/InProgress/Ready/Completed/Cancelled), `SubTotal`, `DiscountAmount`, `VatAmount`, `TotalAmount`, `Note` | Đơn gọi món theo bàn / đơn mang về |
| **`OrderItems`** | `Id`, `OrderId` (FK), `MenuItemId` (Logical FK), `MenuItemName`, `Quantity`, `UnitPrice`, `LineTotal`, `KitchenStatus` (Pending/Cooking/Ready/Served/Cancelled), `Note` | Chi tiết từng món & trạng thái bếp |

---

### 2.7. `RM_PaymentDb` (`PaymentService`)
Quản lý thanh toán và hóa đơn bán hàng (đầu ra / xuất).

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`SalesInvoices`** | `Id`, `InvoiceNumber`, `BranchId`, `OrderId` (Logical FK), `IssuedAt`, `SubTotal`, `DiscountAmount`, `VatRate`, `VatAmount`, `TotalAmount`, `PaymentStatus` (Unpaid/Paid/Refunded), `PdfUrl` | Hóa đơn bán hàng (xuất) |
| **`PaymentTransactions`** | `Id`, `SalesInvoiceId` (FK), `Method` (Cash/BankTransfer/Card/EWallet), `Amount`, `TransactionRef`, `PaidAt`, `CashierUserId` | Lịch sử giao dịch thanh toán |

---

### 2.8. `RM_InventorySupplierDb` (`InventorySupplierService`)
Quản lý nhà cung cấp, nguyên liệu, định lượng món ăn và hóa đơn nhập hàng (đầu vào / mua).

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`Suppliers`** | `Id`, `Code`, `Name`, `TaxCode`, `Phone`, `Address`, `ContactPerson` | Danh sách nhà cung cấp nguyên liệu |
| **`Ingredients`** | `Id`, `Code`, `Name`, `Unit` (kg/g/lít/chai), `MinStockThreshold` | Danh mục nguyên liệu (thịt, rau, gia vị Hàn...) |
| **`BranchInventories`** | `Id`, `BranchId`, `IngredientId` (FK), `CurrentQuantity`, `LastUpdatedAt` | Tồn kho thực tế theo từng cơ sở |
| **`MenuItemRecipes`** | `Id`, `MenuItemId` (Logical FK), `IngredientId` (FK), `QuantityRequired` | Định lượng nguyên liệu trừ kho cho mỗi món |
| **`PurchaseInvoices`** | `Id`, `InvoiceNumber`, `BranchId`, `SupplierId` (nullable FK), `SupplierNameSnapshot`, `InvoiceDate`, `TotalAmount`, `VatAmount`, `ImageUrl`, `Source` (Manual/AiOcr), `Status` (Draft/Verified/Approved) | Hóa đơn nhập hàng / chi phí (mua) |
| **`PurchaseInvoiceItems`** | `Id`, `PurchaseInvoiceId` (FK), `IngredientId` (nullable FK), `ItemName`, `Quantity`, `Unit`, `UnitPrice`, `LineTotal` | Chi tiết từng dòng hàng trên hóa đơn nhập |

---

### 2.9. `RM_AiOcrDb` (`AiOcrService`)
Quản lý tiến trình AI đọc hóa đơn từ ảnh/PDF và bóc tách dữ liệu có cấu trúc.

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`OcrJobs`** | `Id`, `BranchId`, `UploadedByUserId`, `FileUrl`, `FileType` (Image/Pdf), `DocumentType` (PurchaseInvoice/SalesReceipt), `Status` (Pending/Processing/Completed/Failed), `RawResponseJson`, `ExtractedTableJson`, `ConfidenceScore`, `ErrorMessage`, `ProcessedAt` | Lưu vết ảnh hóa đơn & kết quả bảng AI bóc tách |

---

### 2.10. `RM_NotificationChatDb` (`NotificationChatService`)
Quản lý chat realtime (SignalR) và lịch sử thông báo hệ thống.

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`ChatRooms`** | `Id`, `RoomType` (BranchGroup/DepartmentGroup/Direct/CustomerSupport), `BranchId` (nullable), `Name` | Phòng chat theo cơ sở, bộ phận hoặc 1-1 |
| **`ChatRoomMembers`** | `Id`, `RoomId` (FK), `UserId`, `JoinedAt`, `LastReadMessageId` | Thành viên phòng chat & trạng thái đã đọc |
| **`ChatMessages`** | `Id`, `RoomId` (FK), `SenderUserId`, `MessageType` (Text/Image/File), `Content`, `AttachmentUrl`, `SentAt` | Nội dung tin nhắn |
| **`Notifications`** | `Id`, `RecipientUserId`, `Title`, `Body`, `Type` (NewBooking/ShiftPublished/Chat/OcrDone/MenuUpdated), `ReferenceId`, `IsRead`, `ReadAt` | Thông báo realtime đẩy tới người dùng |

---

### 2.11. `RM_NewsfeedDb` (`NewsfeedService`)
Quản lý bảng tin (kiểu Facebook) cho khách hàng xem và tương tác.

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`Posts`** | `Id`, `BranchId` (nullable — null nếu bài viết toàn chuỗi), `AuthorUserId`, `Category` (Promotion/NewMenu/Event/Review), `Title`, `Content`, `MediaUrlsJson`, `LikeCount`, `CommentCount`, `IsPublished`, `PublishedAt` | Bài đăng trên Newsfeed |
| **`PostLikes`** | `Id`, `PostId` (FK), `UserId`, `CreatedAt` | Lượt thích bài viết |
| **`PostComments`** | `Id`, `PostId` (FK), `UserId`, `ParentCommentId` (nullable), `Content`, `CreatedAt` | Bình luận & phản hồi trên bài viết |

---

### 2.12. `RM_ReportingDb` (`ReportingService` - CQRS Read Model)
Lưu dữ liệu tổng hợp sẵn để Sếp xem Dashboard tốc độ cao và xuất file Excel cuối tháng.

| Bảng | Các cột chính | Mô tả |
| :--- | :--- | :--- |
| **`DailyBranchRevenues`** | `Id`, `BranchId`, `Date`, `TotalOrders`, `GrossRevenue`, `DiscountTotal`, `VatTotal`, `NetRevenue`, `PurchaseCost`, `PayrollCost`, `EstimatedProfit` | Số liệu doanh thu – chi phí – lợi nhuận tổng hợp theo ngày/cơ sở |
| **`MonthlyExportLogs`** | `Id`, `Year`, `Month`, `BranchId` (nullable), `ReportType` (Revenue/Payroll/Invoices/Summary), `FileUrl`, `GeneratedByUserId`, `GeneratedAt` | Lịch sử các file Excel báo cáo đã xuất |

---

## 3. Luồng Đồng Bộ Dữ Liệu Qua RabbitMQ (Distributed Events)

| Sự kiện (ETO trong `Shared.Contracts`) | Service phát (Publisher) | Service nhận (Subscriber) | Hành động xử lý |
| :--- | :--- | :--- | :--- |
| `ReservationCreatedEto` | `TableBookingService` | `NotificationChatService` | Báo realtime cho Quản lý & Nhân viên ngoài cơ sở |
| `OrderSubmittedToKitchenEto` | `OrderService` | `NotificationChatService` | Đẩy đơn mới lên màn hình Bếp theo thời gian thực |
| `OrderPaidEto` | `PaymentService` | `InventorySupplierService`, `ReportingService`, `TableBookingService` | Trừ kho theo định lượng, cộng doanh thu ngày, trả trạng thái bàn |
| `OcrInvoiceExtractedEto` | `AiOcrService` | `InventorySupplierService`, `NotificationChatService` | Tạo nháp `PurchaseInvoices` và báo Quản lý kiểm tra |
| `PurchaseInvoiceApprovedEto` | `InventorySupplierService` | `ReportingService` | Cộng tồn kho cơ sở & ghi nhận chi phí nhập hàng |
| `PayrollConfirmedEto` | `EmployeeService` | `ReportingService`, `NotificationChatService` | Ghi nhận chi phí lương tháng & gửi thông báo cho nhân viên |
