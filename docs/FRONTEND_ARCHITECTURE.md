# Kiến Trúc Frontend Web - Angular + NG-ZORRO

Tài liệu mô tả cấu trúc thư mục Frontend (`frontend/web-app/`) sử dụng **Angular + TypeScript + NG-ZORRO**, kết nối tới hệ thống Backend qua `ApiGateway` và tương thích với cơ chế **ABP Proxy**.

---

## 1. Cấu Trúc Thư Mục `frontend/web-app/`

```text
frontend/web-app/
└── src/
    ├── app/
    │   ├── core/                        # Thành phần dùng chung toàn app (Singleton)
    │   │   ├── auth/                    # OIDC/JWT Auth, Token Storage, Auth Callback
    │   │   ├── guards/                  # AuthGuard, RoleGuard (Sếp, Quản lý, Nhân viên ngoài, Bếp, Khách)
    │   │   ├── interceptors/            # HTTP Interceptor (JWT Bearer, Error Handling, Loading)
    │   │   ├── services/                # SignalR Realtime Service, Notification Service, Config
    │   │   └── models/                  # Core models (User, Role, ApiResponse)
    │   │
    │   ├── shared/                      # UI Components & Helpers tái sử dụng
    │   │   ├── ng-zorro/                # Export tập trung các NG-ZORRO Modules (Table, Modal, Form...)
    │   │   ├── components/              # Shared UI (Charts, DataTable, PdfPreview, UploadOcr)
    │   │   ├── directives/              # PermissionDirective (*appHasPermission)
    │   │   ├── pipes/                   # CurrencyVndPipe, DateFormatPipe
    │   │   └── utils/                   # ExcelHelper, PrintHelper
    │   │
    │   ├── proxy/                       # ABP Service Proxies kết nối tới từng Microservice qua ApiGateway
    │   │   ├── identity/
    │   │   ├── branch/
    │   │   ├── employee/
    │   │   ├── menu/
    │   │   ├── table-booking/
    │   │   ├── order/
    │   │   ├── payment/
    │   │   ├── inventory-supplier/
    │   │   ├── ai-ocr/
    │   │   ├── notification-chat/
    │   │   ├── newsfeed/
    │   │   └── reporting/
    │   │
    │   ├── layout/                      # Bố cục giao diện theo nhóm người dùng
    │   │   ├── admin-layout/            # Giao diện quản trị/vận hành (Sếp, Quản lý, Nhân viên ngoài, Bếp)
    │   │   ├── customer-layout/         # Giao diện khách hàng (Newsfeed, Đặt bàn, Xem Menu)
    │   │   └── auth-layout/             # Giao diện Đăng nhập / Đăng ký
    │   │
    │   └── features/                    # Các phân hệ chức năng (Lazy-loaded)
    │       ├── dashboard/               # Dashboard doanh thu toàn chuỗi & từng cơ sở (Sếp)
    │       ├── branches/                # Quản lý danh sách cơ sở / chi nhánh
    │       ├── employees/               # Xếp lịch tuần, check-in/out, tính lương
    │       ├── menu/                    # Thực đơn, giá theo cơ sở, in menu PDF / QR
    │       ├── table-booking/           # Sơ đồ bàn & đặt bàn trực tuyến
    │       ├── orders/                  # POS gọi món (Nhân viên ngoài) & Màn hình Bếp (KDS)
    │       ├── invoices/                # Hóa đơn xuất/mua, AI OCR đọc hóa đơn
    │       ├── inventory-suppliers/     # Quản lý kho, định lượng & nhà cung cấp
    │       ├── newsfeed/                # Bảng tin (Facebook-style) cho Khách hàng & Quản lý
    │       ├── chat-notifications/      # Chat realtime & Trung tâm thông báo
    │       ├── reports/                 # Báo cáo cuối tháng & xuất file Excel
    │       └── settings/                # Phân quyền RBAC, tài khoản, cấu hình
    │
    ├── assets/
    │   ├── i18n/                        # Đa ngôn ngữ (vi.json, ko.json, en.json)
    │   ├── images/
    │   └── styles/
    │
    └── environments/                    # Cấu hình API Gateway URL theo môi trường (dev, prod)
```

---

## 2. Phân Luồng Giao Diện Theo Vai Trò

| Vai trò | Layout sử dụng | Phân hệ truy cập chính |
| :--- | :--- | :--- |
| **Sếp** | `admin-layout` | `dashboard`, `branches`, `reports`, `invoices`, `employees`, `menu`, `settings` |
| **Quản lý** | `admin-layout` | `branches` (cơ sở phụ trách), `employees`, `menu`, `table-booking`, `invoices`, `inventory-suppliers`, `newsfeed`, `chat-notifications` |
| **Nhân viên ngoài** | `admin-layout` | `employees` (lịch làm, check-in/out, lương cá nhân), `table-booking`, `orders` (POS tại bàn), `invoices` (hóa đơn bán), `chat-notifications` |
| **Bếp** | `admin-layout` | `employees` (lịch làm, check-in/out, lương cá nhân), `orders` (màn hình Bếp KDS), `menu` (báo hết món/nguyên liệu), `chat-notifications` |
| **Khách hàng** | `customer-layout` | `newsfeed`, `branches` (xem cơ sở), `menu` (xem thực đơn), `table-booking` (đặt bàn), `chat-notifications` |
