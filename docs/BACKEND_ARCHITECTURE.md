# Kiến Trúc Backend - ABP Microservice

Tài liệu mô tả cấu trúc thư mục và phân lớp chuẩn **ABP Framework Microservice** cho hệ thống **RestaurantManagement**.

---

## 1. Tổng Quan Cấu Trúc `backend/`

```text
backend/
├── apps/
│   └── AuthServer/
│       └── src/RestaurantManagement.AuthServer/           # OpenIddict Auth Server (SSO, Login, Token)
├── gateways/
│   └── ApiGateway/
│       └── src/RestaurantManagement.ApiGateway/           # YARP Reverse Proxy, Rate Limiting, Swagger Aggregation
├── shared/
│   ├── RestaurantManagement.Shared.Hosting/               # Cấu hình chung (Serilog, Swagger, HealthChecks, OpenTelemetry)
│   ├── RestaurantManagement.Shared.Hosting.Microservices/ # RabbitMQ EventBus, Redis Cache, JWT Bearer Auth
│   ├── RestaurantManagement.Shared.Hosting.Gateways/      # Cấu hình YARP Gateway, CORS
│   ├── RestaurantManagement.Shared.Localization/          # Đa ngôn ngữ dùng chung (vi, ko, en)
│   └── RestaurantManagement.Shared.Contracts/             # Shared ETOs (Distributed Events), Constants, Enums
└── services/
    ├── IdentityService/
    ├── BranchService/
    ├── EmployeeService/
    ├── MenuService/
    ├── TableBookingService/
    ├── OrderService/
    ├── PaymentService/
    ├── InventorySupplierService/
    ├── AiOcrService/
    ├── NotificationChatService/
    ├── NewsfeedService/
    └── ReportingService/
```

---

## 2. Cấu Trúc Chuẩn Bên Trong Mỗi Microservice

Mỗi service trong `backend/services/<ServiceName>/` tuân theo kiến trúc **DDD Layered Architecture** của ABP Framework:

```text
<ServiceName>/
├── src/
│   ├── RestaurantManagement.<ServiceName>.Domain.Shared/           # Enums, Constants, Localization, ErrorCodes
│   ├── RestaurantManagement.<ServiceName>.Domain/                  # Entities, Aggregates, ValueObjects, DomainServices, Repository Interfaces
│   ├── RestaurantManagement.<ServiceName>.Application.Contracts/   # AppService Interfaces, DTOs, Permissions, Validators
│   ├── RestaurantManagement.<ServiceName>.Application/             # AppService Implementations, AutoMapper Profiles, Event Handlers
│   ├── RestaurantManagement.<ServiceName>.EntityFrameworkCore/     # DbContext riêng của service, EF Core Configurations, Migrations, Repositories
│   ├── RestaurantManagement.<ServiceName>.HttpApi/                 # REST API Controllers
│   ├── RestaurantManagement.<ServiceName>.HttpApi.Client/          # C# Dynamic/Static Client Proxies (gọi giữa các service)
│   └── RestaurantManagement.<ServiceName>.HttpApi.Host/            # Entry point (Program.cs), Module Host, Swagger, SignalR Hub (nếu có)
└── test/
    ├── RestaurantManagement.<ServiceName>.Domain.Tests/            # Unit test cho Domain logic
    ├── RestaurantManagement.<ServiceName>.Application.Tests/       # Integration/Unit test cho Application Services
    └── RestaurantManagement.<ServiceName>.EntityFrameworkCore.Tests/ # Test DbContext & Repositories
```

---

## 3. Luồng Giao Tiếp Giữa Các Thành Phần

1. **Client (Angular Web / Flutter Mobile) -> `ApiGateway` (YARP):**
   - Toàn bộ request từ bên ngoài đi qua `ApiGateway`.
   - `ApiGateway` xác thực token với `AuthServer` và định tuyến tới đúng Microservice.
2. **Giao tiếp đồng bộ (Synchronous):**
   - Dùng HTTP/REST qua `HttpApi.Client` (hoặc gRPC nội bộ) khi cần lấy dữ liệu tức thời từ service khác.
3. **Giao tiếp bất đồng bộ (Asynchronous / Event-Driven):**
   - Các service publish/subscribe **Distributed Events** qua **RabbitMQ** thông qua các ETO định nghĩa trong `RestaurantManagement.Shared.Contracts`.
   - Ví dụ: `PaymentService` hoàn tất hóa đơn -> phát event `OrderPaidEto` -> `InventorySupplierService` trừ kho & `ReportingService` cập nhật doanh thu.
4. **Realtime:**
   - `NotificationChatService` sử dụng **SignalR** kết hợp **Redis Backplane** để đẩy tin nhắn chat, thông báo đặt bàn, trạng thái bếp theo thời gian thực.
